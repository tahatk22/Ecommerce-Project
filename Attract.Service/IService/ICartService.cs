﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ICartService
    {
        Task<int> GetCartByUser(string userId);
    }
}
