using digiman_common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    //public class DocumentProfileValue
    //{
    //    public Guid? Id { get; set; }
    //    public string Value { get; set; }
    //    public Guid UdfDetailId { get; set; }

    //    public DocumentProfileValue()
    //    {

    //    }

    //    public DocumentProfileValue(SysDocumentProfileDetailValue model)
    //    {
    //        Id = model.Id;
    //        Value = model.FieldValue;
    //        UdfDetailId = model.SysDocumentProfileDetailId.Value;
    //    }

    //    public SysDocumentProfileDetailValue CreateModel()
    //    {
    //        return new SysDocumentProfileDetailValue()
    //        {
    //            Id = Id ?? Guid.NewGuid(),
    //            FieldValue = Value,
    //            DefaultValue = false,
    //            SysDocumentProfileDetailId = UdfDetailId
    //        };
    //    }

    //    public SysDocumentProfileDetailValue CreateModel(Guid userExecutorId)
    //    {
    //        return new SysDocumentProfileDetailValue()
    //        {
    //            Id = Id ?? Guid.NewGuid(),
    //            FieldValue = Value,
    //            DefaultValue = false,
    //            SysDocumentProfileDetailId = UdfDetailId,
    //            CreatedAt = DateTime.Now,
    //            CreatedBy = userExecutorId
    //        };
    //    }
    //}

    //public class SelectionsVm
    //{
    //    public List<DocumentProfileValue> Selections { get; set; }
    //    public List<DocumentProfileValue> DeletedSelections { get; set; }
    //}

    public class DocumentProfileDetail
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? Mandatory { get; set; }
        public string FieldOptions { get; set; }
        public Guid? DocumentProfileId { get; set; }
        //public SelectionsVm Selections { get; set; }
        public int DisplaySequence {get;set;}

        public DocumentProfileDetail()
        {

        }

        //public DocumentProfileDetail(SysDocumentProfileDetail model)
        //{
        //    Id = model.Id;
        //    Name = model.FieldName;
        //    Type = model.FieldType;
        //    Mandatory = model.Mandatory;
        //    FieldOptions = model.FieldOptions;
        //    DocumentProfileId = model.DocumentProfileId;
        //    DisplaySequence = model.DisplaySeq.Value;
        //    //Selections = new SelectionsVm()
        //    //{
        //    //    Selections = model.SysDocumentProfileDetailValues.Select(i => new DocumentProfileValue(i)).ToList(),
        //    //    DeletedSelections = new List<DocumentProfileValue>()
        //    //};
        //}

        //public SysDocumentProfileDetail CreateModel(Guid documentProfileId,int displaySequence = 0)
        //{
        //    return new SysDocumentProfileDetail()
        //    {
        //        Id = Id ?? Guid.NewGuid(),
        //        DocumentProfileId = documentProfileId,
        //        FieldName = Name,
        //        FieldType = Type,
        //        Mandatory = Mandatory,
        //        DisplaySeq = displaySequence,
        //        FieldOptions = FieldOptions//,
        //        //SysDocumentProfileDetailValues = Selections.Selections.Select(i=> i.CreateModel()).ToList()
        //    };
        //}

        //public SysDocumentProfileDetail CreateModel(int displaySequence = 0, Guid? userExecutorId = null)
        //{
        //    return new SysDocumentProfileDetail()
        //    {
        //        Id = Id ?? Guid.NewGuid(),
        //        DocumentProfileId = DocumentProfileId.Value,
        //        FieldName = Name,
        //        FieldType = Type,
        //        Mandatory = Mandatory,
        //        DisplaySeq = displaySequence,
        //        FieldOptions = FieldOptions//,
        //        //SysDocumentProfileDetailValues = Selections.Selections.Select(i => i.CreateModel(userExecutorId.Value)).ToList()
        //    };
        //}
    }
}