using System.Security.Claims;

namespace Mrgrd56.JwtAuth.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Returns a claim with NameIdentifier type.
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static Claim GetIdClaim(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        }
    }
}