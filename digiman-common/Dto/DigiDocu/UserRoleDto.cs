using System;
using System.Collections.Generic;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class UserRole
    {
        public UserRole()
        {

        }

        public Guid? UserId { get; set; }
        public Guid? RoleId { get; set; }
    }
}
