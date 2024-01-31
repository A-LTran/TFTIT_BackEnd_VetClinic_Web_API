using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TFTIC_BackEnd_VetClinic_Web_API.Tools
{
    public class TokenManager
    {
        public static string key = "Test,eozik,eiof,z*$**é123654987aziajzheiahdazd--Test,eozik,eiof,z*$**é123654987aziajzheiahdazdTest,eozik,eiof,z*$**é123654987aziajzheiahdazd";

        public string GenerateToken(User user)
        {
            //Générer la clé de signature de mon Token
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] myclaims = new[]
            {
                new Claim(ClaimTypes.Name, user.LastName),
                new Claim(ClaimTypes.Role, ReturnRole(user.PersonRole)),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.PersonId.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                claims: myclaims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddDays(1)
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        private static string ReturnRole(Role personRole)
        {
            switch (personRole)
            {
                case Role.Administrator:
                    return "administrator";
                case Role.Veterinary:
                    return "veterinary";
                default:
                    return "anonymous";
            }
        }
    }
}
