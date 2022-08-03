using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Data;
using LRProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LRProject.Service
{
    public class SourceService : ISourceService
    {
        private readonly DataContext _context;
        public SourceService(DataContext context)
        {
            _context = context;
        }




        public async Task<List<Source>> AddSource(int source_id, int source_group_id)
        {

            var newSource = new Source() { Id = source_id, SourceGroupId = source_group_id };
            _context.Sources.Add(newSource);
            await _context.SaveChangesAsync();
            return await _context.Sources.ToListAsync();
        }




        public async Task<List<SourceGroup>> GetAllSourceGroups()
        {
            return await _context.SourceGroups.ToListAsync();
        }

        public async Task<List<Source>> GetAllSources()
        {
            return await _context.Sources.ToListAsync();
        }


        public async Task<List<Source>> RemoveSource(int source_id)
        {
            var source = _context.Sources.First(s => s.Id == source_id);
            _context.Sources.Remove(source);
            await _context.SaveChangesAsync();
            return await _context.Sources.ToListAsync();
        }

        public async Task<List<SourceGroup>> AddSourceGroup(int SG_id, string name, int cap)
        {
            SourceGroup newSG = new SourceGroup() { Id = SG_id, Name = name, Capacity = cap };
            _context.SourceGroups.Add(newSG);
            await _context.SaveChangesAsync();
            return await _context.SourceGroups.ToListAsync();
        }

        public async Task<List<Employee>> AddEmployee(int employee_id, string name)
        {
            Employee newEmployee = new Employee() { Id = employee_id, Name = name };
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.Include(c => c.Sources).ToListAsync();
        }

        public async Task<Employee> AddRelationship(int employee_id, int source_id)
        {
            var employee = await _context.Employees.FindAsync(employee_id);
            var source = await _context.Sources.FindAsync(source_id);
            employee.Sources.Add(source);


            await _context.SaveChangesAsync();

            return employee;

        }


    }
}