using Asp.Versioning;
using digiman_common.Dto.DigiDocu;
using digiman_common.Dto.Shared;
using digiman_common.Handler.MessageHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace digiman_api.Controllers.DigiDocu.v1.Master
{
    [Route("api/v{version:apiVersion}/digidocu/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StoragesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<BaseResponse<digiman_common.Dto.DigiDocu.SysStorage>> GetById(Guid id)
        {
            return null;
            //try
            //{
            //    if (id.Equals(Guid.Empty))
            //    {
            //        throw new Exception("id is empty");
            //    }

            //    var result = _storageService.GetById(id);
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

        [HttpGet]
        public async Task<BaseResponse<List<digiman_common.Dto.DigiDocu.StorageList>>> GetAll()
        {
            //try
            //{
            //    return Ok(_storageService.GetAll());
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //     message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
            //     exception: ex.Message
            // ));
            //}

            return null;

        }

        [HttpPost]
        public async Task<BaseResponse<Guid>> CreateAsync([FromBody] digiman_common.Dto.DigiDocu.SysStorage request)
        {

            return null;
            //try
            //{
            //    var id = await _storageService.CreateAsync(request);
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
        public async Task<BaseResponse<Guid>> UpdateAsync(Guid id, [FromBody] digiman_common.Dto.DigiDocu.SysStorage request)
        {
            return null;
            //try
            //{
            //    await _storageService.UpdateAsync(id, request);

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
            //    _storageService.Delete(id);
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

        [HttpGet("types")]
        public async Task<List<SysStorageTypeList>> GetStorageType()
        {

            return null;
            //try
            //{
            //    return Ok(_storageService.GetTypeList());
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //     message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
            //     exception: ex.Message
            // ));
            //}
        }
    }
}
