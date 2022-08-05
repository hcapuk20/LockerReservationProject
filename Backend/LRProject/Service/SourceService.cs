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
            //var SG = await _context.SourceGroups.FindAsync(source_group_id);
            _context.Sources.Add(newSource);
            //SG.Sources.Add(newSource);
            await _context.SaveChangesAsync();
            return await _context.Sources.ToListAsync();
        }

        public async Task<List<SourceGroup>> GetAllSourceGroups()
        {
            return await _context.SourceGroups.Include(s => s.Sources).ToListAsync();
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
            return await _context.Employees.Include(c => c.Sources).Include(c => c.SourceGroups).ToListAsync();
        }

        public async Task<Employee> AddRelationship(int employee_id, int source_id)
        {
            var employee = await _context.Employees.FindAsync(employee_id);
            var source = await _context.Sources.FindAsync(source_id);
            employee.Sources.Add(source);

            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<List<Source>> GetSourcesOfEmployee(int employee_id)
        {
            var sources = _context.Employees.Where(x => x.Id == employee_id).SelectMany(e => e.Sources);
            return sources.ToList();
        }

        public async Task<List<Employee>> GetOwnersOfSource(int source_id)
        {
            var employees = _context.Sources.Where(s => s.Id == source_id).SelectMany(x => x.Employees);
            return employees.ToList();
        }

        public async Task<List<Employee>> RemoveEmployee(int employee_id)
        {
            var employee = _context.Employees.First(s => s.Id == employee_id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<SourceGroup>> RemoveSourceGroup(int sg_id)
        {
            var sg = _context.SourceGroups.First(s => s.Id == sg_id);
            _context.SourceGroups.Remove(sg);
            await _context.SaveChangesAsync();
            return await _context.SourceGroups.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int employee_id)
        {
            var employee = await _context.Employees.FindAsync(employee_id);
            return employee;
        }

        public async Task<Employee> RemoveRelationship(int employee_id, int source_id)
        {
            var employee = _context.Employees.Include(e => e.Sources).SingleOrDefault(e => e.Id == employee_id);


            if (employee != null)
            {
                foreach (var source in employee.Sources.Where(s => s.Id == source_id).ToList())
                {
                    employee.Sources.Remove(source);
                }
            }

            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> AddAdministration(int employee_id, int sg_id)
        {
            var employee = await _context.Employees.FindAsync(employee_id);
            var sg = await _context.SourceGroups.FindAsync(sg_id);
            employee.SourceGroups.Add(sg);

            await _context.SaveChangesAsync();
            return employee;
        }
    }
}