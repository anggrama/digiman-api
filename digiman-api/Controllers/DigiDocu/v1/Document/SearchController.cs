using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using digiman_api.Controllers.DigiDocu.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace digiman_api.Controllers.DigiDocu.v1.Document
{
    [Route("api/v{version:apiVersion}/digidocu/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SearchController : BaseController
    {
        
        public SearchController(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {

        }

        //[HttpGet("documents")]
        //public IActionResult Documents([DataTablesRequest] DataTablesRequest dtRequest, [FromQuery] DocumentSearchRequestData request)
        //{
        //    try
        //    {
        //        var result = _searchService.DocumentSearch(request, dtRequest);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new FailedResponse(
        //           message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
        //           exception: ex.Message
        //       ));
        //    }
        //}
    }
}