using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class ReturnEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ReturnSourceDTO> Sources { get; set; } = new List<ReturnSourceDTO>();
        public virtual List<EmployeeSourceGroup> SourceGroups { get; set; } = new List<EmployeeSourceGroup>();
    }
}