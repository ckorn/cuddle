using CrossCutting.Logging;
using CrossCutting.Logging.Contracts;
using Logic.Authorization;
using Logic.Badges;
using Logic.Badges.Contracts;
using Logic.Chat;
using Logic.Chat.Contracts;
using Logic.Emotes;
using Logic.Emotes.Contracts;
using Logic.HttpClient;
using Logic.HttpClient.Contracts;
using Logic.ImageManagement;
using Logic.ImageManagement.Contracts;
using Logic.Settings;
using Logic.Settings.Contracts;
using Logic.Twitch;
using LogicAuthorization.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Twitch.Contracts;

namespace TwitchChat.NET.WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger logger;
        private readonly IHttpClient httpClient;
        private readonly IBitmapFunctions bitmapFunctions;
        private readonly IBot bot;
        private readonly IAcquireToken acquireToken;
        private readonly ISettings settings;
        private readonly IEmoteCache emoteCache;
        private readonly IBadgeCache badgeCache;
        private readonly ICredentialsManagement credentialsManagement;
        public MainWindow()
        {
            InitializeComponent();

            logger = new Logger();
            httpClient = new Client();
            bitmapFunctions = new BitmapFunctions();
            credentialsManagement = new CredentialsManagement();
            emoteCache = new EmoteCache(new EmoteFactory(httpClient, bitmapFunctions));
            badgeCache = new BadgeCache(new BadgeFactory(httpClient, bitmapFunctions, logger));
            bot = new Bot(logger, emoteCache, badgeCache, this.textEditor, credentialsManagement);
            acquireToken = new AcquireToken(credentialsManagement);
            settings = new Settings();
            bot.Connected += Bot_Connected;
            bot.MessageReceived += this.Bot_MessageReceived;
        }
        private void Bot_MessageReceived(object sender, CrossCutting.DataClasses.Message e)
        {
            void doIt()
            {
                this.textEditor.AppendText($"{e.DisplayMessage}{Environment.NewLine}");
            }
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => doIt());
            }
            else
            {
                doIt();
            }
        }

        private void Bot_Connected(object sender, EventArgs e)
        {
            bot.JoinChannel("raupling");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string token = settings.Token;
            //if (string.IsNullOrEmpty(token))
            //{
            //    token = acquireToken.GetToken();
            //    settings.Token = token;
            //}
            bot.Connect("tarosmolos");

            ////using (IClient client = new Client())
            //{
            //    client.Connect("irc.chat.twitch.tv", 6697);
            //    client.Login("tarosmolos", token);
            //    //Channel channel = client.GetChannel("raupling");
            //}
        }

        private class Logger : ILogger
        {
            public void Log(string line)
            {
                System.Console.WriteLine(line);
            }
        }
    }
}
