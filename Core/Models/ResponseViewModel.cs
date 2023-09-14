using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class ResponseViewModel
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
