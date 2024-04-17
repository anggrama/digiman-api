using Asp.Versioning;
using digiman_common.Dto.DigiDocu;
using digiman_service.DigiDocu.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digiman_api.Controllers.Auth.v1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;

        public AuthController(IHttpContextAccessor httpContextAccessor) 
        {
            _authService = new AuthService();
        }

        [HttpPost]
        public async Task<digiman_common.Dto.Shared.BaseResponse<TokenResponse>> Token(TokenRequest arg,CancellationToken ct)
        {
            try
            {
                var returnValue = await _authService.GetToken(arg, ipAddress());
                setTokenCookie(returnValue.refresh_token);

                return new digiman_common.Dto.Shared.BaseResponse<TokenResponse>()
                {
                    Data = returnValue,
                    Metadata = new digiman_common.Dto.Shared.BaseMetadata()
                };
            }
            catch (Exception ex)
            {
                return new digiman_common.Dto.Shared.BaseResponse<TokenResponse>()
                {
                    Metadata = new digiman_common.Dto.Shared.BaseMetadata()
                    {
                        Code = 500,
                        Message =  ex.Message
                    }
                };
            }
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
