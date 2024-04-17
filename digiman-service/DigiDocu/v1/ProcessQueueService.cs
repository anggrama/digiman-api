using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using DigiDocu.Common.Entities;
using DigiDocu.Common.Repositories;
using DigiDocu.DataAccess.Repositories;

namespace digiman_service.DigiDocu.v1
{
    public class ProcessQueueService
    {
        private readonly ISysProcessQueueRepository _sysProcessQueueRepository;
        Guid _currentUserId;
        public ProcessQueueService(Guid currentUserId)
        {
            _sysProcessQueueRepository = new SysProcessQueueRepository();
            _currentUserId = currentUserId;
        }

        public void CreateAsync(Common.Dto.SysProcessQueue dto)
        {
            try
            {
                var processQueue = dto.CreateModel(_currentUserId);
                processQueue.QueueType = dto.QueueType;
                if (dto.QueueType == "E")
                    processQueue.QueueContent = "";
                else
                    processQueue.QueueContent = "";

                 _sysProcessQueueRepository.AddAsync(processQueue);
                 _sysProcessQueueRepository.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SysProcessQueue> GetList()
        {
            try
            {
                return _sysProcessQueueRepository.GetWithCondition(i=>i.FinishedDate != null).OrderBy(i => i.CreatedDate).Take(5).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
