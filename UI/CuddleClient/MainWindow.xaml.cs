using CrossCutting.Core.Bootstrapping;
using CrossCutting.Core.Contract.Bootstrapping;
using CrossCutting.Core.Contract.DependencyInjection;
using CrossCutting.Core.Contract.DependencyInjection.DataClasses;
using CrossCutting.Core.NinjectAdapter;
using CrossCutting.Logging;
using CrossCutting.Logging.Contracts;
using Logic.Chat.Contracts;
using Registrations.Mappings;
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

namespace UI.CuddleClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBot bot;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Bot_MessageReceived(object sender, CrossCutting.DataClasses.Message e)
        {
            void doIt()
            {
                this.textEditor.AppendText($"{e.DisplayMessage}{Environment.NewLine}");
                this.textEditor.ScrollToEnd();
            }
            this.textEditor.AddMessage(e);
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
            bot.JoinChannel("nashilein");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string token = settings.Token;
            //if (string.IsNullOrEmpty(token))
            //{
            //    token = acquireToken.GetToken();
            //    settings.Token = token;
            //}

            void boot()
            {
                // ----- App-Init
                KernelContainer kernelContainer = new KernelContainer();
                IKernel kernel = kernelContainer.Kernel;

                //Register CoCo.Core
                //Add Bootstrapper
                kernel.Register<IBootstrapper, Bootstrapper>(RegisterScope.Unique);

                //Register components
                new KernelInitializer().Initialize(kernel);

                //Activate components
                IBootstrapper bootstrapper = kernel.Get<IBootstrapper>();
                bootstrapper.ActivatingAll();
                bootstrapper.ActivatedAll();
                bootstrapper.RegisterAllMappings(kernel);

                this.bot = kernel.Get<IBot>();

                bot.Connected += Bot_Connected;
                bot.MessageReceived += this.Bot_MessageReceived;

                bot.Connect("tarosmolos");
            };
            Task.Factory.StartNew(() => boot());

            ////using (IClient client = new Client())
            //{
            //    client.Connect("irc.chat.twitch.tv", 6697);
            //    client.Login("tarosmolos", token);
            //    //Channel channel = client.GetChannel("raupling");
            //}
        }
    }
}
