using DataTables.AspNetCore.Mvc.Binder;
using DigiDocu.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NinjaNye.SearchExtensions;
using DigiDocu.Common.Dto;
using Microsoft.EntityFrameworkCore;

namespace digiman_service.DigiDocu.v1
{
    public class SearchService
    {
        DocumentRepository _documentRepository;
        ObjectRepository _objectRepository;

        public SearchService()
        {

            _documentRepository = new DocumentRepository();
            _objectRepository = new ObjectRepository();
        }

        public DataTablesResponse<DocumentSearchResultData> DocumentSearch(DocumentSearchRequestData request, DataTablesRequest dtRequest)
        {
            var data = _documentRepository.GetAll()
                .Where(i => i.Object.Type == "D")
                .Include(i => i.Object)
                .Include(i => i.Object.DocumentProfile)
                .Include(i => i.DmsDocumentFiles)
                .AsQueryable();

            if (request.DocumentName != null)
                data = data.Search(i => i.Object.Name).Containing(request.DocumentName);
            if (request.ReferenceNumber != null)
                data = data.Search(i => i.ReferenceNumber).Containing(request.ReferenceNumber);
            if (request.Description != null)
                data = data.Search(i => i.Object.Description).Containing(request.Description);
            if (request.CreatedBy != null)
                data = data.Search(i => i.CreatedBy.ToString()).Containing(request.CreatedBy);
            if (request.Status != null)
                data = data.Search(i => i.Object.RecordStatus).Containing(request.Status);
            if (request.CreatedDate != null)
                data = data.Where(i => i.Object.CreatedAt.Value.ToString("dd/MM/yyyy") == request.CreatedDate.Value.ToString("dd/MM/yyyy"));
            if (request.EffectiveDate != null)
                data = data.Where(i => i.EffectiveEndDate == request.EffectiveDate);
            if (request.DocumentProfileId != null)
                data = data.Where(i => i.Object.DocumentProfileId == request.DocumentProfileId);

            if (request.SimpleSearch != null)
                data = data.Search(i => i.Object.Name, i => i.ReferenceNumber, i => i.Object.Description, i => i.CreatedBy.ToString(), i => i.Object.RecordStatus, i => i.Object.RecordStatus, i => i.Object.DocumentProfile.Name)
                    .Containing(request.SimpleSearch);

            if (dtRequest.Search != null)
                if (!string.IsNullOrWhiteSpace(dtRequest.Search.Value))
                    data = data.Search(i => i.Object.Name, i => i.ReferenceNumber, i => i.Object.Description, i => i.CreatedBy.ToString(), i => i.Object.RecordStatus, i => i.Object.RecordStatus, i => i.Object.DocumentProfile.Name)
                        .Containing(dtRequest.Search.Value);

            if (dtRequest.Orders.Count() > 0)
            {
                var firstOrder = dtRequest.Orders.FirstOrDefault();
                var col = dtRequest.Columns.ToArray()[firstOrder.Column];
                //var dataOrderable = data.OrderBy($"{col.Name} {firstOrder.Dir.ToUpper()}");
            }
            var dataPage = data
                          .Skip(dtRequest.Start).Take(dtRequest.Length);
            var dataResult = dataPage.Select(p => new DocumentSearchResultData(p)).ToList();
            foreach (var item in dataResult)
            {
                if (item.LocationName == "-")
                    continue;
                var objectData = _objectRepository.GetById(Guid.Parse(item.LocationName));
                string rootFolder;
                if(objectData.ScopeType == "P")
                    rootFolder = "Public Folder";
                else
                    rootFolder = "Private Folder";
                item.LocationName = $"{rootFolder}\\{LocationFile(Guid.Parse(item.LocationName))}\\";
            }
            return dataResult.ToDataTablesResponse(dtRequest, data.Count(), data.Count());
        }

        public string LocationFile(Guid parentId)
        {
            List<string> locationPath = new List<string>();
            var objectData = _objectRepository.GetById(parentId);
            locationPath.Insert(0,objectData.Name);
            if (objectData.ParentId.HasValue)
                locationPath.Insert(0,LocationFile(objectData.ParentId.Value));

            return string.Join("\\", locationPath);

        }
    }
}
