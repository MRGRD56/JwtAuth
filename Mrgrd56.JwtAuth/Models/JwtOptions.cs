using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Mrgrd56.JwtAuth.Models
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Lifetime { get; set; }

        public JwtOptions()
        {
            
        }
        
        public JwtOptions(string issuer, string audience, string key, int lifetime)
        {
            Issuer = issuer;
            Audience = audience;
            Key = key;
            Lifetime = lifetime;
        }

        private static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
        
        public SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public string GenerateToken(IEnumerable<Claim> claims, string securityAlgorithm)
        {
            var securityKey = GetSecurityKey();
            var credentials = new SigningCredentials(securityKey, securityAlgorithm);

            var token = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                expires: DateTime.Now.AddSeconds(Lifetime),
                signingCredentials: credentials);

            return JwtSecurityTokenHandler.WriteToken(token);
        }

        public string GenerateToken(IEnumerable<Claim> claims) => 
            GenerateToken(claims, SecurityAlgorithms.HmacSha256);

        public string GenerateToken(string id, string login, IEnumerable<string> roles, string securityAlgorithm)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, id),
                new(JwtRegisteredClaimNames.Name, login)
            };
            var rolesClaims = roles.ToArray().Select(role => new Claim("role", role));
            claims.AddRange(rolesClaims);

            return GenerateToken(claims, securityAlgorithm);
        }

        public string GenerateToken(string id, string login, IEnumerable<string> roles) =>
            GenerateToken(id, login, roles, SecurityAlgorithms.HmacSha256);
    }
}