using CrossCutting.Core.Contract.DependencyInjection;
using CrossCutting.Logging;
using Logic.Authorization;
using Logic.Badges;
using Logic.Chat;
using Logic.Emotes;
using Logic.HttpClient;
using Logic.ImageManagement;
using Logic.Settings;
using Logic.Twitch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrations.Mappings
{
    public class KernelInitializer : IKernelInitializer
    {
        public void Initialize(IKernel kernel)
        {
            kernel.RegisterComponent<AuthorizationActivator>();
            kernel.RegisterComponent<BadgesActivator>();
            kernel.RegisterComponent<ChatActivator>();
            kernel.RegisterComponent<EmotesActivator>();
            kernel.RegisterComponent<HttpClientActivator>();
            kernel.RegisterComponent<ImageManagementActivator>();
            kernel.RegisterComponent<LoggingActivator>();
            kernel.RegisterComponent<SettingsActivator>();
            kernel.RegisterComponent<TwitchActivator>();
        }
    }
}
