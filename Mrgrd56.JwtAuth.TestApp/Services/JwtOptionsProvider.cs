using Microsoft.Extensions.Configuration;
using Mrgrd56.JwtAuth.Models;

namespace Mrgrd56.JwtAuth.TestApp.Services
{
    public class JwtOptionsProvider
    {
        public JwtOptions JwtOptions { get; }
        
        public JwtOptionsProvider(IConfiguration configuration)
        {
            JwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
        }
    }
}