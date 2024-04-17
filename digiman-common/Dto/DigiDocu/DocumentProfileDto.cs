using digiman_common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class DocumentProfile
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AutoNumberFormat { get; set; }
        public Guid? ParentProfileId { get; set; }
        public bool IsSetDocumentProfile { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string WatermarkFile { get; set; }
        public List<DocumentProfileDetail> Fields { get; set; }
        public List<DocumentProfileDetail> DeletedFields { get; set; }


        public DocumentProfile()
        {

        }

        //public DocumentProfile(SysDocumentProfiles entity)
        //{
        //    Id = entity.Id;
        //    Name = entity.Name;
        //    Description = entity.Description;
        //    ParentProfileId = entity.ParentProfileId;
        //    IsSetDocumentProfile = entity.IsSetDocumentProfile;
        //    AutoNumberFormat = entity.AutoNumberFormat;
        //    WatermarkFile = entity.WatermarkFile;
        //    Fields = entity.SysDocumentProfileDetail.Select(i => new DocumentProfileDetail(i)).OrderBy(i=>i.DisplaySequence).ToList();
        //    DeletedFields = new List<DocumentProfileDetail>();
        //}

        //public SysDocumentProfiles CreateModel(Guid userId)
        //{
        //    return new SysDocumentProfiles()
        //    {
        //        Id = Id ?? Guid.NewGuid(),
        //        Name = Name,
        //        Description = Description,
        //        ParentProfileId = ParentProfileId,
        //        IsSetDocumentProfile = IsSetDocumentProfile,
        //        RecordStatus = "A",
        //        AutoNumberFormat = AutoNumberFormat,
        //        CreatedBy = userId,
        //        CreatedAt = DateTime.Now,
        //        UpdatedBy = userId,
        //        UpdatedAt = DateTime.Now
                
        //    };
        //}
    }

    public class DocumentProfileSelect
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public DocumentProfileSelect()
        {

        }

        //public DocumentProfileSelect(SysDocumentProfiles documentProfile)
        //{
        //    Id = documentProfile.Id;
        //    Name = documentProfile.Name;
        //}
    }

    public class DocumentProfileTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DocumentProfileTable()
        {

        }

        //public DocumentProfileTable(SysDocumentProfiles documentProfile)
        //{
        //    Id = documentProfile.Id;
        //    Name = documentProfile.Name;
        //    Description = documentProfile.Description;
        //}
    }

    public class DocumentProfileWatermark
    {
        public byte[] Watermark { get; set; }

        public string ContentType { get; set; }
    }
}
