using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;

namespace LRProject.Service
{
    public interface ISourceService
    {
        Task<List<Source>> GetAllSources();
        Task<List<Source>> AddSource(int source_id, int source_group_id);
        Task<List<Employee>> AddEmployee(int employee_id, string name);
        Task<List<Employee>> GetAllEmployees();
        Task<List<Source>> RemoveSource(int source_id);
        Task<List<SourceGroup>> GetAllSourceGroups();
        Task<List<SourceGroup>> AddSourceGroup(int SG_id, string name, int cap);
        Task<Employee> AddRelationship(int employee_id, int source_id);

    }
}