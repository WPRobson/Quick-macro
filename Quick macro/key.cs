﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick_macro
{
    class key
    {

        public int keycode;
        public bool keyDown;

        public key(int inKeyCode, bool inKeydown)
        {
            keycode = inKeyCode;
            keyDown = inKeydown;
        }
    }
}
