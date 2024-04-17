using Asp.Versioning;
using digiman_common.Dto.DigiDocu;
using digiman_common.Dto.Shared;
using digiman_common.Handler.MessageHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digiman_api.Controllers.DigiDocu.v1.Document
{
    [Route("api/v{version:apiVersion}/digidocu/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DocumentsController : ControllerBase
    {
        // done tested
        [HttpGet("folders/{id}")]
        public async Task<BaseResponse<List<FileResponse>>> GetFolders(string id, [FromQuery] string path, CancellationToken ct)
        {
            return null;
            //try
            //{
            //    var result = _documentService.GetObjects(id, "F", path);
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        [HttpPost("create-folder")]
        public async Task<BaseResponse<FolderResponse>> CreateFolder([FromBody] CreateFolderRequest request)
        {
            return null;
            //try
            //{
            //    var result = await _documentService.CreateFolderAsync(request);
            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateSuccess), data: result));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateFailed), exception: ex.Message));
            //}
        }

        [HttpGet("files/{id}")]
        public async Task<BaseResponse<List<FileResponse>>> GetFiles(string id)
        {
            return null;
            //try
            //{
            //    return Ok(_documentService.GetObjects(id, "D"));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        [HttpPost("rename")]
        public async Task<BaseResponse<FolderFileBaseResponse>> Rename([FromBody] RenameRequest request)
        {
            return null;
            //if (ModelState.IsValid == false)
            //    return BadRequest(new FailedResponse(data: ModelState.Values.Select(i => i.Errors).ToList(), message: OperationMessageHandler.GetOperationMessage(OperationStatus.InvalidRequest)));

            //try
            //{
            //    var returnData = _documentService.Rename(request.Id, request.Name);
            //    return Ok(new SuccessResponse(data: returnData, message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        [HttpPost("move")]
        public async Task<BaseResponse<FolderFileBaseResponse>> Move([FromBody] MoveRequest request)
        {
            return null;
            //if (ModelState.IsValid == false)
            //    return BadRequest(new FailedResponse(data: ModelState.Values.Select(i => i.Errors).ToList(), message: OperationMessageHandler.GetOperationMessage(OperationStatus.InvalidRequest)));

            //try
            //{
            //    var returnData = _documentService.Move(request.Id, request.To);
            //    if (returnData == null)
            //        throw new Exception("Root folder can't be moved.");

            //    return Ok(new SuccessResponse(data: returnData, message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse<Guid>> Delete(Guid id)
        {
            return null;
            //try
            //{
            //    _documentService.Delete(id);
            //    return NoContent();
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.DeleteFailed), exception: ex.Message));
            //}
        }

        [HttpPost("upload-drop")]
        public async Task<BaseResponse<FileResponse>> UploadDrag([FromForm] UploadDragRequest request)
        {
            return null;
            //try
            //{
            //    var data = await _documentService.UploadDragDrop(request);
            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationSuccess), data: data));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        [HttpPost("upload")]
        public async Task<BaseResponse<FileResponse>> Upload([FromForm] UploadDragRequest request)
        {
            return null;
            //try
            //{
            //    var data = await _documentService.Upload(request);
            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationSuccess), data: data));
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        //anggra done untested
        [HttpPost]
        public async Task<BaseResponse<FileResponse>> CreateDocument([FromBody] DocumentRequest request)
        {
            return null;
            //try
            //{
            //    var result = await _documentService.CreateDocument(request);
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        //anggra onprogress
        [HttpPut("{id}")]
        public async Task<BaseResponse<FileResponse>> UpdateDocument(Guid id, [FromBody] DocumentRequest request)
        {

            return null;
            //try
            //{
            //    var result = _documentService.UpdateDocument(id, request);
            //    return Ok(new SuccessResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationSuccess)));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        [HttpGet("permission-template")]
        public async Task<BaseResponse<List<SettingCollectionTemplateDto<string,string>>>> GetPermissionTemplate()
        {
            return null;
            //try
            //{
            //    var result = _documentService.GetPermission();
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new FailedResponse(message: OperationMessageHandler.GetOperationMessage(OperationStatus.OperationFailed), exception: ex.Message));
            //}
        }

        //anggra task

        //isal task
        [HttpPost("checkout")]
        public async Task<BaseResponse<Guid>> Checkout([FromBody] Guid id)
        {
            return null;
            //try
            //{
            //    _documentService.CheckOut(id);
            //    return Ok(new SuccessResponse(message: "Checkout success."));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(message: "Failed to checkout.", exception: ex.Message));
            //}
        }

        //ada pilihan untuk naik versi atau engga, versi 2 digit major dan minor 1.0,1.1 dst
        [HttpPost("checkin")]
        public async Task<BaseResponse<Guid>> Checkin([FromBody] Guid id)
        {
            return null;
            //try
            //{
            //    _documentService.CheckIn(id);
            //    return Ok(new SuccessResponse(message: "CheckIn success."));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(message: "Failed to check in.", exception: ex.Message));
            //}
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            return null;
            //try
            //{
            //    var file = _documentService.Download(id);
            //    return File(file.Data, file.ContentType, file.Filename);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(message: "Failed to download file.", exception: ex.Message));
            //}
        }

        [HttpGet("download-zip/{id}")]
        public async Task<IActionResult> DownloadZip(Guid id)
        {
            return null;
            //try
            //{
            //    var file = _documentService.DownloadZip(id);
            //    return File(file.Data, file.ContentType, file.Filename);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(message: "Failed to download file zip.", exception: ex.Message));
            //}
        }

        [HttpGet("get-document/{id}")]
        public async Task<BaseResponse<DocumentResponse>> GetDocument(Guid id)
        {
            return null;
            //try
            //{
            //    var result = _documentService.GetDocument(id);

            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(message: "Failed to get document data", exception: ex.Message));
            //}
        }

        [HttpGet("get-thumbnail/{id}")]
        public async Task<IActionResult> GetDocumentThumbnail(Guid id)
        {
            return null;
            //try
            //{
            //    var result = _documentService.GetDocumentThumbnail(id);

            //    return File(result, "application/png", "thumbnail.png");
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new FailedResponse(message: "Failed to get document data", exception: ex.Message));
            //}
        }

        //Bookmark
        //version history
    }
}
