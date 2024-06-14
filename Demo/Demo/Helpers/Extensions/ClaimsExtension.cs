using Demo.Models;
using System.Security.Claims;

namespace Demo.Helpers.Extensions
{
    public static class ClaimsExtension
    {

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(u => u.Type.Equals("UserName")).Value;
        }
        public static string GetCartId(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(u => u.Type.Equals("CartId")).Value;
        }
    }
}
