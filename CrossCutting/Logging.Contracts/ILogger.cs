﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Logging.Contracts
{
    public interface ILogger
    {
        void Log(string line);
    }
}
