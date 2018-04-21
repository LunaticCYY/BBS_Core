using System.Threading.Tasks;
using BBS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BBS.Services
{
    public class UserServices : IUserServices
    {
        private UserManager<User> _userManager { get; }
        private IHttpContextAccessor _context;

        public UserServices(UserManager<User> userManager, IHttpContextAccessor context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<User> User
        {
            get
            {
                return _userManager.GetUserAsync(_context.HttpContext.User);
            }
        }
    }
}
