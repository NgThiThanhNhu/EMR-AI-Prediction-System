using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EMR_AIPredictionSystem.Model.Request.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace EMR_AIPredictionSystem.Common
{
    public class Encrypt_Decrypt
    {
        public static string Key { get; set; } = "njuepdonghai850b7bbsieu405cto8d0fkhong4c4c5lo080nhldc0";
        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new System.Security.Cryptography.RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
        public static string EncodePassword(string pass, string salt)
        {
            var bytes = Encoding.Unicode.GetBytes(pass);
            var src = Convert.FromBase64String(salt);
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pass + salt));
            return Convert.ToBase64String(data);
        }
        public static string GenerateJwtToken(TokenRequest tokenRequest, IConfiguration config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, tokenRequest.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, tokenRequest.Name),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, tokenRequest.Rolename) // Phân quyền đúng cách
    };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"], // Đảm bảo Issuer không null
                audience: config["Jwt:Audience"], // Đảm bảo Audience hợp lệ
                claims: claims,
                expires: DateTime.Now.AddHours(Common.CommonConst.ExpireTime),
                signingCredentials: credentials
            );
            return tokenHandler.WriteToken(token);
        }
    }
}
