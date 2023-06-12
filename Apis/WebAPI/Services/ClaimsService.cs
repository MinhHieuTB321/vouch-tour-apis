using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            // todo implementation to get the current userId
            var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
            GetId = string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);

            var userRoleId = httpContextAccessor.HttpContext?.User.FindFirstValue("UserRoleId");
            GetUserRoleId= string.IsNullOrEmpty(userRoleId) ? Guid.Empty : Guid.Parse(userRoleId);

            var email= httpContextAccessor.HttpContext?.User.FindFirstValue("Email");
            GetEmail = email.IsNullOrEmpty() ? "" : email.ToString();

        }
        public Guid GetId { get; }

        public string GetEmail { get; }

        public Guid GetUserRoleId { get; }
    }
}
