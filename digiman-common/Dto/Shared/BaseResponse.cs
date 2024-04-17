using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digiman_common.Dto.Shared
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public BaseMetadata Metadata { get; set; }
    }

    public class BaseMetadata
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "OK";
        public string Exception { get; set; } = "";

    }
}
