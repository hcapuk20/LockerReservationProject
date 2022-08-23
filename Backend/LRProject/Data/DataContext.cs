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
        private readonly IUserService _userService;
        public DataContext(DbContextOptions<DataContext> options, IUserService userService) : base(options)
        {
            _userService = userService;
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<SourceGroup> SourceGroups { get; set; }
        public DbSet<EmployeeSource> EmployeeSources { get; set; }
        public DbSet<EmployeeSourceGroup> EmployeeSourcesGroups { get; set; }

        // configuring and customizing joint tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EmployeeSources table
            modelBuilder.Entity<EmployeeSource>()
                    .HasKey(es => new { es.EmployeeId, es.SourceId });
            modelBuilder.Entity<EmployeeSource>()
                    .HasOne(e => e.Employee)
                    .WithMany(es => es.EmployeeSources)
                    .HasForeignKey(e => e.EmployeeId);
            modelBuilder.Entity<EmployeeSource>()
                    .HasOne(e => e.Source)
                    .WithMany(es => es.EmployeeSources)
                    .HasForeignKey(s => s.SourceId);

            // EmployeeSourceGroups table
            modelBuilder.Entity<EmployeeSourceGroup>()
                    .HasKey(esg => new { esg.EmployeeId, esg.SourceGroupId });
            modelBuilder.Entity<EmployeeSourceGroup>()
                    .HasOne(e => e.Employee)
                    .WithMany(esg => esg.EmployeeSourceGroups)
                    .HasForeignKey(e => e.EmployeeId);
            modelBuilder.Entity<EmployeeSourceGroup>()
                    .HasOne(e => e.SourceGroup)
                    .WithMany(esg => esg.EmployeeSourceGroups)
                    .HasForeignKey(sg => sg.SourceGroupId);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                                   .Where(x => x.State == EntityState.Added)
                                   .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                var fullyAuditableEntity = insertedEntry as FullyAuditable;
                var partiallyAuditableEntity = insertedEntry as PartiallyAuditable;
                //If the inserted object is an Auditable. 
                if (fullyAuditableEntity != null)
                {
                    fullyAuditableEntity.DateCreated = DateTimeOffset.UtcNow;
                    fullyAuditableEntity.CreatedByUser = _userService.GetMyId();
                }
                if (partiallyAuditableEntity != null)
                {
                    partiallyAuditableEntity.DateCreated = DateTimeOffset.UtcNow;
                }
            }

            var modifiedEntries = this.ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                //If the inserted object is an Auditable. 
                var fullyAuditableEntity = modifiedEntry as FullyAuditable;
                var partiallyAuditableEntity = modifiedEntry as PartiallyAuditable;
                if (fullyAuditableEntity != null)
                {
                    if (fullyAuditableEntity.Status == 0)
                    {
                        fullyAuditableEntity.DateDeleted = DateTimeOffset.UtcNow;
                        fullyAuditableEntity.DeletedByUser = _userService.GetMyId();
                    }
                    else
                    {
                        fullyAuditableEntity.DateUpdated = DateTimeOffset.UtcNow;
                        fullyAuditableEntity.UpdatedByUser = _userService.GetMyId();
                    }
                }
                if (partiallyAuditableEntity != null)
                {
                    if (partiallyAuditableEntity.Status == 0)
                    {
                        partiallyAuditableEntity.DateDeleted = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        partiallyAuditableEntity.DateUpdated = DateTimeOffset.UtcNow;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}