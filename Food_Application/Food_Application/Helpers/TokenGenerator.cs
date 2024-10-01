using Food_Application.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Food_Application.Helpers
{
    public static class TokenGenerator
    {
        public static string GenerateToken(User user)
        {
            //var authClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
            //};


            //foreach (var userRole in user.Roles)
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            //}
            var t = Environment.GetEnvironmentVariable("SECRET_KEY");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Sub,user.ID.ToString()),
                }
                    ),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(Environment.GetEnvironmentVariable("DURATION_IN_MINUTES"))),
                Issuer = Environment.GetEnvironmentVariable("ISSUER"),
                Audience  =Environment.GetEnvironmentVariable("AUDIENCE") ,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"))), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
