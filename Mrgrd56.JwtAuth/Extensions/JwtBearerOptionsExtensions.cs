using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Mrgrd56.JwtAuth.Models;

namespace Mrgrd56.JwtAuth.Extensions
{
    public static class JwtBearerOptionsExtensions
    {
        public static JwtBearerOptions UseTokenValidationParameters(
            this JwtBearerOptions jwtBearerOptions, 
            JwtOptions jwtOptions, 
            bool? validateIssuer = null,
            bool? validateAudience = null,
            bool? validateLifetime = null,
            bool? validateIssuerSigningKey = null)
        {
            var securityKey = jwtOptions.GetSecurityKey();
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtOptions.Issuer,
                ValidateIssuer = validateIssuer ?? jwtOptions.Issuer != null,
                
                ValidAudience = jwtOptions.Audience,
                ValidateAudience = validateAudience ?? jwtOptions.Audience != null,
                
                ValidateLifetime = validateLifetime ?? jwtOptions.Lifetime > 0,
                
                IssuerSigningKey = securityKey,
                ValidateIssuerSigningKey = validateIssuerSigningKey ?? securityKey != null
            };

            return jwtBearerOptions;
        }
    }
}