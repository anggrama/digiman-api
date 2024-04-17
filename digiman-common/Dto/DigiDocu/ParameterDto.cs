using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace digiman_common.Dto.DigiDocu
{

    public class SysParameter
    {
        public SysParameter()
        {

        }
        //public SysParameter(Entities.SysParameters m)
        //{
        //    Id = m.Id;
        //    Type = m.Type;
        //    Name = m.Name;
        //    Value = m.Value;
        //}
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        public string Description { get; set; }
    }

    public class ParameterTable 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public ParameterTable()
        {

        }

    }
}
