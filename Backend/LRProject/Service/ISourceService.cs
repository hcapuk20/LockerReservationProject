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
        Task<List<Employee>> AddEmployee(int employee_id, string password, string name);
        Task<List<ReturnEmployeeDTO>> GetAllEmployees();
        Task<ReturnEmployeeDTO> GetEmployeeById(int employee_id);
        Task<List<Source>> RemoveSource(int source_id);
        Task<List<Employee>> RemoveEmployee(int employee_id);
        Task<List<SourceGroup>> GetAllSourceGroups();
        Task<List<SourceGroup>> AddSourceGroup(int SG_id, string name, int cap);
        Task<Employee> AddRelationship(int employee_id, int source_id);
        Task<List<Source>> GetSourcesOfEmployee(int employee_id);
        Task<List<Employee>> GetOwnersOfSource(int source_id);
        Task<List<SourceGroup>> RemoveSourceGroup(int sg_id);
        Task<Employee> RemoveRelationship(int employee_id, int source_id);
        Task<Employee> AddAdministration(int employee_id, int sg_id);
        Task<List<ReturnSourceDTO>> GetSourcesByGroup(int source_id);

    }
}