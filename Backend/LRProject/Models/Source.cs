using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class Source
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public SourceGroup SourceGroup { get; set; }
        public int SourceGroupId { get; set; }
        [JsonIgnore]
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}