using System;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNetCore.Mvc.Binder;
using System.Collections.Generic;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;

using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;

using DigiDocu.BusinessLayer.Extensions;
using DigiDocu.Common.Repositories;


namespace digiman_service.DigiDocu.v1

{

    public class DataTableRequest : IDataTablesRequest
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public ISearch Search { get; set; }

        public IEnumerable<IColumn> Columns { get; set; }

        public IDictionary<string, object> AdditionalParameters { get; set; }
    }


    public class ParameterService : BaseService
    {
        public ParameterService()
        {
            _parameterRepository = new DataAccess.Repositories.ParameterRepository();
        }
        private readonly IParameterRepository _parameterRepository;
        public ParameterService(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }
        private Guid _currentUserId;
        public ParameterService(Guid currentUserId)
        {
            _parameterRepository = new DataAccess.Repositories.ParameterRepository();
            _currentUserId = currentUserId;
        }
        public async Task<int> Update(Guid id, Common.Dto.SysParameter dto)
        {
            
            var oModel = _parameterRepository.GetById(id);
            if (oModel == null)
                throw new Exception("data not found");
               

            oModel = oModel.UpdateWithDto(dto);
            oModel.UpdatedBy = _currentUserId;
            _parameterRepository.Update(oModel);
            return await _parameterRepository.SaveAsync();

        }
        public Common.Dto.SysParameter GetById(Guid id, string visibleStatus)
        {
            
            var oModel = _parameterRepository.GetWithCondition(i => i.Id == id && i.VisibleStatus == visibleStatus).FirstOrDefault();
            if (oModel != null)
                return new Common.Dto.SysParameter(oModel);
            else
                return null;
        }
        public Common.Dto.SysParameter GetByName(string name, string visibleStatus)
        {

            var oModel = _parameterRepository.GetWithCondition(i => i.Name == name && i.VisibleStatus == visibleStatus).FirstOrDefault();
            if (oModel != null)
                return new Common.Dto.SysParameter(oModel);
            else
                return null;
        }
        public DataTablesResponse<Common.Dto.SysParameter> GetAllTable(DataTablesRequest request)
        {
                var parameters = _parameterRepository.GetWithCondition(i => i.VisibleStatus == "U");
                var data = parameters.Select(i => new Common.Dto.SysParameter()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Type = i.Type,
                    Value = i.Value
                });

                IQueryable<Common.Dto.SysParameter> filteredData;
                if ((request.Search == null) || (string.IsNullOrWhiteSpace(request.Search.Value)))
                {
                    filteredData = data;
                }
                else
                {
                    filteredData = data.Search(i=>i.Name,i=>i.Value).Containing(request.Search.Value);
                }

                if (request.Orders.Count() > 0)
                {
                    var firstOrder = request.Orders.FirstOrDefault();
                    var col = request.Columns.ToArray()[firstOrder.Column];
                    filteredData = filteredData.OrderBy($"{col.Name} {firstOrder.Dir.ToUpper()}");
                }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            return dataPage.ToDataTablesResponse(request, data.Count(), filteredData.Count());

        }
      
    }
}
