using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class SourceGroup : PartiallyAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        // associated sources
        public List<Source> Sources { get; set; } = new List<Source>();
        // admins
        public List<EmployeeSourceGroup> EmployeeSourceGroups { get; set; } = new List<EmployeeSourceGroup>();
    }
}