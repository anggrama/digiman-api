using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DigiDocu.Common.Entities;
using DigiDocu.Common.Repositories;
using DigiDocu.BusinessLayer.Extensions;
using System.IO;
using Microsoft.AspNetCore.Http;
using DataTables.AspNetCore.Mvc.Binder;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;
using DigiMan.Common.Controller;
using Microsoft.AspNetCore.StaticFiles;

namespace digiman_service.DigiDocu.v1
{
    public class DocumentProfileRequest : Common.Dto.DocumentProfile
    {
        public IFormFile File { get; set; }
    }

    public class DocumentProfileService : BaseService
    {
        ISysDocumentProfileRepository _documentProfileRepository;
        //ISysDocumentProfileValueRepository _documentProfileValueRepository;
        ISysDocumentProfileDetailRepository _documentProfileDetailRepository;
        Guid _currentUserId;
        public DocumentProfileService(Guid currentUserId)
        {
            _documentProfileRepository = new DataAccess.Repositories.SysDocumentProfileRepository();
            _documentProfileDetailRepository = new DataAccess.Repositories.DocumentProfileDetailRepository();
            _currentUserId = currentUserId;
        }
        public DataTablesResponse<Common.Dto.DocumentProfileTable> GetAllTable(DataTablesRequest request)
        {

            var data = _documentProfileRepository.GetAll();

            IQueryable<SysDocumentProfiles> filteredData;
            if (request.Search == null || (String.IsNullOrWhiteSpace(request.Search.Value)))
                filteredData = data;
            else
                filteredData = data.Search(
                    p => p.Name,
                    p => p.Description)
                    .Containing(request.Search.Value);

            if (request.Orders.Count() > 0)
            {
                var firstOrder = request.Orders.FirstOrDefault();
                var col = request.Columns.ToArray()[firstOrder.Column];
                filteredData = filteredData.OrderBy($"{col.Name} {firstOrder.Dir.ToUpper()}");
            }
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);
            return dataPage
                .Select(p => new Common.Dto.DocumentProfileTable(p))
                .ToDataTablesResponse(request, data.Count(), filteredData.Count());
        }
        public List<Common.Dto.UserList> GetList()
        {
            try
            {
                var result = _documentProfileRepository.GetAll().Select(i => new Common.Dto.UserList()
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public bool UploadFileWatermark(IFormFile file, string filename)
        //{
        //    try
        //    {
        //        //var webRoot = _hostingEnvironment.WebRootPath;
        //        var webRoot = "/";
        //        var filePath = Path.Combine(webRoot, "Files", "Watermark");
        //        if (file.Length > 0)
        //        {
        //            if (!Directory.Exists(filePath))
        //            {
        //                Directory.CreateDirectory(filePath);
        //            }

        //            using (var stream = new FileStream(Path.Combine(filePath, filename), FileMode.Create))
        //            {
        //                file.CopyToAsync(stream).Wait();
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<Guid> CreateAsync(Common.Dto.DocumentProfile dto)
        {
            try
            {
                var documentProfile = dto.CreateModel(_currentUserId);

                //Adding new Fields
                if (dto.Fields != null)
                {
                    var newFields = dto.Fields.Select((value, i) =>
                    {
                        value.DisplaySequence = i;
                        return value;
                    }).Where(i => !i.Id.HasValue).Select((value, i) => value.CreateModel(documentProfile.Id, value.DisplaySequence));
                    foreach (var field in newFields)
                    {
                        //field.DocumentProfileId = documentProfile.Id;
                        //await _documentProfileDetailRepository.AddAsync(field);
                        documentProfile.SysDocumentProfileDetail.Add(field);
                    }
                }
                await _documentProfileRepository.AddAsync(documentProfile);

                await _documentProfileRepository.SaveAsync();
                return documentProfile.Id;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(Guid id, Common.Dto.DocumentProfile dto)
        {
            try
            {

                //Update document profile
                //_documentProfileRepository.Update(dto.CreateModel());
                //var documentProfile = _documentProfileRepository.GetByIdWithFields(dto.Id.Value);

                var findDocProfile = _documentProfileRepository.GetById(id);


                //updata document profile
                var editedData = findDocProfile.UpdateWithDto(dto);
                editedData.UpdatedBy = _currentUserId;
                //_documentProfileDetailRepository.DeleteRange(fields.ToArray());
                //editedData.SysDocumentProfileDetail.Clear();


                //Deleting fields
                if (dto.DeletedFields != null || dto.DeletedFields.Count > 0)
                {
                    foreach (var field in dto.DeletedFields)
                    {
                        var deletedField = editedData.SysDocumentProfileDetail.Where(p => p.Id == field.Id).FirstOrDefault();
                        deletedField.DeletedAt = DateTime.UtcNow;
                        deletedField.DeletedBy = _currentUserId;
                    }
                }
                if (dto.Fields != null || dto.Fields.Count > 0)
                {
                    ////Update fields
                    var updateFields = dto.Fields.Select((value, i) =>
                    {
                        value.DisplaySequence = i;
                        return value;
                    }).Where(p => p.Id.HasValue).Select((value, i) => value.CreateModel(findDocProfile.Id, value.DisplaySequence));
                    updateFields.ToList().ForEach(p => _documentProfileDetailRepository.Update(p));

                    ////Add new fields
                    var newFields = dto.Fields.Select((value, i) =>
                    {
                        value.DisplaySequence = i;
                        return value;
                    }).Where(i => !i.Id.HasValue).Select((value, i) => value.CreateModel(findDocProfile.Id, value.DisplaySequence));

                    newFields.ToList().ForEach(p => editedData.SysDocumentProfileDetail.Add(p));
                }
                //var result = await _documentProfileRepository.SaveAsync();
                _documentProfileRepository.Update(editedData);

                await _documentProfileDetailRepository.SaveAsync();
                return await _documentProfileRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                //var currentUserId = Guid.Parse(_userManager.GetUserId(_userClaim));
                var documentProfile = _documentProfileRepository.GetById(id);
                documentProfile.DeletedAt = DateTime.UtcNow;
                documentProfile.DeletedBy = _currentUserId;
                _documentProfileRepository.Update(documentProfile);
                //foreach (var field in documentProfile.SysDocumentProfileDetails)
                //{
                //    await _documentProfileDetailRepository.SoftDelete(field, currentUserId);
                //    var selections = _documentProfileDetailRepository.GetById(field.Id);
                //    foreach (var selection in selections.SysDocumentProfileDetailValues)
                //    {
                //        await _documentProfileValueRepository.SoftDelete(selection, currentUserId);
                //    }
                //}

                var result = await _documentProfileRepository.SaveAsync();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

            //return false;
        }

        public List<Common.Dto.DocumentProfileSelect> GetAll()
        {
            var documentProfiles = _documentProfileRepository.GetAll();
            return documentProfiles.Select(i => new Common.Dto.DocumentProfileSelect(i)).ToList();
        }
        public Common.Dto.DocumentProfile GetById(Guid id)
        {
            var model = _documentProfileRepository.GetById(id);
            //foreach (var field in model.SysDocumentProfileDetails)
            //if (field.FieldType == "H")
            //field.SysDocumentProfileDetailValues = _documentProfileValueRepository.GetByFieldId(field.Id);

            var result = new Common.Dto.DocumentProfile(model);
            return result;
        }
        public List<Common.Dto.DocumentProfileSelect> GetParentSelect()
        {
            var documentProfileSelection = new List<Common.Dto.DocumentProfileSelect>();
            documentProfileSelection.Add(new Common.Dto.DocumentProfileSelect()
            {
                Id = null,
                Name = "No Inherits"
            });

            var documentProfile = _documentProfileRepository.GetWithCondition(p => p.IsSetDocumentProfile == false)
                .Select(p => new Common.Dto.DocumentProfileSelect(p)).ToList();
            documentProfileSelection.AddRange(documentProfile);

            return documentProfileSelection;
        }

        public async Task<int> UploadWatermark(IFormFile file, Guid id)
        {
            var filename = await SaveFile(file, Common.Enums.UploadTypeEnum.Watermark);

            var findDocProfile = _documentProfileRepository.GetById(id);
            if (findDocProfile != null)
            {
                findDocProfile.WatermarkFile = filename;
            }
            _documentProfileRepository.Update(findDocProfile);

            var filePath = Common.Settings.AppSystem.WatermarkFolder;
            using (var stream = new FileStream(Path.Combine(filePath, filename), FileMode.Create))
            {
                file.CopyToAsync(stream).Wait();
            }

            return await _documentProfileRepository.SaveAsync();
        }

        public async Task<int> UploadWatermarkField(IFormFile file, Guid id)
        {
            var filename = await SaveFile(file, Common.Enums.UploadTypeEnum.Watermark);

            var findDocProfile = _documentProfileDetailRepository.GetById(id);
            if (findDocProfile != null)
            {
                findDocProfile.FieldOptions = filename;
            }
            return await _documentProfileRepository.SaveAsync();

        }

        public Common.Dto.DocumentProfileWatermark GetWatermark(Guid id)
        {
            var findDocProfile = _documentProfileRepository.GetById(id);
            if (findDocProfile == null)
                return null;

            if (findDocProfile.WatermarkFile == null)
                return null;

            var fileProvider = new FileExtensionContentTypeProvider();
            // Figures out what the content type should be based on the file name.  
            if (!fileProvider.TryGetContentType(findDocProfile.WatermarkFile, out string contentType))
            {
                throw new ArgumentOutOfRangeException($"Unable to find Content Type for file name {findDocProfile.WatermarkFile}.");
            }

            var filePath = Common.Settings.AppSystem.WatermarkFolder;
            Byte[] b = System.IO.File.ReadAllBytes(filePath + "\\" + findDocProfile.WatermarkFile);   // You can use your own method over here.         

            var returnValue = new Common.Dto.DocumentProfileWatermark()
            {
                Watermark = b,
                ContentType = contentType
            };

            return returnValue;
        }

        public async void DeleteWatermark(Guid id)
        {
            var findDocProfile = _documentProfileRepository.GetById(id);
            if (findDocProfile != null)
            {
                findDocProfile.WatermarkFile = null;
            }
            await _documentProfileRepository.SaveAsync();
        }
    }
}

