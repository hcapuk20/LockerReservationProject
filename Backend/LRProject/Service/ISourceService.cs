using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;

namespace LRProject.Service
{
    public interface ISourceService
    {
        Task<Response<List<Source>>> GetAllSources();
        Task<Response<List<Source>>> AddSource(int source_id, int source_group_id);
        Task<Response<List<Employee>>> AddEmployee(int employee_id, string password, string name);
        Task<Response<List<ReturnEmployeeDTO>>> GetAllEmployees();
        Task<Response<ReturnEmployeeDTO>> GetEmployeeById(int employee_id);
        Task<Response<List<Source>>> RemoveSource(int source_id);
        Task<Response<List<Employee>>> RemoveEmployee(int employee_id);
        Task<Response<List<SourceGroup>>> GetAllSourceGroups();
        Task<Response<List<SourceGroup>>> AddSourceGroup(int SG_id, string name, int cap);
        Task<Response<Employee>> AddRelationship(int employee_id, int source_id);
        Task<Response<List<Source>>> GetSourcesOfEmployee(int employee_id);
        Task<Response<List<EmpDTOIdName>>> GetOwnersOfSource(int source_id);
        Task<Response<List<SourceGroup>>> RemoveSourceGroup(int sg_id);
        Task<Response<Employee>> RemoveRelationship(int employee_id, int source_id);
        Task<Response<Employee>> AddAdministration(int employee_id, int sg_id);
        Task<Response<List<GetSourcesByGroupDTO>>> GetSourcesByGroup(int source_id);
        Task<Response<List<Employee>>> UpdateEmployee(UpdateEmployeeDTO request);
        Task<Response<List<SourceGroup>>> UpdateSourceGroup(int sg_id, int former_emp_id, int employee_id);
        Task<Response<Employee>> UpdateRelationship(int employee_id, int source_id, int new_source_id);
    }
}