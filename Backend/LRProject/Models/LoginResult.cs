using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class LoginResult
    {
        public string Token { get; set; }
        public ReturnEmployeeDTO Employee { get; set; }
    }
}