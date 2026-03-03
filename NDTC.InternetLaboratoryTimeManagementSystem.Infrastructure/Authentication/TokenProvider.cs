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
        public string Create(User user, IList<string>? roles, bool? hasStudentAnsweredEvaluation = default)
        {
            var jwtOption = options.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = [
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new("school_id", user.SchoolId)
            ];

            // if the current user is student, then add the evaluationClaim
            if (hasStudentAnsweredEvaluation.HasValue)
                claims.Add(new("has_answered_to_evaluation", hasStudentAnsweredEvaluation.Value.ToString()));
            
            if (roles != null && roles.Count != 0)
                claims.AddRange(roles.Select(r => new Claim("roles", r)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
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
