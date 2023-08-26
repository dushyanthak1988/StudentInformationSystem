using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentInformationSystem.Models;
using StudentInformationSystem.Models.ResponseModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentInformationSystem.Repository
{
    public interface IJwtService
    {
        LoginResponseModel GenerateTokens(ApplicationUser user);
    }

    public class JwtService : IJwtService
    {

        private readonly AppSettings _appSettings;
        public JwtService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


        public LoginResponseModel GenerateTokens(ApplicationUser user)
        {
            var authClaims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_appSettings.AccessTokenExpirationMinutes));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key)); // Replace with your secret key

            var accessToken = new JwtSecurityToken(
                issuer: _appSettings.Issuer, // Replace with your issuer
                audience: _appSettings.Issuer, // Replace with your audience
                expires: expires,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var refreshToken = new JwtSecurityToken(
                issuer: _appSettings.Issuer, // Replace with your issuer
                audience: _appSettings.Issuer, // Replace with your audience
                expires: expires,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );


            var responseObj = new LoginResponseModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken),
                ExpiresDate = expires

            };
            return responseObj;
        }
    }
}
