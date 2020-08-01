using CrossCutting.Logging.Contracts;
using Logic.Chat.Contracts;
using Logic.Emotes.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitch.Contracts;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace Logic.Twitch
{
    public class Bot : IBot
    {
        private readonly TwitchClient client;
        private readonly ILogger logger;
        private readonly IEmoteCache emoteCache;
        private readonly MessageConverter messageConverter;
        private readonly IMessageFormatManager messageFormatManager;

        public event EventHandler Connected;
        public event EventHandler<CrossCutting.DataClasses.Message> MessageReceived;

        public Bot(ILogger logger, IEmoteCache emoteCache, IMessageFormatManager messageFormatManager)
        {
            this.logger = logger;
            this.emoteCache = emoteCache;
            this.messageFormatManager = messageFormatManager;
            messageConverter = new MessageConverter(emoteCache);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
        }

        public void Connect(string username, string token)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(username, token);

            client.Initialize(credentials);

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;

            client.Connect();
        }

        public void JoinChannel(string name)
        {
            this.client.JoinChannel(name);
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            logger.Log($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            logger.Log($"Connected to {e.AutoJoinChannel}");
            this.Connected?.Invoke(this, new EventArgs());
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            logger.Log("Hey guys! I am a bot connected via TwitchLib!");
            //client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            CrossCutting.DataClasses.Message message = this.messageConverter.ConvertMessage(e.ChatMessage);
            this.messageFormatManager?.Format(message);
            MessageReceived?.Invoke(this, message);
            logger.Log($"{e.ChatMessage.Username}: {e.ChatMessage.Message} {e.ChatMessage.EmoteReplacedMessage}");
            //if (e.ChatMessage.Message.Contains("badword"))
            //    client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            //if (e.WhisperMessage.Username == "my_friend")
            //    client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            logger.Log($"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
            {
                logger.Log($"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            }
            else
            {
                logger.Log($"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
            }
        }
    }
}
