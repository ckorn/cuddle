using Logic.Settings.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Settings
{
    internal class Settings : ISettings
    {
        public string Token { get => Properties.Settings.Default.TOKEN; set { Properties.Settings.Default.TOKEN = value; Properties.Settings.Default.Save(); } }

        public Settings()
        {
            if (Properties.Settings.Default.UPDATE)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UPDATE = false;
            }
        }
    }
}
