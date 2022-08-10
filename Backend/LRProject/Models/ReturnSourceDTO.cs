using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class ReturnSourceDTO
    {
        public int Id { get; set; }
        public int SourceGroupId { get; set; }
        public List<ReturnEmployeeDTO> Employees { get; set; } = new List<ReturnEmployeeDTO>();
    }
}