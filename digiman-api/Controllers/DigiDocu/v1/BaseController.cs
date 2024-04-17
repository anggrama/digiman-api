using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digiman_api.Controllers.DigiDocu.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController(IHttpContextAccessor contextAccessor)
        {
            var contextUserId = contextAccessor.HttpContext.User.FindFirst("id");
            if (contextUserId != null)
            {

                //_userId = Guid.Parse(contextAccessor.HttpContext.User.FindFirst("id")?.Value);
            }
                
        }
    }
}
