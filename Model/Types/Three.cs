﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroLab1.Model.Types
{
    public class Three : Type
    {
        public Three()
        {
            Image = new byte[] { 1, 1, 1, 1, 1,
                                 0, 0, 0, 0, 1,
                                 0, 0, 0, 0, 1,
                                 1, 1, 1, 1, 1,
                                 0, 0, 0, 0, 1,
                                 0, 0, 0, 0, 1,
                                 1, 1, 1, 1, 1 };
        }
    }
}
