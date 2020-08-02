using CrossCutting.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Logging
{
    internal class Logger : ILogger
    {
        public void Log(string line)
        {
            System.Console.WriteLine(line);
        }
    }
}
