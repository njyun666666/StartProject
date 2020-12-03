﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.ViewModels
{
    public class BaseViewModel
    {
        public int code { get; set; }
        public string message { get; set; }
    }
    public class BaseViewModel<T> : BaseViewModel
    {
        public List<T> data { get; set; }
    }
}
