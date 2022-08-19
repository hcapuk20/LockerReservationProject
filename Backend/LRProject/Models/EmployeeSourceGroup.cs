using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class EmployeeSourceGroup : FullyAuditable
    {
        public int EmployeeId { get; set; }
        public int SourceGroupId { get; set; }
        public Employee Employee { get; set; }
        public SourceGroup SourceGroup { get; set; }
    }
}