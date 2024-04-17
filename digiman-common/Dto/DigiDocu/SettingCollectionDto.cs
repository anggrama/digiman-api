using System;
using System.Collections.Generic;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{
    public class SettingCollectionDto<K,V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }

    public class SettingCollectionTemplateDto<K, D>
    {
        public K Key { get; set; }
        public D DisplayName { get; set; }
    }

}
