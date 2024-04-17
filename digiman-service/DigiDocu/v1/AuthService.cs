using digiman_common.Dto;

using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using digiman_common.Dto.DigiDocu;
using Microsoft.Extensions.Configuration;
using digiman_common.Dto.Shared;

namespace digiman_service.DigiDocu.v1
{
    public class AuthService : BaseService
    {

        private UserService _userService;
        //private LiteDbService<ClientSecret> _liteDbClientSecretService;
        //private IClientSecretRepository _clients;

        string dbDir = Directory.GetCurrentDirectory();
        private int expiredHour = 0;
        private byte[] key;
        public AuthService(UserLoginInfo userInfo) :base(userInfo)
        {
            
        }

        public AuthService()
        {
            _userService = new UserService();

            var configToken = _configuration.GetSection("TokenSettings");
            key = Encoding.ASCII.GetBytes(configToken.GetSection("SecretKey").Value);
            expiredHour = Convert.ToInt32(configToken.GetSection("ExpiredHour").Value);
        }

        public async Task<TokenResponse> GetToken(TokenRequest req, string ipAddress)
        {
            try
            {
                UserSelect user;
                user = await _userService.AuthenticateUser(req.username, req.password);

                var token = GenerateJwtToken(user.Id.ToString());
                var refresh_token = GenerateRefreshToken(ipAddress);

                //generate token
                return new TokenResponse()
                {
                    access_token = token,
                    token_type = "Bearer",
                    userinfo = user,
                    expires_in = expiredHour * 60 * 60,
                    refresh_token = refresh_token.Token
                };
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private string GenerateJwtToken(string userId)
        {

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
                Expires = DateTime.UtcNow.AddHours(expiredHour),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.Now
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //private string GenerateJwtToken(UserSelect user, TokenRequest req)
        //{
           
        //    // generate token that is valid for 7 days
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("client_id", req.client_id) }),
        //        Expires = DateTime.UtcNow.AddHours(expiredHour),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //        IssuedAt = DateTime.Now
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        private RefreshTokenResponse GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshTokenResponse
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddHours(expiredHour),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
        //private bool CredentialsCheckDb(string clientId, string clientSecret)
        //{
        //    var data = _clients.GetAll().Where(i => i.Client.ClientId == clientId && i.Value == clientSecret).FirstOrDefault();
        //    if (data != null)
        //    {

        //        var clientSec = new ClientSecret()
        //        {
        //            id = clientId,
        //            client_secret = clientSecret
        //        };

        //        _liteDbClientSecretService.InsertOrUpdate(clientSec,i=>i.id == clientId);
        //        return true;
        //    }
        //    else
        //        throw new Exception("Invalid credentials");

        //}
       //public string GetClientSecretFromLiteDb(string clientId)
       // {
       //     try
       //     {
       //         var result = _liteDbClientSecretService.GetValue(i => i.id == clientId);
       //         if (result != null)
       //             return result.client_secret;
       //         else
       //             return "";
       //     }
       //     catch (Exception)
       //     {

       //         throw;
       //     }
       // }

     
    }
}
