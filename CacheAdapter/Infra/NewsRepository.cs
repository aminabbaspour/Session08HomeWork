﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheAdapter.Infra
{
    public class NewsRepository
    {
        public int GetNewCount()
        {
            System.Threading.Thread.Sleep(4000);
            return 10;
        }

    }
}
