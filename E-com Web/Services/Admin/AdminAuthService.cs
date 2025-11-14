using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_com_Web.Services.Admin
{
    public interface IAdminAuthService
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }

    public class AdminAuthService : IAdminAuthService
    {
        private readonly IConfiguration _config;

        public AdminAuthService(IConfiguration config)
        {
            _config = config;
        }

        public Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var cfgUser = _config["AdminAuth:Username"];
            var cfgPass = _config["AdminAuth:Password"];
            if (string.IsNullOrWhiteSpace(cfgUser) || string.IsNullOrEmpty(cfgPass))
                return Task.FromResult(false);

            var userMatch = string.Equals(cfgUser, username, StringComparison.Ordinal);
            if (!userMatch) return Task.FromResult(false);

            // For now, compare plain text from configuration.
            // We can switch to hashed comparison without changing the interface.
            var passMatch = string.Equals(cfgPass, password, StringComparison.Ordinal);
            return Task.FromResult(passMatch);
        }
    }
}
