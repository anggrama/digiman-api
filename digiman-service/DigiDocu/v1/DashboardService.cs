using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiDocu.Common.Enums;

namespace digiman_service.DigiDocu.v1
{
    public class DashboardService : BaseService
    {

        public DashboardService()
        {
        }
        public int NumberOfDocument()
        {
            //return _documentRepository.Documents()
            //    .Where(i => i.DmsObject.RecordStatus == "A")
            //    .Count();

            return 0;
        }
        public int ActiveDocument()
        {
            //return _documentRepository.Documents()
            //    .Where(i => i.DmsObject.RecordStatus == Convert.ToString((char)DmsObjectRecordStatusEnum.Active))
            //    .Count();

            return 0;
        }
        public int InactiveDocument()
        {
            //return _documentRepository.Documents()
            //    .Where(i => i.DmsObject.RecordStatus != Convert.ToString((char)DmsObjectRecordStatusEnum.Active))
            //    .Count();

            return 0;
        }
    }
}
