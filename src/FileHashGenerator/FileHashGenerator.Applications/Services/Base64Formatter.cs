﻿using System;

namespace Waf.FileHashGenerator.Applications.Services
{
    internal class Base64Formatter : IHashFormatter
    {
        public bool IsCaseSensitive { get { return true; } }
        
        public string FormatHash(byte[] hash)
        {
            return Convert.ToBase64String(hash);
        }
    }
}