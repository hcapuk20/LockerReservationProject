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

        private List<Source> _sourceList = new List<Source>{
            new Source(){Id = 1},
            new Source(){Id = 2},
            new Source(){Id = 3},
            new Source(){Id = 4},
        };
        private List<Employee> _employeeList = new List<Employee>{
            new Employee(){Employee_Id = 1, Name = "Hakan"},
            new Employee(){Employee_Id = 2, Name = "Sevval"},
            new Employee(){Employee_Id = 3, Name = "Mert"},
            new Employee(){Employee_Id = 4, Name = "Gaye"},
        };
        private List<Owns> _ownsList = new List<Owns>{
            new Owns(){Source_Id = 1, Employee_Id = 4},
            new Owns(){Source_Id = 2, Employee_Id = 1},
            new Owns(){Source_Id = 3, Employee_Id = 2},
            new Owns(){Source_Id = 4, Employee_Id = 3},
        };

        public async Task<List<Source>> AddSource(int source_id, int source_group_id)
        {

            var newSource = new Source() { Id = source_id, SourceGroupId = source_group_id };
            _context.Sources.Add(newSource);
            await _context.SaveChangesAsync();
            return await _context.Sources.ToListAsync();
        }



        public async Task<List<Owns>> GetAllRelationships()
        {
            return _ownsList;
        }

        public async Task<List<SourceGroup>> GetAllSourceGroups()
        {
            return await _context.SourceGroups.ToListAsync();
        }

        public async Task<List<Source>> GetAllSources()
        {
            return await _context.Sources.ToListAsync();
        }

        public async Task<List<Owns>> ManualAddRelationship(int source_id, int employee_id)
        {
            _ownsList.Add(new Owns { Source_Id = source_id, Employee_Id = employee_id });
            return _ownsList;
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
    }
}