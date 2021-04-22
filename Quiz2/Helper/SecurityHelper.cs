using System.Security.Claims;

namespace Quiz2.Helper
{
    public static class SecurityHelper
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
        }
    }
}