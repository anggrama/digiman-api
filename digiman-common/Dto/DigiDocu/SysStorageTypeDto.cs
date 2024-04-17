using System;
using System.Collections.Generic;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class SysStorageTypeList
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public List<SysStorageOptionsTemplate> OptionTemplate { get; set; }

    }

    public class SysStorageOptionsTemplate
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DefaultValue { get; set; }
        public string InputType { get; set; }
    }
}
