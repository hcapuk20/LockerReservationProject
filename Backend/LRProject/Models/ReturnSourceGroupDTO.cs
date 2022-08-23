using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class ReturnSourceGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public List<ReturnSourceDTO> Sources { get; set; }
    }
}