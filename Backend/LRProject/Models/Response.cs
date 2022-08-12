using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}