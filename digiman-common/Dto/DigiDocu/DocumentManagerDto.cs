using digiman_common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class FolderFileBaseResponse
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public long Size { get; set; }
        public long Date { get; set; }
        public string Type { get; set; }
        public Guid? ParentId { get; set; }
        public string Path { get; set; }
        public List<FolderResponse> Data { get; set; }

        public FolderFileBaseResponse()
        {

        }
        //public FolderFileBaseResponse(DmsObjects model)
        //{
        //    this.Id = model.Id.ToString();
        //    this.Type = model.Type;
        //    this.Size = 0;
        //    this.Value = model.Name;
        //    this.Date = Convert.ToDateTime(model.CreatedAt).Ticks;
        //    this.ParentId = model.ParentId;
            
        //}
    }
    public class FolderResponse : FolderFileBaseResponse
    {

        public FolderResponse()
        {

        }
        //public FolderResponse(DmsObjects model) : base(model)
        //{

        //}
    }
    public class FileResponse : FolderFileBaseResponse
    {
        public string IsCheckOut { get; set; }
        public string Owner { get; set; }
        public string Ext { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Version { get; set; }

        //public FileResponse(DmsObjects model) : base(model)
        //{
        //    this.ParentId = model.ParentId;
        //    this.Owner = model.CreatedByNavigation.Username;
        //    this.UpdatedDate = Convert.ToString(model.UpdatedAt);

        //    var doc = model.DmsDocuments.FirstOrDefault(i => i.IsObsolete == false);
        //    if (doc != null)
        //    {
        //        this.Version = doc.Version;
        //    }

        //    if (model.Type == ((char)Enums.ObjectTypeEnum.Compound).ToString())
        //        this.Ext = "multi";
        //    else if(model.Type == ((char)Enums.ObjectTypeEnum.Document).ToString())
        //    {
        //        if (doc.DmsDocumentFiles.Count > 0)
        //        {
        //            this.Ext = doc.DmsDocumentFiles.FirstOrDefault().Extension;
        //            this.Size = doc.DmsDocumentFiles.Sum(i => i.Size);
        //        }
        //        else
        //        {
        //            this.Ext = "";
        //            this.Size = 0;
        //        }
                
        //    }
            
        //}

        public FileResponse(FolderResponse dto)
        {
            this.Id = dto.Id;
            this.Value = dto.Value;
            this.Date = dto.Date;
            this.Type = dto.Type;
            this.Size = 0;
            this.ParentId = dto.ParentId;
            this.Path = $"/{dto.Id}";
            this.Data = dto.Data;
        }
    }
    public class CreateFolderRequest
    {
        public string FolderName { get; set; }
        public string ParentId { get; set; }
        public Guid? StorageId { get; set; }
        public int? LimitStorage { get; set; }
        public Guid? DocumentProfileId { get; set; }
        public string Description { get; set; }
    }
    public class RenameRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
    public class MoveRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid To { get; set; }
    }
    public class UploadDragRequest
    {
        //public IFormFile File { get; set; }
        public Guid Id { get; set; }
    }
        






    public class DocumentManagerRequest
    {
        public string Action { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
    }
    public class DocumentManagerDeleteRequest
    {
        public Guid[] Data { get; set; }
    }
    public class ObjectMetadata
    {
        public Guid? Id { get; set; }
        public Guid? DocumentIndexId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueText { get; set; }

        public ObjectMetadata()
        {

        }

        //public ObjectMetadata(DmsDocumentDetails model)
        //{
        //    Id = model.Id;
        //    DocumentIndexId = model.SysDocumentProfileDetailId;
        //    Type = model.SysDocumentProfileDetail?.FieldType;
        //    Name = model.SysDocumentProfileDetail?.FieldName;
        //    if(model.SysDocumentProfileDetail.FieldType == "H")
        //    {
        //        if(model.FieldValue != null)
        //        {
        //            var valueId = Guid.Parse(model.FieldValue);
        //            //Value = model.Field.SysDocumentProfileDetailValues.Where(i => i.Id == valueId).FirstOrDefault().FieldValue;
        //        }

        //    }
        //    else
        //    {
        //        Value = model.FieldValue;
        //    }
        //}

        //public DmsDocumentDetails CreateModel()
        //{
        //    return new DmsDocumentDetails()
        //    {
        //        Id = Id ?? Guid.NewGuid(),
        //        CreatedAt = DateTime.Now,
        //        FieldValue = Value,
        //        SysDocumentProfileDetailId = DocumentIndexId.Value
        //    };
        //}
    }

    public class DocumentManagerRootFolderRequest
    {
        public DocumentManagerRootFolderRequest()
        {

        }

        //public DocumentManagerRootFolderRequest(DmsObjects dmsObject)
        //{
        //    Id = dmsObject.Id.ToString();
        //    Name = dmsObject.Name;
        //    DocumentProfileId = dmsObject.DocumentProfileId;
        //    Description = dmsObject.Description;
        //    StorageId = dmsObject.DefaultStorageId;
        //    LimitStorage = dmsObject.LimitStorage;
        //}

        public string Id { get; set; }
        public string Name { get; set; }
        public Guid? DocumentProfileId { get; set; }
        public string Description { get; set; }
        public Guid? StorageId { get; set; }
        public int? LimitStorage { get; set; }
    }

    public class DownloadFileResult
    {
        public Guid Id { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public string Annotation { get; set; }
        public byte[] Data { get; set; }

        public DownloadFileResult()
        {

        }

        //public DownloadFileResult(DmsDocumentFiles file)
        //{
        //    Id = file.Id;
        //    Filename = file.OriginalFileName;
        //    Extension = file.Extension;
        //    ContentType = file.ContentType;
        //    Annotation = file.Annotation;
        //}
    }

    public class ResultUploadStatus
    {
        public bool Result;
        public DownloadFileResult Data;
    }

    public class DocumentManagerResponseBase
    {
        public String Id { get; set; }
        public string ParentId { get; set; }
        public bool Open { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int? Size { get; set; }
        public string Version { get; set; }
        public string Created_by { get; set; }
        public string Created_date { get; set; }
        public bool? Checkout { get; set; }
        public string Extension { get; set; }
        public bool Webix_branch { get; set; }

        public DocumentManagerResponseBase()
        {

        }

        //public DocumentManagerResponseBase(DmsObjects dmsObject)
        //{
        //    var type = "folder";
        //    if (dmsObject.Type == "D")
        //        type = "document";

        //    Id = dmsObject.Id.ToString();
        //    Open = false;
        //    Type = type;
        //    Value = dmsObject.Name;
        //    Webix_branch = true;
        //    if (dmsObject.ParentId.HasValue)
        //        ParentId = dmsObject.ParentId.ToString();
        //    else
        //        ParentId = "publicFolder";
        //    if(dmsObject.CreatedAt != null)
        //        Created_date = dmsObject.CreatedAt.Value.ToString("dd-MM-yyyy");
        //    if(dmsObject.CreatedByNavigation != null)
        //        Created_by = dmsObject.CreatedByNavigation.Fullname;
        //    //if (dmsObject.DmsDocuments?.DmsDocumentFiles?.Count() > 0)
        //    //    Extension = dmsObject.DmsDocuments.DmsDocumentFiles.FirstOrDefault().Extension;
        //    //if (dmsObject.DmsDocuments != null)
        //    //{
        //    //    Version = dmsObject.DmsDocuments?.Version;
        //    //    Size = dmsObject.DmsDocuments?.Size;
        //    //    Checkout = dmsObject.DmsDocuments?.IsCheckout;
        //    //}
        //}
    }

    public class DocumentManagerResponseData : DocumentManagerResponseBase
    {
        public List<DocumentManagerResponseBase> Data { get; set; }

        public DocumentManagerResponseData()
        {

        }

        //public DocumentManagerResponseData(DmsObjects dmsObject) : base(dmsObject)
        //{

        //}
    }

    public class DocumentManagerResponse : DocumentManagerResponseBase
    {
        public string Parent { get; set; }
        public List<DocumentManagerResponseBase> Data;

        public DocumentManagerResponse()
        {

        }
    }

}
