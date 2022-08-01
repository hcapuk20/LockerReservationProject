using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public class Source
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Source_Id { get; set; }
        public SourceGroup SourceGroup { get; set; }
        public int SourceGroupId { get; set; }

    }
}