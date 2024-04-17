using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace digiman_common.Dto.DigiDocu
{

    public class RoleTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public RoleTable(Roles role)
        //{
        //    Id = role.Id;
        //    Name = role.Name;
        //    Description = role.Description;
        //}
    }

    public class Role 
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RoleClaims> RoleClaims { get; set; }
        public Role()
        {

        }

        //public Role(Roles role)
        //{
        //    if(role != null)
        //    {
        //        Id = role.Id;
        //        Name = role.Name;
        //        Description = role.Description;
        //        RoleClaims = role.RoleClaims.Select(i=>new RoleClaims()
        //        {
        //            //Id = i.Id,
        //            ClaimId = i.ClaimId,
        //            //ClaimsType = i.ClaimType,
        //            Claims = Utf8Json.JsonSerializer.Deserialize<List<SettingCollectionDto<string,bool>>>(i.ClaimValue)
        //    }).ToList();
                    
        //       //if (role.RoleClaims != null)
        //            //RoleClaims = Utf8Json.JsonSerializer.Deserialize<List<SysClaimSave>>(role.RoleClaims);
        //    }
        //}

        //public Roles CreateModel(Guid userId)
        //{
        //    var createdDate = DateTime.Now;
        //    return new Roles()
        //    {
        //        Id = Id ?? Guid.NewGuid(),
        //        Name = Name,
        //        NormalizedName = Name.ToUpper(),
        //        Description = Description,
        //        CreatedAt  = createdDate,
        //        CreatedBy = userId,
        //        UpdatedAt = createdDate,
        //        UpdatedBy = userId,
        //        RoleClaims = RoleClaims.Select(i => new Entities.RoleClaims()
        //        {
        //            //Id = Guid.NewGuid(),
        //            //ClaimType = i.ClaimsType,
        //            //ClaimValue = i.ClaimsValue,
        //            ClaimId = i.ClaimId,
        //            ClaimValue = Utf8Json.JsonSerializer.ToJsonString(i.Claims)
        //        }).ToList()
        //    };
        //}
    }

    public class RoleClaims
    {
        //public Guid Id { get; set; }
        public Guid ClaimId { get; set; }
        //public string ClaimsType { get; set; }
        //public string ClaimsValue { get; set; }
        public List<SettingCollectionDto<string,bool>> Claims { get; set; }
    }

    public class RoleList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    
    public class SysClaims
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int? Level { get; set; }
        public Guid? ParentId { get; set; }
        public string ClaimsType { get; set; }
        public List<SettingCollectionDto<string,bool>> ClaimsTemplateValue { get; set; }

        //public SysClaims(Entities.SysClaims m)
        //{
        //    Id = m.Id;
        //    Name = m.Name;
        //    DisplayName = m.DisplayName;
        //    Level = m.Level;
        //    ParentId = m.ParentId;
        //    ClaimsType = m.ClaimsType;

        //    //var test = new List<SysClaimValue>();
        //    //test.Add(new SysClaimValue() { Key = "read", DisplayName = "Read", Value = false });
        //    //test.Add(new SysClaimValue() { Key = "edit", DisplayName = "Edit", Value = false });

        //    //var testString = Utf8Json.JsonSerializer.ToJsonString(test);

        //    if (m.ClaimsTemplateValue != null)
        //        ClaimsTemplateValue = Utf8Json.JsonSerializer.Deserialize<List<SettingCollectionDto<string,bool>>>(m.ClaimsTemplateValue);
        //}
    }

}
