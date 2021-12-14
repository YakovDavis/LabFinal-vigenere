using System;
using System.Collections.Generic;
using System.Text;

namespace FinalLab
{
    public class KeyHelper
    {
        private string key;

        public KeyHelper(string key)
        {
            this.key = key;
        }

        public char CharAt(int i)
        {
            i = i % key.Length;
            return key[i];
        }
    }
}
