using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LRProject.Service
{
    public class UserService : IUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;



        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public int GetMyId()
        {
            var result = 0;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));
            }
            return result;
        }
    }
}