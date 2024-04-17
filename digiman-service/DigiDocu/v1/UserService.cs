using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using digiman_common.Dto.DigiDocu;
using digiman_dal.Extensions;
using Microsoft.Extensions.Configuration;
using digiman_common.Dto.Shared;
using AutoMapper;

namespace digiman_service.DigiDocu.v1
{
    public class UserService : BaseService
    {
        Mapper _mapper;
        public UserService(UserLoginInfo user) :  base(user)
        {
            var mapperConfig = new MapperConfiguration(cfg =>cfg.CreateMap<digiman_dal.Models.User, digiman_common.Dto.DigiDocu.User>());
            _mapper = new Mapper(mapperConfig);
        }

        public UserService()
        {

        }


        //public DataTablesResponse<User> GetAllTable(DataTablesRequest request)
        //{
        //    try
        //    {
        //        var data = _context.Users.Where(i=>i.DeletedAt == null);
        //        //var data = user.Select(p => new Common.Dto.User(p));

        //        IQueryable<digiman_dal.Models.User> filteredData;
        //        if (request.Search == null || (String.IsNullOrWhiteSpace(request.Search.Value)))
        //            filteredData = data;
        //        else
        //            filteredData = data.SearchAll(request.Search.Value);

        //        if (request.Orders.Count() > 0)
        //        {
        //            var firstOrder = request.Orders.FirstOrDefault();
        //            var col = request.Columns.ToArray()[firstOrder.Column];
        //            filteredData = filteredData.OrderBy($"{col.Name} {firstOrder.Dir.ToUpper()}");
        //        }

        //        var dataPage = filteredData.Skip(request.Start).Take(request.Length);
        //        return dataPage
        //            .Select(p => new Common.Dto.User(p))
        //            .ToDataTablesResponse(request, data.Count(), filteredData.Count());
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public async Task<List<UserList>> GetList()
        {
            try
            {
                var result = await _context.Users.Where(i=>i.DeletedBy != null).Select(i=> new UserList() {
                    Id = i.Id,
                    Name = i.Fullname
                }).ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<User?> GetById(Guid id)
        {
            try
            {
                var dbData = await _context.Users.Where(i=>i.Id == id).FirstOrDefaultWithNoLockAsync();
                if (dbData == null)
                    return null;
                else
                    return _mapper.Map<digiman_common.Dto.DigiDocu.User>(dbData);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Guid> CreateAsync(User dto)
        {
            try
            {
                //check password confirmation
                if (dto.Password != dto.ConfirmPassword)
                    throw new Exception("The password and confirmation password do not match.");
                //username validation
                if (string.IsNullOrWhiteSpace(dto.Username))
                    
                    throw new Exception(digiman_common.Handler.MessageHandler.OperationMessageHandler.GetOperationMessage(digiman_common.Handler.MessageHandler.OperationStatus.CannotNull));
                var isUsernameExist = await _context.Users.Where(i => i.Username == dto.Username && i.DeletedAt == null).CountWithNoLockAsync();
                if (isUsernameExist != 0)
                    throw new Exception(digiman_common.Handler.MessageHandler.OperationMessageHandler.GetOperationMessage(digiman_common.Handler.MessageHandler.OperationStatus.DuplicateNotAllowed));

                //var user = _mapper.Map<digiman_dal.Models.User>(dto);

                var dbUser = new digiman_dal.Models.User()
                {
                    Id = Guid.NewGuid(),
                    Username = dto.Username,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Username + dto.Password),
                    Fullname = dto.FullName,
                    PhoneNumber = dto.PhoneNumber,
                    IsLdap = dto.IsLdap,
                    RecordStatus = dto.Status
                };


                await _context.Users.AddAsync(dbUser);
                await _context.SaveChangesAsync();

                return dbUser.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> UpdateAsync(Guid id, User dto)
        {
            try
            {
                //username validation
                var isUsernameExist = await _context.Users.Where(p => p.Username == dto.Username && p.DeletedAt == null).CountWithNoLockAsync();
                var dbData = await _context.Users.Where(i=>i.Id == id).FirstOrDefaultWithNoLockAsync();

                if (string.IsNullOrWhiteSpace(dto.Username))
                    throw new Exception(digiman_common.Handler.MessageHandler.OperationMessageHandler.GetOperationMessage(digiman_common.Handler.MessageHandler.OperationStatus.CannotNull));
                if (isUsernameExist > 0 && dbData.Username != dto.Username)
                    throw new Exception(digiman_common.Handler.MessageHandler.OperationMessageHandler.GetOperationMessage(digiman_common.Handler.MessageHandler.OperationStatus.DuplicateNotAllowed));

                //update user handler
                dbData.UpdatedAt = DateTime.UtcNow;

                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    //check password confirmation
                    if (dto.Password != dto.ConfirmPassword)
                        throw new Exception("The password and confirmation password do not match.");
                    dbData.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Username + dto.Password);
                }

                //clear user group and user roles
                //dbData.UserGroups.Clear();
                //dbData.UserRoles.Clear();

                //foreach (var group in dto.Groups)
                //{
                //    var userGroup = new UserGroups()
                //    {
                //        UserId = id,
                //        GroupId = group.Id.Value
                //    };
                //    editedData.UserGroups.Add(userGroup);
                //}
                //foreach (var role in dto.Roles)
                //{
                //    var userRole = new UserRoles()
                //    {
                //        UserId = id,
                //        RoleId = role.Id.Value
                //    };
                //    editedData.UserRoles.Add(userRole);
                //}


                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Delete(Guid id)
        {
            try
            {
                var user = await _context.Users.Where(i=>i.Id == id).FirstOrDefaultWithNoLockAsync();

                if (user == null)
                    throw new Exception("Data not found");

                user.DeletedAt = DateTime.UtcNow;
                user.DeletedBy = _loggedUserInfo.UserId;
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<UserLoginInfo?> GetCurrentUserInfo(Guid userId)
        {
            try
            {
                var dbData = await GetById(userId);
                if (dbData == null)
                    return null;
                else
                    return new UserLoginInfo()
                    {
                         UserId = dbData.Id.Value,
                         Fullname = dbData.FullName,
                         UserName = dbData.Username
                    };
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<UserSelect> AuthenticateUser(string user, string password)
        {
            try
            {
                var data = await _context.Users.Where(i => i.Username == user).FirstOrDefaultWithNoLockAsync();
                if (data == null)
                    throw new Exception("User / password Invalid");

                var isMatchPass = BCrypt.Net.BCrypt.Verify(password, data.PasswordHash);
                if (isMatchPass == false)
                    throw new Exception("User / password Invalid");
               
                if (data.LockoutEnabled == true)
                    throw new Exception("User Locked");

                return new UserSelect()
                {
                    Id = data.Id,
                    Fullname = data.Fullname
                };
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
