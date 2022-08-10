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
        public List<Source> Sources { get; set; } = new List<Source>();
        public virtual List<SourceGroup> SourceGroups { get; set; } = new List<SourceGroup>();
    }
}