using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using DigiDocu.DataAccess.Repositories;
using DigiMan.Common.MessageHandler;
using DigiDocu.Common.Repositories;
using DigiDocu.BusinessLayer.Extensions;
using DigiDocu.Storage.Interface;
using DigiDocu.Storage.Settings;
using Microsoft.AspNetCore.Http;

namespace digiman_service.DigiDocu.v1
{
    public class StorageService : BaseService 
    {

        private readonly IStorageRepository _storageRepository;
        private readonly ISysStorageTypeRepository _sysStorageTypeRepository;
        Guid _currentUserId;

        //CRUD

        public StorageService()
        {
            _storageRepository = new StorageRepository();
            _sysStorageTypeRepository = new SysStorageTypeRepository();
        }

        public StorageService(Guid currentUserId)
        {
            _storageRepository = new StorageRepository();
            _sysStorageTypeRepository = new SysStorageTypeRepository();
            _currentUserId = currentUserId;
        }
        public async Task<Guid> CreateAsync(Common.Dto.SysStorage dto)
        {
            try
            {
                //name validation
                var countValidation = _storageRepository.GetWithCondition(i => i.Name == dto.Name && i.DeletedAt == null).Count();
                if (countValidation != 0)
                    throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DuplicateNotAllowed));

                var model = dto.CreateModel(_currentUserId);
               
                await _storageRepository.AddAsync(model);
                await _storageRepository.SaveAsync();

                return model.Id;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> UpdateAsync(Guid id, Common.Dto.SysStorage dto)
        {
            try
            {
                //name validation
                var countValidation = _storageRepository.GetWithCondition(i => i.Name == dto.Name && i.DeletedAt == null && i.Id != id).Count();
                if (countValidation > 0)
                    throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DuplicateNotAllowed));

                //update group header
                var findEntity = _storageRepository.GetById(id);
                Common.Entities.SysStorages editedData = findEntity.UpdateWithDto(dto);
                editedData.UpdatedAt = DateTime.UtcNow;
                editedData.UpdatedBy = _currentUserId;

                _storageRepository.Update(editedData);

                return await _storageRepository.SaveAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Delete(Guid id)
        {
            try
            {
                var data = _storageRepository.GetById(id);
                if (data == null)
                {
                    throw new Exception("Data not found");
                }

                data.DeletedAt = DateTime.UtcNow;
                data.DeletedBy = _currentUserId;
                _storageRepository.Update(data);
                _storageRepository.SaveAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public Common.Dto.SysStorage GetById(Guid id)
        {
            try
            {
                var model = _storageRepository.GetById(id);
                if (model == null)
                    return null;

                var optionsTemplate = Newtonsoft.Json.Linq.JArray.Parse(model.StorageType.OptionsTemplate);
                var options = Newtonsoft.Json.Linq.JArray.Parse(model.StorageOptions);
                
                optionsTemplate.Merge(options, new Newtonsoft.Json.Linq.JsonMergeSettings { MergeArrayHandling = Newtonsoft.Json.Linq.MergeArrayHandling.Merge });
                model.StorageOptions = optionsTemplate.ToString();

                return new Common.Dto.SysStorage(model);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public IEnumerable<Common.Dto.StorageList> GetAll()
        {
            return _storageRepository.GetAll().Where(i=>i.DeletedAt == null).Select(i => new Common.Dto.StorageList
            {
                Id = i.Id,
                Name = i.Name,
                FreeSpace = "0 MB",
                UsedSpace = "0 MB",
                IconClass = i.StorageType.IconClass,
                FileCount = 100
            }).ToList();
        }
        public IEnumerable<Common.Dto.StorageList> GetByType(string Type)
        {
            return _sysStorageTypeRepository.GetAll().Select(i => new Common.Dto.StorageList
            {
                Id = i.Id,
                Name = i.Name
            }).ToList();
        }
        public IEnumerable<Common.Dto.SysStorageTypeList>GetTypeList()
        {
            return _sysStorageTypeRepository.GetAll().Select(i => new Common.Dto.SysStorageTypeList
            {
                Id = i.Id,
                DisplayName = i.DisplayName,
                OptionTemplate = Utf8Json.JsonSerializer.Deserialize<List<Common.Dto.SysStorageOptionsTemplate>>(i.OptionsTemplate)
            }).ToList();
        }
        // END CRUD


        //FILE STORAGE
        public async Task<byte[]> GetFile(Guid storageId, string filename)
        {
            try
            {
                var storage = _storageRepository.GetById(storageId);
                var fileStorageService = new Storage.FileStorageService(storage.StorageType.Name, storage.StorageOptions);
                return await fileStorageService.GetFileFromStorage(filename);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UploadFile(Guid storageId,string filename,IFormFile file,Common.Dto.DocumentRequest metadata = null)
        {
            try
            {
                var storage = _storageRepository.GetById(storageId);
                var fileStorageService = new Storage.FileStorageService(storage.StorageType.Name, storage.StorageOptions);
                fileStorageService.UploadToStorage(file,filename,metadata);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END FILE STORAGE

        
    }
}
