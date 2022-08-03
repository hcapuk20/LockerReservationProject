using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LRProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<SourceGroup> SourceGroups { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}