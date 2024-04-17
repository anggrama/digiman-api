using System;
using System.Collections.Generic;
using System.Text;

namespace digiman_common.Handler.MessageHandler
{
    public enum OperationStatus
    {
        Unknown,
        CreateSuccess,
        CreateFailed,
        UpdateSuccess,
        UpdateFailed,
        DeleteSuccess,
        DeleteFailed,
        GetSuccess,
        GetFailed,
        DuplicateNotAllowed,
        UploadFailed,
        UploadSuccess,
        CannotNull,
        DataNotFound,
        OperationFailed,
        OperationSuccess,
        InvalidRequest
        
    }
    public static class OperationMessageHandler
    {
        public static string GetOperationMessage(OperationStatus status)
        {
            switch(status)
            {
                case OperationStatus.CreateSuccess:
                    return "Data has been created successfully";
                case OperationStatus.CreateFailed:
                    return "Data failed to create";
                case OperationStatus.UpdateSuccess:
                    return "Data has been updated successfully";
                case OperationStatus.UpdateFailed:
                    return "Data failed to update";
                case OperationStatus.DeleteSuccess:
                    return "Data has been deleted successfully";
                case OperationStatus.DeleteFailed:
                    return "Data failed to delete";
                case OperationStatus.GetSuccess:
                    return "";
                case OperationStatus.GetFailed:
                    return "Data failed to retrive";
                case OperationStatus.DuplicateNotAllowed:
                    return "Duplicate data not allowed";
                case OperationStatus.UploadFailed:
                    return "Upload failed";
                case OperationStatus.UploadSuccess:
                    return "Upload success";
                case OperationStatus.CannotNull:
                    return "Value cannot be null";
                case OperationStatus.InvalidRequest:
                    return "Input Parameter Not Valid";
                case OperationStatus.DataNotFound:
                    return "Data Not Found";
                case OperationStatus.OperationFailed:
                    return "Operation Failed";
                case OperationStatus.OperationSuccess:
                    return "Operation Success";
                default:
                    return "";

            }
        }
    }
}
