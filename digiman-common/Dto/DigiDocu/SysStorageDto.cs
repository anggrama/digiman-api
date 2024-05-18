
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class SysStorageCreate
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? StorageTypeId { get; set; }
        public List<SettingCollectionDto<string,string>>? StorageOptions { get; set; }
        public bool IsActive { get; set; }

        //public SysStorage(Entities.SysStorages storage)
        //{
        //    Id = storage.Id;
        //    Name = storage.Name;
        //    StorageTypeId = storage.StorageTypeId;
        //    StorageOptions = Utf8Json.JsonSerializer.Deserialize<List<SettingCollectionDto<string,string>>>(storage.StorageOptions);
        //    IsActive = storage.IsActive;

        //}

        //public Entities.SysStorages CreateModel(Guid userId)
        //{
        //    var createdDate = DateTime.Now;
        //    var model = new Entities.SysStorages()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = Name,
        //        StorageTypeId = StorageTypeId,
        //        StorageOptions = Utf8Json.JsonSerializer.ToJsonString(StorageOptions),
        //        CreatedAt = createdDate,
        //        UpdatedAt = createdDate,
        //        CreatedBy = userId,
        //        UpdatedBy = userId,
        //        IsActive = true,
        //    };
          
        //    return model;
        //}

    }

    public class SysStorageUpdate
    {
        public string Name { get; set; }
        public Guid? StorageTypeId { get; set; }
        public List<SettingCollectionDto<string, string>>? StorageOptions { get; set; }
        public bool IsActive { get; set; }

    }

    public class SysStorageResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public Guid? StorageTypeId { get; set; }
        public string? StorageOptions { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class SysStorageList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FreeSpace { get; set; }
        public string UsedSpace { get; set; }
        public string IconClass { get; set; }
        public int FileCount { get; set; }
    }
}
