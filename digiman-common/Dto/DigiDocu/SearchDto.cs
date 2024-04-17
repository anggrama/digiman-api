using digiman_common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{

    public class DocumentSearchRequestData
    {
        public string SimpleSearch { get; set; }
        public string DocumentName { get; set; }
        public string ReferenceNumber { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? DocumentProfileId { get; set; }
        public List<object> Metadata { get; set; }
    }

    public class DocumentSearchRequestData2
    {

        public int Draw { get; }
        public int Start { get; }
        public int Length { get; }
        //public Search Search { get; }
        public List<object> Orders { get; }
        public List<object> Columns { get; }
        public string SimpleSearch { get; set; }
        public string DocumentName { get; set; }
        public string ReferenceNumber { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? DocumentProfileId { get; set; }
        public List<object> Metadata { get; set; }
    }

    public class DocumentSearchResultData
    {
        //public DocumentSearchResultData(DmsDocuments i)
        //{
        //    Id = i.Id;
        //    Title = i.Object.Name ?? "-";
        //    LocationName = i.Object?.ParentId.ToString();
        //    ProfileName = i.Object?.DocumentProfile?.Name ?? "-";
        //    ReferenceNumber = i.ReferenceNumber ?? "-";
        //    Description = i.Object.Description ?? "-";
        //    if (i.EffectiveStartDate.HasValue && i.EffectiveEndDate.HasValue)
        //        EffectiveDate = $"{i.EffectiveStartDate.Value.ToString("dd/MM/yyyy")} - {i.EffectiveEndDate.Value.ToString("dd/MM/yyyy")}";
        //    else
        //        EffectiveDate = "-";
        //    Contents = "-";
        //    Status = i.Object.RecordStatus ?? "-";
        //    var sizeConverter = new Helper.SizeConverter();
        //    Size = sizeConverter.SizeSuffix(Convert.ToInt64(i.DmsDocumentFiles.Sum(j => j.Size)), 2);
        //    UpdatedDate = i.UpdatedAt.HasValue ? i.UpdatedAt.Value.ToString("dd/MM/yyyy") : "-";
        //}

        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string LocationName { get; set; }
        public string ProfileName { get; set; }
        public string ReferenceNumber { get; set; }
        public string Description { get; set; }
        public string EffectiveDate { get; set; }
        public string Contents { get; set; }
        public string Status { get; set; }
        public string Size { get; set; }
        public string UpdatedDate { get; set; }
    }
}
