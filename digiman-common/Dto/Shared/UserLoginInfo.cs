using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digiman_common.Dto.Shared
{
    public class UserLoginInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
    }
}
