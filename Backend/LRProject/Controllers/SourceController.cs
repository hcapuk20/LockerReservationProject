using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;
using LRProject.Service;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "DbManager")]


        public async Task<ActionResult<Response<List<ReturnEmployeeDTO>>>> GetAllEmployees()
        {
            return Ok(await _ISourceService.GetAllEmployees());
        }
        [HttpGet]
        [Route("getEmployeeById")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> GetEmployeeById(int employee_id)
        {
            return Ok(await _ISourceService.GetEmployeeById(employee_id));
        }
        [HttpPost]
        [Route("addEmployee")]

        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> AddEmployee(int employee_id, string name, string password, string role, int[] sg_ids)
        {
            return Ok(await _ISourceService.AddEmployee(employee_id, name, password, role, sg_ids));
        }
        [HttpDelete]
        [Route("removeEmployee")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<ReturnEmployeeDTO>>>> RemoveEmployee(int employee_id)
        {
            return Ok(await _ISourceService.RemoveEmployee(employee_id));
        }
        [HttpGet]
        [Route("getAllSources")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<ReturnSourceDTO>>>> GetAllSources()
        {
            return Ok(await _ISourceService.GetAllSources());
        }
        [HttpGet]
        [Route("getSourcesByGroup")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<GetSourcesByGroupDTO>>>> GetSourcesByGroup(int group_id)
        {
            return Ok(await _ISourceService.GetSourcesByGroup(group_id));
        }
        [HttpPost]
        [Route("addSource")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<ReturnSourceDTO>>>> AddSource(int id, int sg_id)
        {
            return Ok(await _ISourceService.AddSource(id, sg_id));
        }
        [HttpDelete]
        [Route("removeSource")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<ReturnSourceDTO>>>> RemoveSource(int source_id)
        {
            return Ok(await _ISourceService.RemoveSource(source_id));
        }
        [HttpGet]
        [Route("getAllSourceGroups")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<ReturnSourceGroupDTO>>>> GetAllSourceGroups()
        {
            return Ok(await _ISourceService.GetAllSourceGroups());
        }
        [HttpPost]
        [Route("addSourceGroup")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<ReturnSourceGroupDTO>>>> AddSourceGroup(int sourceGroup_id, string name, int cap)
        {
            return Ok(await _ISourceService.AddSourceGroup(sourceGroup_id, name, cap));
        }
        [HttpDelete]
        [Route("removeSourceGroup")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<ReturnSourceGroupDTO>>>> RemoveSourceGroup(int source_group_id)
        {
            return Ok(await _ISourceService.RemoveSourceGroup(source_group_id));
        }


        [HttpPost]
        [Route("addRelationship")]


        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> AddRelationship(int employee_id, int source_id)
        {
            return Ok(await _ISourceService.AddRelationship(employee_id, source_id));
        }
        [HttpGet]
        [Route("getSourcesOfEmployee")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<Source>>>> GetSourcesOfEmployee(int employee_id)
        {
            return Ok(await _ISourceService.GetSourcesOfEmployee(employee_id));
        }
        [HttpGet]
        [Route("getOwnersOfSource")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<List<EmpDTOIdName>>>> GetOwnersOfSource(int source_id)
        {
            return Ok(await _ISourceService.GetOwnersOfSource(source_id));
        }



        [HttpDelete]
        [Route("removeRelationship")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> RemoveRelationship(int employee_id, int source_id)
        {
            return Ok(await _ISourceService.RemoveRelationship(employee_id, source_id));
        }
        [HttpPost]
        [Route("addAdministration")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> AddAdministration(int employee_id, int sg_id)
        {
            return Ok(await _ISourceService.AddAdministration(employee_id, sg_id));
        }

        [HttpPut]
        [Route("updateSourceGroup")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<ReturnSourceGroupDTO>>> UpdateSourceGroup(int sg_id, int former_id, int emp_id)
        {
            return Ok(await _ISourceService.UpdateSourceGroup(sg_id, former_id, emp_id));
        }
        [HttpPut]
        [Route("updateRelationship")]
        [Authorize(Roles = "DbManager")]

        public async Task<ActionResult<Response<ReturnEmployeeDTO>>> UpdateRelationship(int emp_id, int source_id, int new_source_id)
        {
            return Ok(await _ISourceService.UpdateRelationship(emp_id, source_id, new_source_id));
        }


    }
}
