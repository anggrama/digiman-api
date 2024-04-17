using System;
using System.Collections.Generic;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class SysProcessQueue
    {
        public Guid Id { get; set; }
        public string QueueType { get; set; }
        public string QueueContent { get; set; }

        //public Entities.SysProcessQueue CreateModel(Guid userId)
        //{
        //    var createdDate = DateTime.Now;
        //    var model = new Entities.SysProcessQueue()
        //    {
        //        Id = Guid.NewGuid(),
        //        CreatedBy = userId
        //    };

        //    return model;
        //}

    }

    public class SysProcessQueueEmail
    {
        public string DestinationEmail { get; set; }
        public string FromEmail { get; set; }
        public string Content { get; set; }
    }

    public class SysProcessFile
    {
        public string DmsDocumentFileId { get; set; }
        public string ConvertedFilename { get; set; }
        public string Extension { get; set; }
    }
    //public class SysProcessQueueOcr
    //{
    //    public string ConvertedFilename { get; set; }
    //    public string Extension { get; set; }
    //}

    //public class SysProcessQueueEncrypt
    //{
    //    public string ConvertedFilename { get; set; }
    //    public string Extension { get; set; }
    //}
}
