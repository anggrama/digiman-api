using Asp.Versioning;
using digiman_common.Dto.DigiDocu;
using digiman_common.Dto.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digiman_api.Controllers.DigiDocu.v1.Document
{
    [Route("api/v{version:apiVersion}/digidocu/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProfilesController : ControllerBase
    {

        [HttpGet("list")]
        public async Task<BaseResponse<List<UserList>>> GetList()
        {
            return null;
            //try
            //{
            //    var result = _documentProfileService.GetList();
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(
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
        //        if (request.Draw == 0)
        //            throw new Exception("no parameter request");

        //        var result = _documentProfileService.GetAllTable(request);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
        //    }
        //}

        [HttpGet("parents")]
        public async Task<BaseResponse<List<DocumentProfileSelect>>> GetParentDocumentProfile()
        {
            return null;
            //try
            //{
            //    var result = _documentProfileService.GetParentSelect();
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
        [HttpGet("{id}")]
        public async Task<BaseResponse<DocumentProfile>> GetById(Guid id)
        {
            return null;
            //try
            //{
            //    var result = _documentProfileService.GetById(id);
            //    if (result == null)
            //        throw new Exception();

            //    return Ok(new SuccessResponse(result));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new
            //    {
            //        result = "failed",
            //        message = "Failed to get parameter.",
            //        exception = ex.Message
            //    });
            //}
        }
        [HttpPost]
        public async Task<BaseResponse<Guid>> CreateAsync([FromBody] DocumentProfile dto)
        {
            return null;

            //try
            //{
            //    var result = _documentProfileService.CreateAsync(dto).Result;
            //    return Ok(new SuccessResponse(data: new { id = result }, message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateFailed), exception: ex.Message));
            //}
        }
        [HttpPut("{id}")]
        public async Task<BaseResponse<Guid>> UpdateAsync(Guid id, [FromBody] DocumentProfile request)
        {
            return null;
            //try
            //{
            //    await _documentProfileService.UpdateAsync(id, request);

            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.UpdateSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //        message: OperationMessageHandler.GetOperationMessage(OperationStatus.UpdateFailed),
            //        exception: ex.Message
            //    ));
            //}
        }

        //[HttpPost]
        //public IActionResult OnPostCreateUpdateAsync([FromForm] IFormFile watermarkFile, [FromForm] DocumentProfile dto)
        //{
        //    try
        //    {
        //        var result = _documentProfileService.CreateAndUpdateAsync(dto, watermarkFile).Result;

        //        if (result)
        //            return Ok(new
        //            {
        //                result = "success",
        //                message = "Document profile has been saved successfully."
        //            });
        //        else
        //            throw new Exception();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            result = "failed",
        //            message = "Failed to save document profile.",
        //            exception = ex.Message
        //        });
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<BaseResponse<Guid>> DeleteAsync(Guid id)
        {
            return null;
            //try
            //{
            //    var result = await _documentProfileService.DeleteAsync(id);
            //    if (result)
            //        return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.DeleteSuccess)));
            //    else
            //        throw new Exception();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(
            //        message: OperationMessageHandler.GetOperationMessage(OperationStatus.DeleteFailed),
            //        exception: ex.Message
            //    ));
            //}
        }

        [HttpPost("upload-wm")]
        public async Task<IActionResult> UploadWatermark(IFormFile file, [FromForm] Guid id, CancellationToken ct)
        {
            return null;
            //try
            //{
            //    await _documentProfileService.UploadWatermark(file, id);
            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        [HttpPost("upload-wmfield")]
        public async Task<IActionResult> UploadWatermarkField(IFormFile file, Guid id, CancellationToken ct)
        {
            return null;
            //try
            //{
            //    await _documentProfileService.UploadWatermark(file, id);
            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        [HttpGet("wm-old/{id}")]
        public async Task<BaseResponse<DocumentProfileWatermark>> Watermark(Guid id)
        {
            return null;
            //try
            //{
            //    var wm = _documentProfileService.GetWatermark(id);



            //    if (wm != null)
            //        return File(wm.Watermark, wm.ContentType);
            //    else
            //        throw new Exception("Image not Found");
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}


        }

        [HttpGet("wm/{id}")]
        public async Task<BaseResponse<DocumentProfileWatermark>> Watermark64(Guid id)
        {
            return null;
            //try
            //{
            //    var wm = _documentProfileService.GetWatermark(id);
            //    string src = $"data:{wm.ContentType};base64,{Convert.ToBase64String(wm.Watermark)}";

            //    if (wm != null)
            //        return Ok(src);
            //    else
            //        throw new Exception("Image not Found");
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}


        }

        [HttpDelete("wm/{id}")]
        public async Task<BaseResponse<Guid>>DeleteWatermark(Guid id)
        {
            return null;
            //try
            //{
            //    _documentProfileService.DeleteWatermark(id);
            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}

        }
    }
}
