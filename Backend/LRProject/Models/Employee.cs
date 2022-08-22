using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class Employee : PartiallyAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<EmployeeSource> EmployeeSources { get; set; } = new List<EmployeeSource>();
        // source groups that can be manipulated by this user (Administration)
        public List<EmployeeSourceGroup> EmployeeSourceGroups { get; set; } = new List<EmployeeSourceGroup>();

    }
}