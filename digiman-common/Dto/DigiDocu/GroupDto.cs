using digiman_common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace digiman_common.Dto.DigiDocu
{
    public class Group 
    {
        [Key]
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RoleList> Roles { get; set; }

        public Group()
        {

        }

        //public Group(Groups model)
        //{
        //    if(model != null)
        //    {
        //        Id = model.Id;
        //        Name = model.Name;
        //        Description = model.Description;
        //        Roles = model.GroupRoles.Select(i => new RoleList() {
        //            Id = i.RoleId,
        //            Name = i.Role.Name
        //        }).ToList();
        //    }
        //}

        //public Groups CreateModel(Guid userId)
        //{
        //    return new Groups()
        //    {
        //        Id = Id ?? Guid.NewGuid(),
        //        Name = Name,
        //        Description = Description,
        //        CreatedAt = DateTime.Now,
        //        CreatedBy = userId,
        //        UpdatedAt = DateTime.Now,
        //        UpdatedBy = userId
        //    };
        //}
       
    }

    public class GroupList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
