﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.ViewModels
{
    public class ResponseModel
    {
    }

    public class Rootobject<T>
    {
        public int TotalRow { get; set; }
        public List<T> Items { get; set; }
    }
}