﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Logic.ImageManagement.Contracts
{
    public interface IBitmapFunctions
    {
        BitmapImage ToBitmapImage(Bitmap bitmap);
    }
}
