using digiman_common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace digiman_common.Dto.DigiDocu
{

    #region "-- Document Request --"

    public class DocumentRequest : ObjectDto
    {
        public string ReferenceNumber { get; set; }
        public string Priority { get; set; }
        public DocumentReminder Reminder { get; set; }
        public List<DocumentDetails> Metadata { get; set; }
        public List<DocumentRelations> Relation { get; set; }
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }

    }

    #endregion

    #region "-- Document Response --"

    #endregion




    public class DocumentResponse : DocumentRequest
    {
        public DocumentResponse()
        {

        }
        //public DocumentResponse(DmsObjects dmsObject)
        //{
        //    Id = dmsObject.Id;
        //    Name = dmsObject.Name;
        //    //LocationId = dmsObject.ParentId;
        //    DocumentProfileId = dmsObject.DocumentProfileId;
        //    Description = dmsObject.Description;
        //    //ReferenceNumber = dmsObject.DmsDocuments?.ReferenceNumber;
        //    //EffectiveStartDate = dmsObject.DmsDocuments?.EffectiveStartDate;
        //    //EffectiveEndDate = dmsObject.DmsDocuments?.EffectiveEndDate;
        //    ////NotificationEmail = dmsObject.DmsDocuments?.EmailNotification;
        //    ////NotificationSystem = dmsObject.DmsDocuments?.SystemNotification;
        //    //Priority = dmsObject.DmsDocuments?.Priority;
        //    //Metadata = dmsObject.DmsObjectDetails?.Select(p => new ObjectMetadata(p)).ToList();
        //    LocationText = dmsObject.Root?.Name;
        //    //ReminderPersons = dmsObject.DmsDocument?.ReminderPersons.Select(i => new DocumentReminderPersons(i)).ToList();
        //    //ReminderTimes = dmsObject.DmsDocument?.ReminderTimes.Select(i => new DocumentReminderTimes(i)).ToList();
        //}

        //public DocumentResponse(DmsDocuments dmsDocument)
        //{
        //    Id = dmsDocument.Object.Id;
        //    Name = dmsDocument.Object.Name;
        //    //LocationId = dmsDocument.Object.ParentId;
        //    DocumentProfileId = dmsDocument.Object.DocumentProfileId;
        //    DocumentProfileName = dmsDocument.Object?.DocumentProfile?.Name;
        //    Description = dmsDocument.Object.Description;
        //    //ReferenceNumber = dmsDocument.ReferenceNumber;
        //    EffectiveStartDate = dmsDocument.EffectiveStartDate;
        //    EffectiveEndDate = dmsDocument.EffectiveEndDate;
        //    //NotificationEmail = dmsDocument.EmailNotification;
        //    //NotificationSystem = dmsDocument.SystemNotification;
        //    Priority = dmsDocument.Priority;
        //    LocationText = dmsDocument.Object.Root?.Name;
        //    UpdatedAt = dmsDocument.UpdatedAt.HasValue ? dmsDocument.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm") : null;
        //    CreatedAt = dmsDocument.CreatedAt.HasValue ? dmsDocument.CreatedAt.Value.ToString("dd/MM/yyyy HH:mm") : null;
        //    OwnerName = dmsDocument.Object?.CreatedByNavigation?.Username;
        //    Version = dmsDocument.Version;
        //    Metadata = dmsDocument.DmsDocumentDetails.Select(p => new DocumentDetails(p)).ToList();
        //    DocumentFiles = dmsDocument.DmsDocumentFiles.Select(i => new DocumentFiles(i)).ToList();
        //    //Metadata = dmsDocument.Object.DmsObjectDetails.Select(p => new ObjectMetadata(p)).ToList();
        //    //ReminderTimes = dmsDocument.ReminderTimes.Select(i => new DocumentReminderTimes(i)).ToList();
        //    //ReminderPersons = dmsDocument.ReminderPersons.Select(i => new DocumentReminderPersons(i)).ToList();
        //}

        public string LocationText { get; set; }
        public string LocationPath { get; set; }
        public string DocumentProfileName { get; set; }
        public string OwnerName { get; set; }
        public string UpdatedAt { get; set; }
        public string CreatedAt { get; set; }
        public string Version { get; set; }
        public List<DocumentFiles> DocumentFiles { get; set; }

    }

    public class DocumentDetails
    {
        public DocumentDetails()
        {
        }

        //public DocumentDetails(DmsDocumentDetails detail)
        //{
        //    Id = detail.Id;
        //    Caption = detail.SysDocumentProfileDetail?.FieldName;
        //    if (detail.SysDocumentProfileDetail?.FieldType == "S")
        //        Value = detail.FieldValue;
        //    else if (detail.SysDocumentProfileDetail?.FieldType == "D")
        //        Value = detail.DateValue.HasValue ? detail.DateValue.Value.ToString("dd/MM/yyyy") : null;
        //    else
        //        Value = detail.NumericValue.HasValue ? detail.NumericValue.Value.ToString("dd/MM/yyyy") : null;
        //}

        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string Value { get; set; }
    }

    public class DocumentRelations
    {
        public Guid Id { get; set; }
        public Guid ObjectRelationId { get; set; }


    }

    public class DocumentFiles
    {
        public DocumentFiles()
        {

        }

        //public DocumentFiles(DmsDocumentFiles i)
        //{
        //    FileId = i.Id;
        //    FileName = i.OriginalFileName;
        //    var sizeConverter = new Helper.SizeConverter();
        //    Size = sizeConverter.SizeSuffix(Convert.ToInt64(i.Size), 2);
        //    Extension = i.Extension;
        //}

        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Extension { get; set; }
    }
  

    #region "-- Reminder Dto --"

    public class DocumentReminder
    {
        public List<DocumentReminderChannel> Channel { get; set; }
        public List<DocumentReminderTimes> Period { get; set; }
        public List<DocumentReminderPerson> RemindsTo { get; set; }
       
        public DocumentReminder()
        {

        }
     
    }

    public class DocumentReminderChannel
    {
        public string ChannelType { get; set; }
    }

    public class DocumentReminderTimes
    {
        public int PeriodTimes { get; set; }
        public string Interval { get; set; }
    }

    public class DocumentReminderPerson
    {
        public string PersonId { get; set; }
    }


    #endregion



}
