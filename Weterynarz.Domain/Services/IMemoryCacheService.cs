﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Basic.Const;

namespace Weterynarz.Domain.Services
{
    public interface IMemoryCacheService
    {
        void UpdateContent(string contentKey, string contentValue);
        Dictionary<string, string> GetContentToDict(string cacheKey);
        void RemoveContentDict(string cacheKey);
    }
}