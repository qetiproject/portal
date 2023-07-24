using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Portal.Portal.Persistence
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return accessor?.HttpContext?.User as ClaimsPrincipal;
        }
    }

    public interface IUserService
    {
        ClaimsPrincipal GetUser();
    }
}
