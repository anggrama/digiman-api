using digiman_common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace digiman_common.Dto.DigiDocu
{
    public class User
    {
        [Key]
        public Guid? Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Status { get; set; }
        public bool IsLdap { get; set; }
        public bool IsLock { get; set; }
        public DateTime? LastLogin { get; set; }
        public List<Group> Groups { get; set; }
        public List<Role> Roles { get; set; }

        public User()
        {
            Groups = new List<Group>();
            Roles = new List<Role>();
        }

        //public User(Users model)
        //{
        //    Id = model.Id;
        //    Username = model.Username;
        //    FullName = model.Fullname;
        //    Email = model.Email;
        //    PhoneNumber = model.PhoneNumber;
        //    Status = model.RecordStatus;
        //    LastLogin = model.LastLogin;
        //    IsLdap = model.IsLdap;
        //    IsLock = model.LockoutEnabled;
        //    Groups = model.UserGroups.Select(i => new Group(i.Group)).ToList();
        //    Roles = model.UserRoles.Select(i => new Role(i.Role)).ToList();
        //}

        //public Users CreateModel()
        //{
        //    return new Users()
        //    {
        //        Id = Id ?? Guid.NewGuid(),
        //        Fullname = FullName,
        //        Email = Email,
        //        PhoneNumber = PhoneNumber,
        //        Username = Username,
        //        IsLdap = IsLdap,
        //        LockoutEnabled = IsLock,
        //        RecordStatus = Status
        //    };
        //}
    }

    public class UserSelect
    {
        [IgnoreDataMember]
        public Guid Id { get; set; }
        public string Fullname { get; set; }

        //public UserSelect(Users model)
        //{
        //    Id = model.Id;
        //    Name = model.Fullname;
        //}

        public UserSelect()
        {

        }
    }

    public class UserTable
    {
        public Guid Id;
        public string Username;
        public string FullName;
        public string UserGroup;
        public string Email;
        public string Status;
        public bool Lockout;
        public string LastLogin;

        public UserTable()
        {

        }

        //public UserTable(Users user)
        //{
        //    Id = user.Id;
        //    Username = user.Username;
        //    FullName = user.Fullname;
        //    Email = user.Email;
        //    Status = user.RecordStatus;
        //    Lockout = false;

        //    if (user.UserGroups.Count > 0)
        //        UserGroup = user.UserGroups.FirstOrDefault().Group.Name;
        //    else
        //        UserGroup = "~ No Group ~";

        //    if (user.LastLogin != null)
        //        LastLogin = user.LastLogin.Value.ToString("dd-MM-yyyy HH:mm:ss");
        //    else
        //        LastLogin = "Unused";
        //}
    }

    public class UserList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
