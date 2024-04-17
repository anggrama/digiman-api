using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DataTables.AspNet.AspNetCore;
//using DataTables.AspNet.Core;
using DataTables.AspNetCore.Mvc.Binder;
using System.Linq.Dynamic.Core;

using DigiDocu.BusinessLayer.Extensions;
using DigiMan.Common.MessageHandler;

using DigiDocu.Common.Entities;
using NinjaNye.SearchExtensions;

namespace digiman_service.DigiDocu.v1
{
    public class GroupService : BaseService
    {

        private readonly Common.Repositories.IGroupRepository _groupRepository;
        Guid _currentUserId;
        public GroupService(Guid currentUserId)
        {
            _groupRepository = new DataAccess.Repositories.GroupRepository();
            _currentUserId = currentUserId;
        }

        public List<Common.Dto.GroupList> GetList()
        {
            try
            {
                var model = _groupRepository.GetWithCondition(i => i.DeletedAt == null).Select(i => new Common.Dto.GroupList()
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
        public DataTablesResponse<Common.Dto.Group> GetAll(DataTablesRequest request)
        {
            try
            {
                var groups = _groupRepository.GetAll().Where(i=>i.DeletedAt == null);

                IQueryable<Groups> filteredData;
                if ((request.Search == null) || (string.IsNullOrWhiteSpace(request.Search.Value)))
                {
                    filteredData = groups;
                }
                else
                {
                    filteredData = groups.Search(x=>x.Name,x=>x.Description).Containing(request.Search.Value);
                }

                if (request.Orders.Count() > 0)
                {
                    var firstOrder = request.Orders.FirstOrDefault();
                    var col = request.Columns.ToArray()[firstOrder.Column];
                    filteredData = filteredData.OrderBy($"{col.Name} {firstOrder.Dir.ToUpper()}");
                }
                
                var dataPage = filteredData.Skip(request.Start).Take(request.Length);

                return dataPage
                    .Select(i => new Common.Dto.Group(i))
                    .ToDataTablesResponse(request, groups.Count(), filteredData.Count());

            }
            catch (Exception)
            {
                throw;
            }


        }
        public Common.Dto.Group GetById(Guid id)
        {
            try
            {
                var model = _groupRepository.GetById(id);
                if (model == null)
                    return null;
                else
                    return new Common.Dto.Group(model);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Guid> CreateAsync(Common.Dto.Group dto)
        {
            try
            {
                //name validation
                var countValidation = _groupRepository.GetWithCondition(i => i.Name == dto.Name && i.DeletedAt == null).Count();
                if (countValidation != 0)
                    throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DuplicateNotAllowed));

                var model = dto.CreateModel(_currentUserId);
                foreach (var role in dto.Roles)
                {
                    var groupRole = new GroupRoles()
                    {
                        RoleId = role.Id
                    };
                    model.GroupRoles.Add(groupRole);
                }
                await _groupRepository.AddAsync(model);
                await _groupRepository.SaveAsync();

                return model.Id;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> UpdateAsync(Guid id, Common.Dto.Group dto)
        {
            try
            {
                //name validation
                var countValidation = _groupRepository.GetWithCondition(i => i.Name == dto.Name && i.DeletedAt == null && i.Id != id).Count();
                if (countValidation > 0)
                    throw new Exception(OperationMessageHandler.GetOperationMessage(OperationStatus.DuplicateNotAllowed));

                //update group header
                var findEntity = _groupRepository.GetById(id);
                Groups editedData = findEntity.UpdateWithDto(dto);
                editedData.UpdatedAt = DateTime.UtcNow;

                //clear and add group role
                editedData.GroupRoles.Clear();
                
                foreach (var role in dto.Roles)
                {
                    var groupRole = new GroupRoles()
                    {
                        RoleId = role.Id
                    };
                    editedData.GroupRoles.Add(groupRole);
                    
                }
                _groupRepository.Update(editedData);

                return await _groupRepository.SaveAsync();

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
                var group = _groupRepository.GetById(id);
                if (group == null)
                {
                    throw new Exception("Data not found");
                }

                group.DeletedAt = DateTime.UtcNow;
                group.DeletedBy = _currentUserId;
                _groupRepository.Update(group);
                 _groupRepository.SaveAsync();
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
