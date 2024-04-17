using System;
using System.Collections.Generic;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class UserPermissionDto
    {
        public Guid Id { get; set; } //User Id
        public string PermissionType { get; set; }
        public List<SettingCollectionDto<string, bool>> PermissionValue { get; set; }
    }
}
