﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WholesomeMVC
{
    public class Account
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string passwordhash { get; set; }
        public int role { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }
}