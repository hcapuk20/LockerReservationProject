using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRProject.Models;
using LRProject.Service;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<Source>> GetAllSources()
        {
            return Ok(_ISourceService.GetAllSources());
        }
    }
}