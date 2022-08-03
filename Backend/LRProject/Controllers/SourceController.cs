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
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
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
        public async Task<ActionResult<List<Employee>>> AddEmployee(int employee_id, string name)
        {
            return Ok(await _ISourceService.AddEmployee(employee_id, name));
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


    }
}