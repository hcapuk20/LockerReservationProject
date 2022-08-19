using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class EmployeeSource : FullyAuditable
    {
        public int EmployeeId { get; set; }
        public int SourceId { get; set; }
        public Employee Employee { get; set; }
        public Source Source { get; set; }
    }
}