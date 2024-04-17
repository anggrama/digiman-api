using Asp.Versioning;
using digiman_common.Dto.DigiDocu;
using digiman_common.Dto.Shared;
using digiman_common.Handler.MessageHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digiman_api.Controllers.DigiDocu.v1.Master
{
    [Route("api/v{version:apiVersion}/digidocu/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ParametersController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult GetAll([DataTablesRequest] DataTablesRequest request)
        //{
        //    try
        //    {
        //        var result = _parameterService.GetAllTable(request);
        //        if (request.Draw == 0)
        //        {
        //            throw new Exception("no parameter request");
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new FailedResponse(
        //            message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
        //            exception: ex.Message
        //        ));
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<BaseResponse<SysParameter>> GetById(Guid id)
        {
            return null;
            //try
            //{
                //if (id.Equals(Guid.Empty))
                //{
                //    throw new Exception("id is empty");
                //}

                //var result = _parameterService.GetById(id, "U");
                //if (result != null)
                //    return Ok(new SuccessResponse(result));
                //else
                //    throw new Exception("data not found");
            //}
            //catch (Exception ex)
            //{
                //return BadRequest(new FailedResponse(
                //    message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
                //    exception: ex.Message
                //));
            //}
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse<Guid>> Update(Guid id, [FromBody] SysParameter request)
        {
            return null;
            //if (!ModelState.IsValid)
            //    return BadRequest(new FailedResponse(data: ModelState.Values.Select(i => i.Errors).ToList(), message: OperationMessageHandler.GetOperationMessage(OperationStatus.InvalidRequest)));

            //try
            //{
            //    int result = await _parameterService.Update(id, request);
            //    if (result > 0)
            //        return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.UpdateSuccess)));
            //    else
            //        throw new Exception();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //    exception: ex.Message,
            //    message: OperationMessageHandler.GetOperationMessage(OperationStatus.UpdateFailed)
            //    ));

            //}
        }
    }
}
