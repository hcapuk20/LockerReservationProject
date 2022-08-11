using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class GetSourcesByGroupDTO
    {
        public int Id { get; set; }
        public int SourceGroupId { get; set; }
        public List<EmpDTOIdName> Employees { get; set; } = new List<EmpDTOIdName>();
        public int Space { get; set; }
    }
}