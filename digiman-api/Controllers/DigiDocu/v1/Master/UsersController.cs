using Asp.Versioning;
using digiman_common.Dto.DigiDocu;
using digiman_common.Dto.Shared;
using digiman_common.Handler.MessageHandler;
using digiman_service.DigiDocu.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace digiman_api.Controllers.DigiDocu.v1.Master
{
    [Route("api/v{version:apiVersion}/digidocu/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : BaseController
    {
        private readonly UserService _userService;
        public UsersController(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _userService = new UserService();
        }


        [HttpGet("list")]
        public async Task<BaseResponse<List<UserList>>> GetUserList()
        {
            try
            {
                var result = await _userService.GetList();
                return new BaseResponse<List<UserList>>()
                {
                    Data = result,
                    Metadata = new BaseMetadata()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<UserList>>()
                {
                    Metadata = new BaseMetadata()
                    {
                        Code = 500,
                        Message = OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
                        Exception = ex.Message
                    }
                };

            }
        }

       
        //[HttpGet]
        //public IActionResult GetAll([DataTablesRequest] DataTablesRequest request)
        //{
        //    try
        //    {
        //        if (request.Draw == 0)
        //            throw new Exception("no parameter request");
        //        var result = _userService.GetAllTable(request);

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
        //[HttpGet("{id}")]
        //public IActionResult GetById(Guid id)
        //{
        //    try
        //    {
        //        if (id.Equals(Guid.Empty))
        //            throw new Exception("id is empty");

        //        var result = _userService.GetById(id);
        //        if (result != null)
        //            return Ok(new SuccessResponse(result));
        //        else
        //            throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DataNotFound));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new FailedResponse(
        //           message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
        //           exception: ex.Message
        //       ));
        //        // return BadRequest(new FailedResponse(
        //        //    message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
        //        //    exception: ex.Message
        //        //));
        //    }
        //}
        //[HttpPost("getadinfo")]
        //public IActionResult GetUserAd([FromBody] User dto)
        //{
        //    try
        //    {
        //        var result = new User()
        //        {
        //            Username = dto.Username,
        //            FullName = dto.Username,
        //            Email = string.Format($"{dto.Username}@sdkindonesia.co.id"),
        //            PhoneNumber = "021021021",
        //            IsLdap = true
        //        };
        //        return Ok(new SuccessResponse(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new FailedResponse(
        //           message: OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
        //           exception: ex.Message
        //       ));
        //    }
        //}
        [HttpPost]
        public async Task<BaseResponse<Guid>> CreateAsync([FromBody] User request)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(new FailedResponse(data: ModelState.Values.Select(i => i.Errors).ToList(), message: OperationMessageHandler.GetOperationMessage(OperationStatus.InvalidRequest)));

            try
            {
                var id = await _userService.CreateAsync(request);
                return new BaseResponse<Guid>()
                {
                    Data = id,
                    Metadata = new BaseMetadata()
                };
                //return Created(new Uri(Url.Link("", new { id })), new SuccessResponse(data: new { id=id }, message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateSuccess)));
                //return Ok(new SuccessResponse(data: new { id = id }, message: OperationMessageHandler.GetOperationMessage(OperationStatus.CreateSuccess)));

            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid>()
                {
                    Metadata = new BaseMetadata()
                    {
                        Code = 500,
                        Message = OperationMessageHandler.GetOperationMessage(OperationStatus.GetFailed),
                        Exception = ex.Message
                    }
                };
            }
        }

        // Just For Layout
        [HttpGet("{id}")]
        public async Task<BaseResponse<User>> GetById(Guid id)
        {
            return null;
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse<Guid>> UpdateAsync(Guid id, [FromBody] User request)
        {

            return null;
        }
        [HttpDelete("{id}")]
        public async Task<BaseResponse<Guid>> Delete(Guid id)
        {
            return null;
        }


    }
}
