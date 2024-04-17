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
    public class GroupsController : ControllerBase
    {

        [HttpGet("list")]
        public async Task<BaseResponse<GroupList>> GetList()
        {
            return null;
            //try
            //{
            //    var result = _groupService.GetList();
            //    return Ok(new SuccessResponse(result));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //       message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
            //       exception: ex.Message
            //   ));
            //}
        }

        //[HttpGet]
        //public IActionResult GetAll([DataTablesRequest] DataTablesRequest request)
        //{
        //    try
        //    {
        //        var result = _groupService.GetAll(request);
        //        if (request.Draw == 0)
        //        {
        //            throw new Exception("no parameter request");
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new FailedResponse(
        //           message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
        //           exception: ex.Message
        //       ));
        //    }
        //}

        [HttpGet("{id}")]
        public Task<BaseResponse<Group>> GetById(Guid id)
        {
            return null;
            //try
            //{
            //    if (id.Equals(Guid.Empty))
            //    {
            //        throw new Exception("id is empty");
            //    }

            //    var result = _groupService.GetById(id);
            //    if (result != null)
            //        return Ok(new SuccessResponse(result));
            //    else
            //        throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DataNotFound));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //       message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
            //       exception: ex.Message
            //   ));
            //}
        }

        [HttpPost]
        public async Task<BaseResponse<Guid>> CreateAsync([FromBody] Group request)
        {
            return null;
            //try
            //{
            //    var id = await _groupService.CreateAsync(request);
            //    return Created(new Uri(Url.Link("", new { id })), new SuccessResponse(data: new { id = id }, message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //        message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateFailed),
            //        exception: ex.Message
            //    ));
            //}
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse<Guid>> UpdateAsync(Guid id, [FromBody] Group request)
        {
            return null;
            //try
            //{
            //    await _groupService.UpdateAsync(id, request);

            //    return Ok(new SuccessResponse(
            //    message: OperationMessageHandler.GetOperationMessage(OperationStatus.UpdateSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //        message: OperationMessageHandler.GetOperationMessage(OperationStatus.UpdateFailed),
            //        exception: ex.Message
            //    ));
            //}
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse<Guid>> Delete(Guid id)
        {
            return null;
            //try
            //{
            //    _groupService.Delete(id);
            //    return NoContent();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //       message: OperationMessageHandler.GetOperationMessage(OperationStatus.DeleteFailed),
            //       exception: ex.Message
            //   ));
            //}
        }
    }
}
