using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRProject.Models
{
    public abstract class FullyAuditable
    {
        public int CreatedByUser { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int UpdatedByUser { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
        public int DeletedByUser { get; set; }
        public DateTimeOffset DateDeleted { get; set; }
        // if status is 1, item is present.
        public int Status { get; set; }

    }
}