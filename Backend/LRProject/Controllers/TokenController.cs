using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LRProject.Data;
using Microsoft.AspNetCore.Authorization;
using LRProject.Models;

namespace LRProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        public IConfiguration _config;
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly ISourceService _sourceService;

        public TokenController(IConfiguration config, DataContext context, IUserService userService, ISourceService sourceService)
        {
            _config = config;
            _context = context;
            _userService = userService;
            _sourceService = sourceService;
        }
        [HttpGet]
        [Route("getUserId")]
        public ActionResult<string> GetUserid()
        {
            var userid = _userService.GetMyId();
            return Ok(userid);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(int id, string password)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.Password == password && e.Status == 1);
            if (employee != null)
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                    new Claim("UserId", employee.Id.ToString()),
                    new Claim(ClaimTypes.Name, employee.Name),
                    new Claim(ClaimTypes.Role, employee.Role)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: cred);

                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                var req = await _sourceService.GetEmployeeById(id);
                var emp = req.Data;

                return Ok(new LoginResult() { Token = accessToken, Employee = emp });

                //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid User Id or password.");
            }
        }

    }
}