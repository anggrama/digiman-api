using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using DataTables.AspNetCore.Mvc.Binder;

using DigiDocu.BusinessLayer.Extensions;
using DigiDocu.Common.Entities;
using DigiDocu.Common.Repositories;
using DigiDocu.DataAccess.Repositories;
using DigiMan.Common.MessageHandler;
using NinjaNye.SearchExtensions;

namespace digiman_service.DigiDocu.v1
{
    public class RoleService : BaseService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ISysClaimRepository _sysClaimRepository;
        Guid _currentUserId;

        public RoleService(Guid currentUserId)
        {
            _roleRepository = new RoleRepository();
            _sysClaimRepository = new SysClaimRepository();
            _currentUserId = currentUserId;

        }

        public DataTablesResponse<Common.Dto.RoleTable> GetAllTable(DataTablesRequest request)
        {
            try
            {
                var roles = _roleRepository.GetAll().Where(i=>i.DeletedAt == null);

                IQueryable<Roles> filteredData;
                if ((request.Search == null) || (string.IsNullOrWhiteSpace(request.Search.Value)))
                {
                    filteredData = roles;
                }
                else
                {
                    filteredData = roles.Search(i => i.Name,i=>i.Description).Containing(request.Search.Value);
                }

                if (request.Orders.Count() > 0)
                {
                    var firstOrder = request.Orders.FirstOrDefault();
                    var col = request.Columns.ToArray()[firstOrder.Column];
                    filteredData = filteredData.OrderBy($"{col.Name} {firstOrder.Dir.ToUpper()}");
                }

                var dataPage = filteredData.Skip(request.Start).Take(request.Length);

                return dataPage.Select(i => new Common.Dto.RoleTable(i))
                    .ToDataTablesResponse(request, roles.Count(), filteredData.Count());
            }
            catch (Exception)
            {
                throw;
            }


        }
        public List<Common.Dto.RoleList> GetList()
        {
            try
            {
                var model = _roleRepository.GetWithCondition(i=>i.DeletedAt == null).Select(i=> new Common.Dto.RoleList()
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList();
                return model;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public Common.Dto.Role GetById(Guid id)
        {
            try
            {
                var model = _roleRepository.GetById(id);

                //string roleClaimsTemplateString = CreateRoleClaimsTemplateString();
                //var roleClaimsTemplate = Newtonsoft.Json.Linq.JArray.Parse(roleClaimsTemplateString);
                //var roleClaims = Newtonsoft.Json.Linq.JArray.Parse(model.RoleClaims);
                //roleClaimsTemplate.Merge(roleClaims, new Newtonsoft.Json.Linq.JsonMergeSettings { MergeArrayHandling = Newtonsoft.Json.Linq.MergeArrayHandling.Merge,MergeNullValueHandling=Newtonsoft.Json.Linq.MergeNullValueHandling.Ignore });
                ////model.RoleClaims = Regex.Replace(roleClaimsTemplate.ToString(), @"\r|\n", "");
                //model.RoleClaims = roleClaimsTemplate.ToString(Newtonsoft.Json.Formatting.None);

                if (model == null)
                    return null;
                else
                {
                    var dto = new Common.Dto.Role(model);
                    //dto.RoleClaims = model.RoleClaims.Select(i=>i.)
                    //dto.RoleClaims = Newtonsoft.Json.JsonConvert.DeserializeObject <List<Common.Dto.RoleClaims>>(roleClaimsTemplate.ToString());
                    return dto;
                }
                    
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Guid> CreateAsync(Common.Dto.Role dto)
        {
            try
            {
                //name validation
                var countValidation = _roleRepository.GetWithCondition(i => i.Name == dto.Name && i.DeletedAt == null).Count();
                if (countValidation != 0)
                    throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DuplicateNotAllowed));

                var role = dto.CreateModel(_currentUserId);
               
                await _roleRepository.AddAsync(role);
                await _roleRepository.SaveAsync();

                return role.Id;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> UpdateAsync(Guid id, Common.Dto.Role dto)
        {
            try
            {
                //name validation
                var countValidation = _roleRepository.GetWithCondition(i => i.Name == dto.Name && i.DeletedAt == null && i.Id != id).Count();
                if (countValidation > 0)
                    throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DuplicateNotAllowed));

                //update group header
                var findEntity = _roleRepository.GetById(id);
                Roles editedData = findEntity.UpdateWithDto(dto);
                editedData.UpdatedAt = DateTime.UtcNow;
                editedData.UpdatedBy = _currentUserId;

                foreach (var claims in dto.RoleClaims)
                {
                    var findClaims = editedData.RoleClaims.FirstOrDefault(i => i.ClaimId == claims.ClaimId);
                    if (findClaims != null)
                    {
                        findClaims.ClaimValue = Utf8Json.JsonSerializer.ToJsonString(claims.Claims);
                    }
                    else
                    {
                        var roleClaims = new RoleClaims()
                        {
                            ClaimId = claims.ClaimId,
                            ClaimValue = Utf8Json.JsonSerializer.ToJsonString(claims.Claims)
                        };
                        editedData.RoleClaims.Add(roleClaims);
                    }
                }

                //clear and add group role
                _roleRepository.Update(editedData);

                return await _roleRepository.SaveAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Delete(Guid id)
        {
            try
            {
                var group = _roleRepository.GetById(id);
                if (group == null)
                {
                    throw new Exception("Data not found");
                }

                group.DeletedBy = _currentUserId;
                group.DeletedAt = DateTime.UtcNow;
                _roleRepository.Update(group);
                _roleRepository.SaveAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Common.Dto.SysClaims> CreateClaimsTemplate()
        {
            try
            {
                var modelClaims = _sysClaimRepository.GetAll().Select(i => new Common.Dto.SysClaims(i)).ToList();
                return modelClaims;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        //public Common.Dto.RoleClaims ConvertToRoleClaimsObject(string roleClaimString)
        //{
        //    //eliminate \r \n string
        //    //var jsonSterilize = "{" + Regex.Replace(roleClaimString.ToString(), @"\r|\n", "") + "}";
        //    //var jsonSterilize = "{" + roleClaimString + "}";
        //    var test =  Newtonsoft.Json.JsonConvert.DeserializeObject<Common.Dto.RoleClaims>(roleClaimString);
        //    return null;
        //}
        //public string CreateRoleClaimsTemplateString()
        //{

        //    var roleClaims = CreateRoleClaimsTemplate();
        //    return Utf8Json.JsonSerializer.ToJsonString(roleClaims);
        //}
        //public List<Common.Dto.RoleClaims> CreateRoleClaimsTemplate()
        //{
        //    var modelClaims = _sysClaimRepository.GetAll().OrderBy(i => i.Level).ToList();
        //    List<Common.Dto.RoleClaims> hierarchy = new List<Common.Dto.RoleClaims>();
        //    hierarchy = modelClaims
        //                    .Where(c => c.ParentId == null)
        //                    .Select(c => new Common.Dto.RoleClaims()
        //                    {
        //                        Name = c.Name,
        //                        DisplayName = c.DisplayName,
        //                        ClaimsValue = c.ClaimsTemplateValue,
        //                        ClaimsType = c.ClaimsType,
        //                        Claims = GetChildrenClaims(modelClaims, c.Id)
        //                    })
        //                    .ToList();



        //    return hierarchy;
        //}

        //public List<Common.Dto.RoleClaims> GetChildrenClaims(List<Common.Entities.SysClaim> models, Guid? parentId)
        //{
        //    return models
        //            .Where(c => c.ParentId == parentId)
        //            .Select(c => new Common.Dto.RoleClaims()
        //            {
        //                Name = c.Name,
        //                DisplayName = c.DisplayName,
        //                ClaimsValue = c.ClaimsTemplateValue,
        //                ClaimsType = c.ClaimsType,
        //                Claims = GetChildrenClaims(models, c.Id)
        //            })
        //            .ToList();
        //}

    }
}
