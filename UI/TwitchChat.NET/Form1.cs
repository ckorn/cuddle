using CrossCutting.Logging.Contracts;
using Logic.Authorization;
using Logic.Chat;
using Logic.Chat.Contracts;
using Logic.Emotes;
using Logic.Emotes.Contracts;
using Logic.HttpClient;
using Logic.ImageManagement;
using Logic.ImageManagement.Contracts;
using Logic.Settings;
using Logic.Settings.Contracts;
using Logic.Twitch;
using LogicAuthorization.Contracts;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Twitch.Contracts;

namespace TwitchChat.NET
{
    public partial class Form1 : Form
    {
        private readonly IBot bot;
        private readonly IAcquireToken acquireToken;
        private readonly ISettings settings;
        private readonly IEmoteCache emoteCache;
        public Form1()
        {
            InitializeComponent();
            emoteCache = new EmoteCache(new EmoteFactory(new Client(), new BitmapFunctions()));
            bot = new Bot(new Logger(this), emoteCache, null);
            acquireToken = new AcquireToken();
            settings = new Settings();
            bot.Connected += Bot_Connected;
            bot.MessageReceived += this.Bot_MessageReceived;
        }

        private void Bot_MessageReceived(object sender, CrossCutting.DataClasses.Message e)
        {
            //if (this.InvokeRequired)
            //{
            //    this.richTextBoxOutput.Invoke(new Action(() => this.richTextBoxOutput.AppendText(e.EmoteText + Environment.NewLine)));
            //}
            //else
            //{
            //    this.richTextBoxOutput.AppendText(e.EmoteText + Environment.NewLine);
            //}
        }

        private void Bot_Connected(object sender, EventArgs e)
        {
            bot.JoinChannel("kartargo");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string token = settings.Token;
            if (string.IsNullOrEmpty(token))
            {
                token = acquireToken.GetToken();
                settings.Token = token;
            }
            bot.Connect("tarosmolos", "***REMOVED***");

            //MessageFormatRtfManager messageFormat = new MessageFormatRtfManager();
            //CrossCutting.DataClasses.Message message = new CrossCutting.DataClasses.Message();
            //message.PlainText = "Kappa";
            //byte[] data = File.ReadAllBytes(@"c:\temp\Kappa.0");
            //Bitmap bitmap;
            //using (MemoryStream stream = new MemoryStream(data))
            //{
            //    bitmap = new Bitmap(stream);
            //}
            //message.EmotePositionList.Add(new CrossCutting.DataClasses.EmotePosition() { StartIndex = 0, EndIndex = 4, Id = "1", Emote = new CrossCutting.DataClasses.Emote() { Id = "1", Name = "Kappa", Data = data, Bitmap = bitmap } });
            //messageFormat.Format(message);

            //richTextBoxOutput.Rtf = message.EmoteText;
            ////using (IClient client = new Client())
            //{
            //    client.Connect("irc.chat.twitch.tv", 6697);
            //    client.Login("tarosmolos", "***REMOVED***");
            //    //Channel channel = client.GetChannel("raupling");
            //}
        }

        private class Logger : ILogger 
        {
            private readonly Form1 form1;

            public Logger(Form1 form1)
            {
                this.form1 = form1;
            }

            public void Log(string line)
            {
                if (this.form1.InvokeRequired)
                {
                    this.form1.textBoxLog.Invoke(new Action(() => this.form1.textBoxLog.Text += line + Environment.NewLine));
                }
                else
                {
                    this.form1.textBoxLog.Text += line + Environment.NewLine;
                }
            }
        }
    }
}
