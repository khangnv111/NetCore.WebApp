using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.ViewModels
{
    public class ResponseModel
    {
    }

    public class RootObject<T>
    {
        public int TotalRow { get; set; }
        public List<T> Items { get; set; }
        public T link { get; set; }
    }
}
