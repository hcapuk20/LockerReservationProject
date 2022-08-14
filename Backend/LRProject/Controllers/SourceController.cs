using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;
using LRProject.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LRProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SourceController : ControllerBase
    {
        private readonly ISourceService _ISourceService;

        public SourceController(ISourceService sourceService)
        {
            _ISourceService = sourceService;

        }

        [HttpGet]
        [Route("getAllEmployees")]
        public async Task<ActionResult<Response<List<ReturnEmployeeDTO>>>> GetAllEmployees()
        {
            return Ok(await _ISourceService.GetAllEmployees());
        }
        [HttpGet]
        [Route("getAllSources")]
        public async Task<ActionResult<Response<List<Source>>>> GetAllSources()
        {
            return Ok(await _ISourceService.GetAllSources());
        }

        [HttpPost]
        [Route("addEmployee")]
        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> AddEmployee(int employee_id, string name, string password)
        {
            return Ok(await _ISourceService.AddEmployee(employee_id, name, password));
        }

        [HttpPost]
        [Route("addSource")]
        public async Task<ActionResult<Response<List<Source>>>> AddSource(int id, int sg_id)
        {
            return Ok(await _ISourceService.AddSource(id, sg_id));
        }

        [HttpDelete]
        [Route("removeSource")]
        public async Task<ActionResult<Response<List<Source>>>> RemoveSource(int source_id)
        {
            return Ok(await _ISourceService.RemoveSource(source_id));
        }

        [HttpGet]
        [Route("getAllSourceGroups")]
        public async Task<ActionResult<Response<List<SourceGroup>>>> GetAllSourceGroups()
        {
            return Ok(await _ISourceService.GetAllSourceGroups());
        }

        [HttpPost]
        [Route("addSourceGroup")]
        public async Task<ActionResult<Response<List<SourceGroup>>>> AddSourceGroup(int sourceGroup_id, string name, int cap)
        {
            return Ok(await _ISourceService.AddSourceGroup(sourceGroup_id, name, cap));
        }

        [HttpPost]
        [Route("addRelationship")]
        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> AddRelationship(int employee_id, int source_id)
        {
            return Ok(await _ISourceService.AddRelationship(employee_id, source_id));
        }
        [HttpGet]
        [Route("getSourcesOfEmployee")]
        public async Task<ActionResult<Response<List<Source>>>> GetSourcesOfEmployee(int employee_id)
        {
            return Ok(await _ISourceService.GetSourcesOfEmployee(employee_id));
        }
        [HttpGet]
        [Route("getOwnersOfSource")]
        public async Task<ActionResult<Response<List<EmpDTOIdName>>>> GetOwnersOfSource(int source_id)
        {
            return Ok(await _ISourceService.GetOwnersOfSource(source_id));
        }
        [HttpDelete]
        [Route("removeEmployee")]
        public async Task<ActionResult<Response<List<ReturnEmployeeDTO>>>> RemoveEmployee(int employee_id)
        {
            return Ok(await _ISourceService.RemoveEmployee(employee_id));
        }
        [HttpDelete]
        [Route("removeSourceGroup")]
        public async Task<ActionResult<Response<List<SourceGroup>>>> RemoveSourceGroup(int source_group_id)
        {
            return Ok(await _ISourceService.RemoveSourceGroup(source_group_id));
        }
        [HttpGet]
        [Route("getEmployeeById")]
        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> GetEmployeeById(int employee_id)
        {
            return Ok(await _ISourceService.GetEmployeeById(employee_id));
        }
        [HttpDelete]
        [Route("removeRelationship")]
        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> RemoveRelationship(int employee_id, int source_id)
        {
            return Ok(await _ISourceService.RemoveRelationship(employee_id, source_id));
        }
        [HttpPost]
        [Route("addAdministration")]
        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> AddAdministration(int employee_id, int sg_id)
        {
            return Ok(await _ISourceService.AddAdministration(employee_id, sg_id));
        }
        [HttpGet]
        [Route("getSourcesByGroup")]
        public async Task<ActionResult<Response<List<GetSourcesByGroupDTO>>>> GetSourcesByGroup(int group_id)
        {
            return Ok(await _ISourceService.GetSourcesByGroup(group_id));
        }
        [HttpPut]
        [Route("updateEmployee")]
        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> UpdateEmployee(UpdateEmployeeDTO request)
        {
            return Ok(await _ISourceService.UpdateEmployee(request));
        }
        [HttpPut]
        [Route("updateSourceGroup")]
        public async Task<ActionResult<Response<List<SourceGroup>>>> UpdateSourceGroup(int sg_id, int former_id, int emp_id)
        {
            return Ok(await _ISourceService.UpdateSourceGroup(sg_id, former_id, emp_id));
        }
        [HttpPut]
        [Route("updateRelationship")]
        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> UpdateRelationship(int emp_id, int source_id, int new_source_id)
        {
            return Ok(await _ISourceService.UpdateRelationship(emp_id, source_id, new_source_id));
        }
        [HttpPost]
        [Route("addAutoRelationship")]
        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> AddAutoRelationship(int emp_id, int sg_id)
        {
            return Ok(await _ISourceService.AddAutoRelationship(emp_id, sg_id));
        }

    }
}