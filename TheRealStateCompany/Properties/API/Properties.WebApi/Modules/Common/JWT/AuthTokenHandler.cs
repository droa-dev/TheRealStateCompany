using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Properties.WebApi.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Properties.WebApi.Modules.Common.JWT
{
    /// <summary>
    ///     Handler for token operations.
    /// </summary>
    public sealed class AuthTokenHandler : IAuthTokenHandler
    {
        private readonly JwtConfigs _jwtConfigs;

        public AuthTokenHandler(IOptionsMonitor<JwtConfigs> optionsMonitor)
        {
            _jwtConfigs = optionsMonitor.CurrentValue;
        }

        public async Task<string> GenerateJwtToken(UserModel user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfigs.SingingKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Username),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Audience = _jwtConfigs.ValidIssuer,
                Issuer = _jwtConfigs.ValidIssuer,
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return await Task.FromResult(jwtToken)
               .ConfigureAwait(false);
        }

        public async Task<bool> ValidCredentials(UserModel user)
        {
            //dummy validation
            //this method should be as BussinesCase in Application layer
            //will be implemented with Redis cache as persistence provider
            bool response = false;

            if (user == null) return false;

            if (user.Username.Equals(_jwtConfigs.DefaultUser))
            {
                if (user.Password.Equals(_jwtConfigs.DefaultPassword))
                {
                    response = true;
                }
            }

            return await Task.FromResult(response)
               .ConfigureAwait(false);
        }
    }
}
