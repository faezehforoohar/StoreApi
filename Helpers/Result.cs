using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Helpers
{
    public class Result<T>
    {
        public T data { get; set; }
        public bool result { get; set; }
        public string message { get; set; }
    }
}
