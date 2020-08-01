using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Settings.Contracts
{
    public interface ISettings
    {
        string Token { get; set; }
    }
}
