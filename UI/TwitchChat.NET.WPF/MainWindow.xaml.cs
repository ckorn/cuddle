using CrossCutting.Logging;
using CrossCutting.Logging.Contracts;
using Logic.Authorization;
using Logic.Chat;
using Logic.Chat.Contracts;
using Logic.Emotes;
using Logic.Emotes.Contracts;
using Logic.HttpClient;
using Logic.ImageManagement;
using Logic.Settings;
using Logic.Settings.Contracts;
using Logic.Twitch;
using LogicAuthorization.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        private readonly IBot bot;
        private readonly IAcquireToken acquireToken;
        private readonly ISettings settings;
        private readonly IEmoteCache emoteCache;
        public MainWindow()
        {
            InitializeComponent();

            emoteCache = new EmoteCache(new EmoteFactory(new Client(), new BitmapFunctions()));
            bot = new Bot(new Logger(), emoteCache, this.textEditor.ImageElementGenerator);
            acquireToken = new AcquireToken();
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
            bot.JoinChannel("cohhcarnage");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string token = settings.Token;
            //if (string.IsNullOrEmpty(token))
            //{
            //    token = acquireToken.GetToken();
            //    settings.Token = token;
            //}
            bot.Connect("tarosmolos", "***REMOVED***");

            ////using (IClient client = new Client())
            //{
            //    client.Connect("irc.chat.twitch.tv", 6697);
            //    client.Login("tarosmolos", "***REMOVED***");
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
