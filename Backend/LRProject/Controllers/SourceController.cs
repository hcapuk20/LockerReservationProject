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
        [Route("getAllSources")]
        public async Task<ActionResult<List<Source>>> GetAllSources()
        {
            return Ok(await _ISourceService.GetAllSources());
        }
        [HttpPost]
        [Route("addSource")]
        public async Task<ActionResult<List<Source>>> AddSource(int id, string type)
        {
            return Ok(await _ISourceService.AddSource(id, type));
        }
        [HttpPost]
        [Route("manualAddRelationship")]
        public async Task<ActionResult<List<Owns>>> ManualAddRelationship(int source_id, int employee_id)
        {
            return Ok(await _ISourceService.ManualAddRelationship(source_id, employee_id));
        }

        [HttpDelete]
        [Route("removeSource")]
        public async Task<ActionResult<List<Source>>> RemoveSource(int source_id)
        {
            return Ok(await _ISourceService.RemoveSource(source_id));
        }

        [HttpGet]
        [Route("getRelationships")]
        public async Task<ActionResult<List<Owns>>> GetAllRelationships()
        {
            return Ok(await _ISourceService.GetAllRelationships());
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


    }
}