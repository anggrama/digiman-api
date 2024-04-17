using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiDocu.Api.Models
{
    public class DocumentViewerConfiguration
    {
        public Guid DocumentId;
        public Guid? FileId;
        public string SessionId;
        public string ControlId;
    }
}
