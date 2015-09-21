﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Hardware
{
    public interface IScanner
    {
        IScannerListener Listener
        {
            set;
            get;
        }

        bool Enabled
        {
            get;
            set;
        }
    }
}
