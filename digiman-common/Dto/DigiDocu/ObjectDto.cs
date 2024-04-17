using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class ObjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid? DocumentProfileId { get; set; }
        public string Description { get; set; }
        public Guid? DefaultStorageId { get; set; }
        public int? LimitStorage { get; set; }
        public bool IsInheritPermission { get; set; }
        public ObjectPermission AccessControlList { get; set; }

    }

    public class ObjectPermission
    {
        public List<ObjectPermissionPrincipal> Principal { get; set; }
        public List<ObjectPermissionOperation> Operations { get; set; }
    }

    public class ObjectPermissionOperation
    {
        public string Key { get; set; }
        public bool Value { get; set; }
    }

    public class ObjectPermissionPrincipal
    {
        public Guid PrincipalId { get; set; }
        public string PrincipalType { get; set; }
    }
}
