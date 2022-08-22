using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;

namespace LRProject.Service
{
    public interface ISourceService
    {
        Task<Response<int>> Login(int id);
        Task<Response<ReturnEmployeeDTO>> AddEmployee(int employee_id, string password, string role, string name);
        Task<Response<List<ReturnEmployeeDTO>>> GetAllEmployees();
        Task<Response<ReturnEmployeeDTO>> GetEmployeeById(int employee_id);
        Task<Response<List<Source>>> GetAllSources();
        Task<Response<List<Source>>> AddSource(int source_id, int source_group_id);
        Task<Response<List<SourceGroup>>> GetAllSourceGroups();
        Task<Response<List<SourceGroup>>> AddSourceGroup(int SG_id, string name, int cap);
        Task<Response<List<Source>>> RemoveSource(int source_id);
        Task<Response<List<ReturnEmployeeDTO>>> RemoveEmployee(int employee_id);
        Task<Response<ReturnEmployeeDTO>> AddRelationship(int employee_id, int source_id);
        Task<Response<List<ReturnSourceDTO>>> GetSourcesOfEmployee(int employee_id);
        Task<Response<List<EmpDTOIdName>>> GetOwnersOfSource(int source_id);
        Task<Response<List<SourceGroup>>> RemoveSourceGroup(int sg_id);
        Task<Response<ReturnEmployeeDTO>> RemoveRelationship(int employee_id, int source_id);
        Task<Response<ReturnEmployeeDTO>> AddAdministration(int employee_id, int sg_id);
        Task<Response<List<GetSourcesByGroupDTO>>> GetSourcesByGroup(int source_id);
        Task<Response<ReturnEmployeeDTO>> UpdateEmployee(UpdateEmployeeDTO request);
        Task<Response<List<SourceGroup>>> UpdateSourceGroup(int sg_id, int former_emp_id, int employee_id);
        Task<Response<ReturnEmployeeDTO>> UpdateRelationship(int employee_id, int source_id, int new_source_id);
        // Task<Response<ReturnEmployeeDTO>> AddAutoRelationship(int employee_id, int sg_id);
    }
}