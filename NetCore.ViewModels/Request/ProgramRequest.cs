using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.ViewModels.Request
{
    public class ProgramRequest
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int IsSetted { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
