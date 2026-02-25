using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using Microsoft.Extensions.Options;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Options;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Authentication
{
    internal sealed class TokenProvider(IOptions<JwtOptions> options) 
        : ITokenProvider
    {
        public string Create(User user)
        {
            var jwtOption = options.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    //new Claim(JwtRegisteredClaimNames.Email, user.Email)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(jwtOption.AccessTokenExpirationMinutes),
                SigningCredentials = credentials,
                Issuer = jwtOption.Issuer,
                Audience = jwtOption.Audience
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
