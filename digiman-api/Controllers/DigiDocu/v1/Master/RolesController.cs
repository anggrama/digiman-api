using Asp.Versioning;
using digiman_common.Dto.DigiDocu;
using digiman_common.Dto.Shared;
using digiman_common.Handler.MessageHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using static Azure.Core.HttpHeader;

namespace digiman_api.Controllers.DigiDocu.v1.Master
{
    [Route("api/v{version:apiVersion}/digidocu/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RolesController : ControllerBase
    {
        [HttpGet("list")]
        public async Task<BaseResponse<List<RoleList>>> GetList()
        {
            return null;
            //try
            //{
            //    var result = _roleService.GetList();
            //    return Ok(new SuccessResponse(result));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(
            //       message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
            //       exception: ex.Message
            //   ));
            //}
        }

        /// <summary>
        /// Get Role List for DataTable.Net
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public IActionResult GetAll([DataTablesRequest] DataTablesRequest request)
        //{
        //    try
        //    {
        //        var result = _roleService.GetAllTable(request);
        //        if (request.Draw == 0)
        //        {
        //            throw new Exception("no parameter request");
        //        }
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

        //[AuthorizeResource("roles","read")]
        [HttpGet("{id}")]
        public async Task<BaseResponse<Role>> GetById(Guid id)
        {

            return null;
            //try
            //{
            //    if (id.Equals(Guid.Empty))
            //    {
            //        throw new Exception("id is empty");
            //    }

            //    var result = _roleService.GetById(id);
            //    if (result != null)
            //    {
            //        return Ok(new SuccessResponse(result));
            //    }
            //    else
            //    {
            //        throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DataNotFound));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(
            //       message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
            //       exception: ex.Message
            //   ));
            //}
        }

        //[AuthorizeResource("roles", Arguments = new object[] { "add" })]
        [HttpPost]
        public async Task<BaseResponse<Guid>> CreateAsync([FromBody] Role request)
        {
            return null;
            //try
            //{
            //    var id = await _roleService.CreateAsync(request);
            //    //return Created(new Uri(Url.Link("", new { id })), new SuccessResponse(data: new { id = id }, message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateSuccess)));
            //    return Ok(new SuccessResponse(data: new { id = id }, message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(
            //        message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateFailed),
            //        exception: ex.Message
            //    ));
            //}
        }

        //[AuthorizeResource("roles", Arguments = new object[] { "update" })]
        [HttpPut("{id}")]
        public async Task<BaseResponse<Guid>> UpdateAsync(Guid id, [FromBody] Role request)
        {
            return null;
            //try
            //{
            //    await _roleService.UpdateAsync(id, request);
            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.UpdateSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(
            //        message: OperationMessageHandler.GetOperationMessage(OperationStatus.UpdateFailed),
            //        exception: ex.Message
            //    ));
            //}
        }

        //[AuthorizeResource("roles", Arguments = new object[] { "delete" })]
        [HttpDelete("{id}")]
        public async Task<BaseResponse<Guid>> Delete(Guid id)
        {
            return null;
            //try
            //{
            //    _roleService.Delete(id);
            //    return NoContent();
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(
            //       message: OperationMessageHandler.GetOperationMessage(OperationStatus.DeleteFailed),
            //       exception: ex.Message
            //   ));
            //}
        }

        //[AuthorizeResource("roles", Arguments = new object[] { "read" })]
        [HttpGet("claim-list")]
        public async Task<BaseResponse<List<SysClaims>>> ClaimsTemplate()
        {
            return null;
            //try
            //{
            //    var result = _roleService.CreateClaimsTemplate();
            //    return Ok(new SuccessResponse(result));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(
            //       message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
            //       exception: ex.Message
            //   ));
            //}
        }
    }
}
