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
        public async Task<ActionResult<List<ReturnEmployeeDTO>>> GetAllEmployees()
        {
            return Ok(await _ISourceService.GetAllEmployees());
        }
        [HttpGet]
        [Route("getAllSources")]
        public async Task<ActionResult<List<Source>>> GetAllSources()
        {
            return Ok(await _ISourceService.GetAllSources());
        }

        [HttpPost]
        [Route("addEmployee")]
        public async Task<ActionResult<List<Employee>>> AddEmployee(int employee_id, string password, string name)
        {
            return Ok(await _ISourceService.AddEmployee(employee_id, password, name));
        }

        [HttpPost]
        [Route("addSource")]
        public async Task<ActionResult<List<Source>>> AddSource(int id, int sg_id)
        {
            return Ok(await _ISourceService.AddSource(id, sg_id));
        }

        [HttpDelete]
        [Route("removeSource")]
        public async Task<ActionResult<List<Source>>> RemoveSource(int source_id)
        {
            return Ok(await _ISourceService.RemoveSource(source_id));
        }

        [HttpGet]
        [Route("getAllSourceGroups")]
        public async Task<ActionResult<List<SourceGroup>>> GetAllSourceGroups()
        {
            return Ok(await _ISourceService.GetAllSourceGroups());
        }

        [HttpPost]
        [Route("addSourceGroup")]
        public async Task<ActionResult<List<SourceGroup>>> AddSourceGroup(int sourceGroup_id, string name, int cap)
        {
            return Ok(await _ISourceService.AddSourceGroup(sourceGroup_id, name, cap));
        }

        [HttpPost]
        [Route("addRelationship")]
        public async Task<ActionResult<Employee>> AddRelationship(int employee_id, int source_id)
        {
            return Ok(await _ISourceService.AddRelationship(employee_id, source_id));
        }
        [HttpGet]
        [Route("getSourcesOfEmployee")]
        public async Task<ActionResult<List<Source>>> GetSourcesOfEmployee(int employee_id)
        {
            return Ok(await _ISourceService.GetSourcesOfEmployee(employee_id));
        }
        [HttpGet]
        [Route("getOwnersOfSource")]
        public async Task<ActionResult<List<Employee>>> GetOwnersOfSource(int source_id)
        {
            return Ok(await _ISourceService.GetOwnersOfSource(source_id));
        }
        [HttpDelete]
        [Route("removeEmployee")]
        public async Task<ActionResult<List<Employee>>> RemoveEmployee(int employee_id)
        {
            return Ok(await _ISourceService.RemoveEmployee(employee_id));
        }
        [HttpDelete]
        [Route("removeSourceGroup")]
        public async Task<ActionResult<List<SourceGroup>>> RemoveSourceGroup(int source_group_id)
        {
            return Ok(await _ISourceService.RemoveSourceGroup(source_group_id));
        }
        [HttpGet]
        [Route("getEmployeeById")]
        public async Task<ActionResult<ReturnEmployeeDTO>> GetEmployeeById(int employee_id)
        {
            return Ok(await _ISourceService.GetEmployeeById(employee_id));
        }
        [HttpDelete]
        [Route("removeRelationship")]
        public async Task<ActionResult<Employee>> RemoveRelationship(int employee_id, int source_id)
        {
            return Ok(await _ISourceService.RemoveRelationship(employee_id, source_id));
        }
        [HttpPost]
        [Route("addAdministration")]
        public async Task<ActionResult<Employee>> AddAdministration(int employee_id, int sg_id)
        {
            return Ok(await _ISourceService.AddAdministration(employee_id, sg_id));
        }
        [HttpGet]
        [Route("getSourcesByGroup")]
        public async Task<ActionResult<List<ReturnSourceDTO>>> GetSourcesByGroup(int group_id)
        {
            return Ok(await _ISourceService.GetSourcesByGroup(group_id));
        }
        [HttpPut]
        [Route("updateEmployee")]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(UpdateEmployeeDTO request)
        {
            return Ok(await _ISourceService.UpdateEmployee(request));
        }
        [HttpPut]
        [Route("updateSourceGroup")]
        public async Task<ActionResult<List<SourceGroup>>> UpdateSourceGroup(int sg_id, int emp_id)
        {
            return Ok(await _ISourceService.UpdateSourceGroup(sg_id, emp_id));
        }
        [HttpPut]
        [Route("updateRelationship")]
        public async Task<ActionResult<Employee>> UpdateRelationship(int emp_id, int source_id, int new_source_id)
        {
            return Ok(await _ISourceService.UpdateRelationship(emp_id, source_id, new_source_id));
        }

    }
}