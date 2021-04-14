using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ShopMate.Data
{
    public interface ICurrentUser
    {
        public string UserId { get; }
    }

    public class HttpContextCurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _context;

        public HttpContextCurrentUser(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string UserId => _context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
