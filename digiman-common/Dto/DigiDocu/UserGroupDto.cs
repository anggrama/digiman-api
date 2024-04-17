using System;
using System.Collections.Generic;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class UserGroup
    {
        public UserGroup()
        {

        }

        public Guid? UserId { get; set; }
        public Guid? GroupId { get; set; }
    }
}
