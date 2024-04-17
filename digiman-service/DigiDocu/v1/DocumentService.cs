using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

using DigiDocu.DataAccess.Repositories;
using DigiDocu.Common.Entities;
using DigiDocu.Common.Enums;
using DigiDocu.Common.Repositories;
using System.Drawing;

namespace digiman_service.DigiDocu.v1
{
    public class DocumentService : BaseService
    {
        IDocumentRepository _documentRepository;
        IDocumentFileRepository _documentFileRepository;
        IObjectRepository _objectRepository;
        ISysDocumentProfileRepository _sysDocumentProfileRepository;
        
        StorageService _storageService;
        ParameterService _parameterService;
        
        private Guid _currentUserId;

        public DocumentService()
        {

            _documentRepository = new DocumentRepository();
            _documentFileRepository = new DocumentFileRepository();

            _objectRepository = new ObjectRepository();
            _sysDocumentProfileRepository = new SysDocumentProfileRepository();
            _storageService = new StorageService();
            _parameterService = new ParameterService();
        }

        public DocumentService(Guid currentUserId)
        {

            _documentRepository = new DocumentRepository();
            _documentFileRepository = new DocumentFileRepository();

            _objectRepository = new ObjectRepository();
            _sysDocumentProfileRepository = new SysDocumentProfileRepository();
            _storageService = new StorageService(currentUserId);
            _parameterService = new ParameterService(currentUserId);

            _currentUserId = currentUserId;
        }

        // done tested
        private List<Common.Dto.FolderResponse> InitFolders(string path = null)
        {
            //TODO : Check Permission
            List<Common.Dto.FolderResponse> initFolder = new List<Common.Dto.FolderResponse>();
            initFolder.Add(new Common.Dto.FolderResponse { Id = "public", Size = 4096, Type = "F", Value = "Public Folder", Date = DateTime.UtcNow.Ticks });
            initFolder.Add(new Common.Dto.FolderResponse { Id = "private", Size = 4096, Type = "F", Value = "Private Folder", Date = DateTime.UtcNow.Ticks });

            if(path != null)
            {
                var pathList = path.Split("/").ToList();
                if(pathList.Count() > 2)
                {
                    pathList.RemoveAt(0);
                    var parentPath = pathList[0];
                    string parentId = pathList[0];
                    var folder = initFolder.Where(i => i.Id == parentPath).FirstOrDefault();
                    foreach (var item in pathList)
                    {
                        if (item == "public" || item == "private")
                        {
                            parentPath = null;
                            parentId = item;
                            continue;
                        }

                        if(folder.Data == null)
                        {
                            var newData = _objectRepository.GetAll();
                            if (parentPath != null)
                                newData = newData.Where(i => i.ParentId == Guid.Parse(parentPath));
                            else
                                newData = newData.Where(i => i.ParentId == null);

                            newData = newData.Where(i => i.Type == "F")
                                    .OrderBy(i => i.Name)
                                    .OrderByDescending(i => i.Type);
                            folder.Data = newData.Select(i => new Common.Dto.FolderResponse(i)).ToList();
                            folder.Data.ForEach(i =>
                            {
                                if (i.ParentId != null)
                                {
                                    var objectData = _objectRepository.GetById(i.ParentId.Value);
                                    string rootFolder;
                                    if (objectData.ScopeType == "P")
                                        rootFolder = "public";
                                    else
                                        rootFolder = "private";
                                    i.Path = $"/{rootFolder}/{LocationDocumentId(i.ParentId.Value)}/{i.Id}";
                                }
                                else
                                    i.Path = $"/{parentId}/{i.Id}";
                            });
                        }
                        folder = folder.Data.FirstOrDefault(i => i.Id == item);
                        parentPath = item;
                    }
                }
            }
            //initFolder.Add(new Common.Dto.FolderResponse { Id = "shared", Size = 4096, Type = "F", Value = "Shared Folder", Date = DateTime.UtcNow.Ticks });

            return initFolder;
        }

        public List<Common.Dto.FileResponse> GetObjects(string id,string objectType, string path = null)
        {

            //TODO : Permission
            Guid resultGuid;
            bool isGuid = Guid.TryParse(id, out resultGuid);

            var listQueryable = _objectRepository.GetAllRelation().Where(i => i.DeletedAt == null);

            if (objectType == "F")
                listQueryable = listQueryable.Where(i => i.Type == objectType);

            if (isGuid == false)
            {
                if (id == "public")
                    listQueryable = listQueryable.Where(i => i.RootId == null && i.ScopeType == "P");
                else if (id == "private")
                    listQueryable = listQueryable.Where(i => i.RootId == null && i.ScopeType == "S");
                else if (id == "init")
                {
                    var initFolder = InitFolders(path).OrderBy(i => i.Value).OrderByDescending(i => i.Type).Select(i=> new Common.Dto.FileResponse(i)).ToList();
                    return initFolder;
                }
                else
                    return null;
            }
            else
            {
                listQueryable = listQueryable.Where(i => i.ParentId == Guid.Parse(id));
            }

            List<Common.Dto.FileResponse> listFolderFile = listQueryable.OrderBy(i => i.Name).OrderByDescending(i => i.Type).Select(i => new Common.Dto.FileResponse(i)).ToList();
            listFolderFile.ForEach(i =>
            {
                if (i.ParentId != null)
                {
                    var objectData = _objectRepository.GetById(i.ParentId.Value);
                    string rootFolder;
                    if (objectData.ScopeType == "P")
                        rootFolder = "public";
                    else
                        rootFolder = "private";
                    i.Path = $"/{rootFolder}/{LocationDocumentId(i.ParentId.Value)}/{i.Id}";
                }
                else
                    i.Path = $"/{id}/{i.Id}";
            });

            return listFolderFile;
        }
        public async Task<Common.Dto.FolderResponse> CreateFolderAsync(Common.Dto.CreateFolderRequest dto)
        {
            try
            {
                Guid? rootId = null;
                DmsObjects parentObject = null;
                int hierarchyLevel = 0;
                string scopeType = "";

                //Cek Parent
                Guid? parentId = null;
                try
                {
                    parentId = Guid.Parse(dto.ParentId);
                }
                catch (Exception)
                {
                    if (dto.ParentId.ToLower() == "public")
                        scopeType = "P";
                    else if (dto.ParentId.ToLower() == "private")
                        scopeType = "S";
                    else
                        throw new Exception("Unknown parent Id");
                }


                //root folder if null
                if (parentId != null)
                {
                    parentObject = _objectRepository.GetById(Guid.Parse(dto.ParentId));
                    hierarchyLevel = Convert.ToInt32(parentObject.HierarchyLevel) + 1;
                    rootId = parentObject.RootId ?? parentObject.Id;
                    scopeType = parentObject.ScopeType;
                }

                var model = new DmsObjects()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = _currentUserId,
                    OwnerId = _currentUserId,
                    Name = dto.FolderName,
                    Type = Convert.ToString((char)ObjectTypeEnum.Folder),
                    RecordStatus = Convert.ToString((char)DmsObjectRecordStatusEnum.Active),
                    ParentId = parentId,
                    HierarchyLevel = hierarchyLevel,
                    RootId = rootId,
                    ScopeType = scopeType,
                    DefaultStorageId = dto.StorageId,
                    LimitStorage = dto.LimitStorage,
                    DocumentProfileId = dto.DocumentProfileId,
                    Description = dto.Description
                };
                await _objectRepository.AddAsync(model);
                var isSuccess = await _objectRepository.SaveAsync();

                if (isSuccess > 0)
                {
                    return this.GetFolderResponse(model.Id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Common.Dto.FolderResponse GetFolderResponse(Guid id)
        {
            var result = _objectRepository.GetById(id);
            var dataResult = new Common.Dto.FolderResponse(result);
            if (dataResult.ParentId != null)
            {
                var objectData = _objectRepository.GetById(dataResult.ParentId.Value);
                string rootFolder;
                if (objectData.ScopeType == "P")
                    rootFolder = "public";
                else
                    rootFolder = "private";
                dataResult.Path = $"/{rootFolder}/{LocationDocumentId(dataResult.ParentId.Value)}/{dataResult.Id}";
            }
            else
                dataResult.Path = $"/{id}/{dataResult.Id}";
            return dataResult;
        }
        private Common.Dto.FileResponse GetFileResponse(Guid id)
        {
            var result = _objectRepository.GetById(id);
            var dataResult = new Common.Dto.FileResponse(result);
            if (dataResult.ParentId != null)
            {
                var objectData = _objectRepository.GetById(dataResult.ParentId.Value);
                string rootFolder;
                if (objectData.ScopeType == "P")
                    rootFolder = "public";
                else
                    rootFolder = "private";
                dataResult.Path = $"/{rootFolder}/{LocationDocumentId(dataResult.ParentId.Value)}/{dataResult.Id}";
            }
            else
                dataResult.Path = $"/{id}/{dataResult.Id}";
            return dataResult;
        }

        public async Task<Common.Dto.FileResponse> UploadDragDrop (Common.Dto.UploadDragRequest request)
        {
            try
            {
                var filename = await SaveFile(request.File, UploadTypeEnum.Document);

                //-- filename validation --
                var document = _objectRepository.GetWithCondition(i => i.Name == filename && i.DeletedAt != null).FirstOrDefault();
                if (document != null)
                    throw new Exception("Document already exist");
                //-- filename end --

                //find parent
                Guid? defaultStorageId = null;
                var parent = _objectRepository.GetById(request.Id);
                if (parent.RootId != null)
                {
                    var root = _objectRepository.GetById(Guid.Parse(parent.RootId.ToString()));
                    defaultStorageId = root.DefaultStorageId;
                }
                else
                {
                    defaultStorageId = parent.DefaultStorageId;
                }
                //create object

                //TODO: Document Profile Inherits
                Guid objectId = Guid.NewGuid();
                DmsObjects objects = new DmsObjects()
                {
                    Id = objectId,
                    ParentId = request.Id,
                    Type = ((char)ObjectTypeEnum.Document).ToString(),
                    Name = request.File.FileName,
                    RootId = parent.RootId,
                    HierarchyLevel = parent.HierarchyLevel + 1,
                    OwnerId = _currentUserId,
                    DocumentProfileId = parent.DocumentProfileId,
                    DefaultStorageId = defaultStorageId,
                    CreatedBy = _currentUserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedBy = _currentUserId,
                    UpdatedAt = DateTime.UtcNow
                };
                //await _objectRepository.AddAsync(objects);

                //create document
                Guid documentId = Guid.NewGuid();
                DmsDocuments oDocument = new DmsDocuments()
                {
                    Id = documentId,
                    ObjectId = objectId,
                    IsCheckout = false,
                    IsObsolete = false,
                    Version = "1.0"
                };
                //await _documentRepository.AddAsync(oDocument);

                var uploadResult = _storageService.UploadFile(defaultStorageId.Value, filename, request.File);
                //TODO : Default Storage

                SaveThumbnail(objectId.ToString(), request.File);

                //create files
                DmsDocumentFiles oFiles = new DmsDocumentFiles()
                {
                    Id = Guid.NewGuid(),
                    OriginalFileName = request.File.FileName,
                    Extension = Path.GetExtension(request.File.FileName),
                    ConvertedFilename = filename,
                    DocumentId = documentId,
                    Size = request.File.Length
                };

                oDocument.DmsDocumentFiles.Add(oFiles);
                objects.DmsDocuments.Add(oDocument);

                await _objectRepository.AddAsync(objects);

                ///await _documentFileRepository.AddAsync(oFiles);

                await _objectRepository.SaveAsync();
                //await _documentRepository.SaveAsync();
                //await _documentFileRepository.SaveAsync();

                return this.GetFileResponse(objectId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            
        }   

        public void SaveThumbnail(string filename, IFormFile file)
        {
            using (GdPicture14.GdPictureImaging gdpictureImaging = new GdPicture14.GdPictureImaging())
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    //var oGdPicturePDF = new GdPicture14.GdPicturePDF();
                    //oGdPicturePDF.LoadFromStream(ms);
                    //oGdPicturePDF.SelectPage(1);
                    //var imageId = oGdPicturePDF.GetPageThumbnail(500, 500, Color.Gray);
                    GdPicture14.DocumentFormat documentFormat = GdPicture14.DocumentFormat.DocumentFormatPDF;
                    int imageId = 0;
                    int pageCount = 0;
                    GdPicture14.GdPictureDocumentUtilities.GetDocumentPreview(ms, filename, 500, 500, Color.Gray.ToArgb(), ref documentFormat, ref imageId, ref pageCount);

                    //var imageId = gdpictureImaging.CreateGdPictureImageFromStream(ms, GdPicture14.DocumentFormat.DocumentFormatPDF);
                    gdpictureImaging.SaveAsPNG(imageId, $"F:\\DigiManStorage\\Thumbnail\\{filename}.png");
                    gdpictureImaging.ReleaseGdPictureImage(imageId);
                }
            }
        }

        public byte[] GetDocumentThumbnail(Guid objectId)
        {
            var documentPath = Path.Combine("F:\\DigiManStorage\\Thumbnail\\", $"{objectId.ToString()}.png");
            byte[] fileBytes = File.ReadAllBytes(documentPath);
            return fileBytes;

        }

        public Common.Dto.FolderFileBaseResponse Move(Guid id, Guid? dest)
        {
            try
            {
                var model = _objectRepository.GetRootById(id);
                //if (model.DmsRoots != null)
                //    return false;

                model.ParentId = dest;
                model.UpdatedBy = _currentUserId;
                model.UpdatedAt = DateTime.UtcNow;
                _objectRepository.SaveAsync();

                return new Common.Dto.FolderFileBaseResponse
                {
                    Id = model.Id.ToString(),
                    Date = Convert.ToDateTime(model.UpdatedAt).Ticks,
                    Type = model.Type,
                    Size = 0,
                    Value = model.Name,
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Common.Dto.FolderFileBaseResponse Rename(Guid id, string name)
        {
            var model = _objectRepository.GetById(id);
            model.Name = name;
            model.UpdatedAt = DateTime.UtcNow;
            model.UpdatedBy = _currentUserId;
            _objectRepository.SaveAsync();

            return new Common.Dto.FolderFileBaseResponse
            {
                Id = model.Id.ToString(),
                Date = Convert.ToDateTime(model.UpdatedAt).Ticks,
                Type = model.Type,
                Size = 0,
                Value = model.Name,
            };
        }


        public List<Common.Dto.SettingCollectionTemplateDto<string, string>> GetPermission()
        {
            var result = _parameterService.GetByName("permission.acl", "S");
            return Utf8Json.JsonSerializer.Deserialize<List<Common.Dto.SettingCollectionTemplateDto<string, string>>>(result.Value);
        }

        //done untested
        public async Task<Common.Dto.FileResponse> CreateDocument(Common.Dto.DocumentRequest request)
        {
            //-- filename validation inside same folder --
            var document = _objectRepository.GetWithCondition(i => i.Name == request.Name && i.ParentId== request.ParentId && i.DeletedAt != null).FirstOrDefault();
            if (document != null)
                throw new Exception("Document already exist");
            //-- filename end --

            //find parent
            Guid? defaultStorageId = null;
            var parent = _objectRepository.GetById(Guid.Parse(request.ParentId.ToString()));
            if (parent.RootId != null)
            {
                var root = _objectRepository.GetById(Guid.Parse(parent.RootId.ToString()));
                defaultStorageId = root.DefaultStorageId;
            }
            else
            {
                defaultStorageId = parent.DefaultStorageId;
            }

            //create object
            Guid objectId = Guid.NewGuid();
            DmsObjects objects = new DmsObjects()
            {
                Id = objectId,
                ParentId = request.ParentId,
                Type = ((char)ObjectTypeEnum.Document).ToString(),
                Name = request.Name,
                RootId = parent.RootId,
                HierarchyLevel = parent.HierarchyLevel + 1,
                OwnerId = _currentUserId,
                DocumentProfileId = request.DocumentProfileId,
                DefaultStorageId = defaultStorageId,
                ScopeType = parent.ScopeType,
                IsInheritPermission = request.IsInheritPermission,
                ObjectPermission = Utf8Json.JsonSerializer.ToJsonString(request.AccessControlList),
                CreatedBy = _currentUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = _currentUserId,
                UpdatedAt = DateTime.UtcNow
            };

            //create document
            Guid documentId = Guid.NewGuid();
            DmsDocuments oDocument = new DmsDocuments()
            {
                Id = documentId,
                ObjectId = objectId,
                EffectiveStartDate = request.EffectiveStartDate,
                EffectiveEndDate = request.EffectiveEndDate,
                ReferenceNumber = request.ReferenceNumber,
                Priority = request.Priority,
                ReminderSettings = Utf8Json.JsonSerializer.ToJsonString(request.Reminder),
                IsCheckout = false,
                IsObsolete = false,
                Version = "1.0",
                CreatedBy = _currentUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = _currentUserId,
                UpdatedAt = DateTime.UtcNow
            };

            //create profiles
            if (request.DocumentProfileId != null)
            {
                var docProfile = _sysDocumentProfileRepository.GetById(Guid.Parse(request.DocumentProfileId.ToString()));
                foreach (var item in docProfile.SysDocumentProfileDetail)
                {
                    var metadataValue = request.Metadata.FirstOrDefault(i => i.Id == item.Id).Value;
                    DmsDocumentDetails oDocumentDetails = new DmsDocumentDetails()
                    {
                        Id = Guid.NewGuid(),
                        SysDocumentProfileDetailId = item.Id,
                        FieldValue = metadataValue,
                        CreatedBy = _currentUserId,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedBy = _currentUserId,
                        UpdatedAt = DateTime.UtcNow
                    };

                    if (item.FieldType == "D")
                        oDocumentDetails.DateValue = Convert.ToDateTime(metadataValue);

                    if (item.FieldType == "N")
                        oDocumentDetails.NumericValue = Convert.ToDouble(metadataValue);

                    oDocument.DmsDocumentDetails.Add(oDocumentDetails);
                }
            }

            //create relation
            if (request.Relation != null)
            {
                foreach (var item in request.Relation)
                {
                    var documentRelation = _documentRepository.GetWithCondition(i => i.ObjectId == item.ObjectRelationId && i.IsObsolete == false).FirstOrDefault();
                    var relation = new DmsDocumentRelations()
                    {
                        Id = Guid.NewGuid(),
                        DocumentId = documentId,
                        DocumentRefId = documentRelation.Id
                    };
                    oDocument.DmsDocumentRelationsDocument.Add(relation);
                }
            }
            objects.DmsDocuments.Add(oDocument);

            //TODO : Create Audit Log

            //add one objects
            await _objectRepository.AddAsync(objects);
            await _objectRepository.SaveAsync();

            return new Common.Dto.FileResponse(objects);

        }
        public async Task<Common.Dto.FileResponse> UpdateDocument(Guid id,Common.Dto.DocumentRequest request)
        {
            //-- filename validation --
            var objects = _objectRepository.GetByIdWithRelation(id);
            if (objects != null)
                throw new Exception("Document already exist");
            //-- filename end --

            //find parent
            Guid? defaultStorageId = null;
            var parent = _objectRepository.GetById(request.Id);
            if (parent.RootId != null)
            {
                var root = _objectRepository.GetById(Guid.Parse(parent.RootId.ToString()));
                defaultStorageId = root.DefaultStorageId;
            }
            else
            {
                defaultStorageId = parent.DefaultStorageId;
            }

            //update objects
            objects.DocumentProfileId = parent.DocumentProfileId;
            objects.DefaultStorageId = defaultStorageId;
            objects.IsInheritPermission = request.IsInheritPermission;
            objects.ObjectPermission = Utf8Json.JsonSerializer.ToJsonString(request.AccessControlList);
            objects.UpdatedBy = _currentUserId;
            objects.UpdatedAt = DateTime.UtcNow;

            //update documents
            var document = objects.DmsDocuments.FirstOrDefault(i => i.IsObsolete == true);
            document.EffectiveStartDate = request.EffectiveStartDate;
            document.EffectiveEndDate = request.EffectiveEndDate;
            document.ReferenceNumber = request.ReferenceNumber;
            document.Priority = request.Priority;
            document.ReminderSettings = Utf8Json.JsonSerializer.ToJsonString(request.Reminder);
            document.UpdatedBy = _currentUserId;
            document.UpdatedAt = DateTime.UtcNow;

            //update profiles
            //var metadatas = document.DmsDocumentDetails.ToList();
           
            //foreach (var item in metadatas)
            //{
            //    var requestMeta = request.Metadata.FirstOrDefault(i => i.Id == item.SysDocumentProfileDetailId);
            //    if (requestMeta == null)
            //    {
            //        metadatas.Remove(item);
            //    }

            //    item.FieldValue = requestMeta.Value;
            //    item.NumericValue = null;
            //    item.DateValue = null;
            //    item.UpdatedAt = DateTime.UtcNow;
            //    item.UpdatedBy = _currentUserId;
                
            //    if (item.SysDocumentProfileDetail.FieldType == "D")
            //        item.DateValue = Convert.ToDateTime(requestMeta.Value);

            //    if (item.SysDocumentProfileDetail.FieldType == "N")
            //        item.NumericValue = Convert.ToDouble(requestMeta.Value);
            //}

            _objectRepository.Update(objects);
            await _objectRepository.SaveAsync();

            return new Common.Dto.FileResponse(objects);


        }
        public async Task<Common.Dto.FileResponse> Upload(Common.Dto.UploadDragRequest request)
        {
            var filename = await SaveFile(request.File, UploadTypeEnum.Document);

            var findDocument = _documentRepository.GetWithCondition(i => i.ObjectId == request.Id && i.IsObsolete == false).FirstOrDefault();
            if (findDocument == null)
                throw new Exception("Data Not Found");

            //create files
            DmsDocumentFiles oFiles = new DmsDocumentFiles()
            {
                Id = Guid.NewGuid(),
                OriginalFileName = request.File.FileName,
                Extension = Path.GetExtension(request.File.FileName),
                ConvertedFilename = filename,
                DocumentId = findDocument.Id,
                Size = request.File.Length
            };

            await _documentFileRepository.AddAsync(oFiles);
            await _documentFileRepository.SaveAsync();

            return this.GetFileResponse(request.Id);


        }
        public void Delete(Guid id)
        {
            try
            {
                var model = _objectRepository.GetById(id);
                model.DeletedAt = DateTime.UtcNow;
                model.DeletedBy = _currentUserId;
                _objectRepository.SaveAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        


        //todo
        public void CheckIn(Guid id)
        {
            try
            {
                var model = _documentRepository.GetById(id);
                model.IsCheckout = false;
                _objectRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CheckOut(Guid id)
        {
            try
            {
                var model = _documentRepository.GetById(id);
                model.IsCheckout = true;
                _objectRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Common.Dto.DownloadFileResult Download(Guid id)
        {
            var dmsObject = _objectRepository.GetById(id);
            var dmsDocument = _documentRepository.GetById(id);
            if (dmsDocument.DmsDocumentFiles.Count > 1)
            {
                var files = dmsDocument.DmsDocumentFiles.Select(i => GetFile(i));
                var result = CreateZip(files.ToArray(), dmsObject.Name);
                return result;
            }
            else
            {
                var file = dmsDocument.DmsDocumentFiles.FirstOrDefault();
                var result = GetFile(file);
                return result;
            }
        }
        public Common.Dto.DownloadFileResult DownloadZip(Guid id)
        {
            var dmsObject = _objectRepository.GetById(id);
            var dmsDocument = _documentRepository.GetById(id);
            if (dmsDocument.DmsDocumentFiles.Count > 1)
            {
                var files = dmsDocument.DmsDocumentFiles.Select(i => GetFile(i));
                var result = CreateZip(files.ToArray(), dmsObject.Name);
                return result;
            }
            else
            {
                var file = dmsDocument.DmsDocumentFiles.FirstOrDefault();
                var convertedFile = GetFile(file);
                var files = new List<Common.Dto.DownloadFileResult>();
                files.Add(convertedFile);
                var result = CreateZip(files.ToArray(), dmsObject.Name);
                return result;
            }
        }
        public void Bookmark(Guid documentId)
        {
            try
            {
                //var model = _documentRepository.GetById(id);
                //model.IsCheckout = true;
                //_objectRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Unbookmark(Guid documentId)
        {
            try
            {
                //var model = _documentRepository.GetById(id);
                //model.IsCheckout = true;
                //_objectRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Common.Dto.DownloadFileResult GetFileByFileId(Guid fileId)
        {
            var file = _documentFileRepository.GetById(fileId);
            return GetFile(file);
        }

        public Common.Dto.DownloadFileResult GetFile(DmsDocumentFiles file)
        {
            byte[] fileBytes = _storageService.GetFile(file.Document.Object.DefaultStorageId.Value, file.ConvertedFilename).Result;

            var result = new Common.Dto.DownloadFileResult(file);
            result.Data = fileBytes;
            return result;
        }
        public Common.Dto.DownloadFileResult GetFile(Guid documentId, Guid? fileId = null)
        {
            if(fileId == null)
            {
                var dmsDocument = _documentRepository.GetById(documentId);
                if (dmsDocument.DmsDocumentFiles.Count > 1)
                {
                    var files = dmsDocument.DmsDocumentFiles.Select(i => GetFile(i));
                    return files.FirstOrDefault();
                }
                else
                {
                    var file = dmsDocument.DmsDocumentFiles.FirstOrDefault();
                    return GetFile(file);
                }
            } 
            else
            {
                var fileDocument = _documentFileRepository.GetById(fileId.Value);
                return GetFile(fileDocument);
            }
        }
        private Common.Dto.DownloadFileResult CreateZip(Common.Dto.DownloadFileResult[] files, string fileName)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        //Create a zip entry for each attachment
                        var zipEntry = zipArchive.CreateEntry(file.Filename);

                        //Get the stream of the attachment
                        using (var originalFileStream = new MemoryStream(file.Data))
                        using (var zipEntryStream = zipEntry.Open())
                        {
                            //Copy the attachment stream to the zip entry stream
                            originalFileStream.CopyTo(zipEntryStream);
                        }

                    }

                }

                //var content = ReadFully(compressedFileStream);
                return new Common.Dto.DownloadFileResult()
                {
                    Id = Guid.NewGuid(),
                    ContentType = "application/zip",
                    Extension = "zip",
                    Filename = $"{fileName}.zip",
                    Data = compressedFileStream.ToArray()
                };
            }
        }
        public Common.Dto.DocumentResponse GetDocument(Guid id)
        {
            try
            {
                var dmsObject = _documentRepository.GetById(id);
                var result = new Common.Dto.DocumentResponse(dmsObject);
                if (dmsObject.Object.ParentId != null)
                {
                    var objectData = _objectRepository.GetById(dmsObject.Object.ParentId.Value);
                    string rootFolder;
                    string rootFolderId;
                    if (objectData.ScopeType == "P")
                    {
                        rootFolder = "Public Folder";
                        rootFolderId = "public";
                    }
                    else
                    {
                        rootFolder = "Private Folder";
                        rootFolderId = "private";
                    }
                    result.LocationText = $"{rootFolder}\\{LocationDocumentName(dmsObject.Object.ParentId.Value)}";
                    result.LocationPath = $"/{rootFolderId}/{LocationDocumentId(dmsObject.Object.ParentId.Value)}";
                }
                else
                {
                    result.LocationPath = $"/{dmsObject.Object.ParentId.Value}";
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string LocationDocumentName(Guid parentId)
        {
            List<string> locationPath = new List<string>();
            var objectData = _objectRepository.GetById(parentId);
            locationPath.Insert(0, objectData.Name);
            if (objectData.ParentId.HasValue)
                locationPath.Insert(0, LocationDocumentName(objectData.ParentId.Value));

            return string.Join("\\", locationPath);

        }
        public string LocationDocumentId(Guid parentId)
        {
            List<string> locationPath = new List<string>();
            var objectData = _objectRepository.GetById(parentId);
            locationPath.Insert(0, objectData.Id.ToString());
            if (objectData.ParentId.HasValue)
                locationPath.Insert(0, LocationDocumentId(objectData.ParentId.Value));

            return string.Join("/", locationPath);

        }

        public bool UpdateAnnotation(Guid id, string annotationCodeBase64)
        {
            try
            {
                var file = _documentFileRepository.GetById(id);
                file.Annotation = annotationCodeBase64;
                _documentFileRepository.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte[] GetWatermark(Guid documentId)
        {
            try
            {
                var documentFile = _documentFileRepository.GetById(documentId);
                if(documentFile != null) {
                    var fileName = documentFile.Document?.Object?.DocumentProfile?.WatermarkFile;
                    var filePath = Common.Settings.AppSystem.WatermarkFolder;
                    byte[] fileBytes = null;
                    if (File.Exists(Path.Combine(filePath, fileName)))
                        fileBytes = File.ReadAllBytes(Path.Combine(filePath, fileName));
                    return fileBytes;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private Method
        private Guid? GetDocumentProfileParentId(Guid? parentObjectId)
        {
            try
            {
                if (parentObjectId == null)
                    return null;

                var parentObject = _objectRepository.GetById(parentObjectId.Value);
                if (parentObject.DocumentProfileId == null)
                    GetDocumentProfileParentId(parentObject.ParentId);

                return parentObject.DocumentProfileId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Guid? GetParentStorageId (Guid? parentObjectId)
        {
            return null;
        }
        
        //private Common.Dto.DocumentManagerResponseBase CreateDocument(Guid? parentId = null, string fileName = "new document", string convertedFileName = null, int? fileSize = 0, string contentType = null, string extension = null)
        //{
        //    try
        //    {
        //        //Check Name Validation
        //        var documentNameExists = _objectRepository.GetParentBranch(parentId.Value).Where(i => i.Type == "D" && i.Name == fileName).Count();
        //        if (documentNameExists > 0)
        //        {
        //            return null;
        //        }

        //        //Creating Document
        //        var parentObject = _objectRepository.GetById(parentId.Value);
        //        var rootId = parentObject.RootId ?? parentObject.Id;
        //        var hierarchyLevel = parentObject.HierarchyLevel + 1;
        //        var parentDocumentProfileId = GetDocumentProfileParentId(parentId);
        //        var currentUserId = _currentUserId;
        //        var rootObject = _objectRepository.GetById(rootId);
        //        var dmsObject = new DmsObjects()
        //        {
        //            Id = Guid.NewGuid(),
        //            Description = "",
        //            ParentId = parentId,
        //            HierarchyLevel = hierarchyLevel,
        //            DocumentProfileId = parentDocumentProfileId,
        //            RecordStatus = "A",
        //            Type = "D",
        //            Name = fileName,
        //            OwnerId = currentUserId,
        //            CreatedAt = DateTime.UtcNow,
        //            CreatedBy = currentUserId,
        //            RootId = rootId,
        //        };

        //        var dmsDocument = new DmsDocuments()
        //        {
        //            ObjectId = dmsObject.Id,
        //            EffectiveStartDate = null,
        //            EffectiveEndDate = null,
        //            IsCheckout = false,
        //            Version = "1"
        //        };

        //        //var dmsFile = new DmsDocumentFiles()
        //        //{
        //        //    Id = Guid.NewGuid(),
        //        //    ContentType = contentType,
        //        //    OriginalFileName = fileName,
        //        //    ConvertedFilename = convertedFileName,
        //        //    DocumentId = dmsDocument.ObjectId,
        //        //    Extension = extension//,
        //        //    //StorageId = rootObject.DmsRoot.StorageId
        //        //};

        //        //dmsDocument.DmsDocumentFiles.Add(dmsFile);
        //        //dmsObject.DmsDocuments = dmsDocument;

        //        _objectRepository.AddAsync(dmsObject);
        //        //_storageService.UploadFile(rootObject.DmsRoot.StorageId.Value, fileName, file);
        //        if (_objectRepository.SaveAsync().Result > 0)
        //        {
        //            dmsObject = _objectRepository.GetById(dmsObject.Id);
        //            return new Common.Dto.DocumentManagerResponseBase(dmsObject);
        //        }
        //        else
        //        {
        //            return null;
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //Document
        //1.Get Document List Public, Private, Shared
        //public dynamic GetBranch(Common.Dto.DocumentManagerRequest request)
        //{
        //    if (request.Action == "GetBranch")
        //    {
        //        var result = new List<Common.Dto.DocumentManagerResponseData>();

        //        var privateFolder = _objectRepository.GetPersonalBranch(_currentUserId)
        //            .Select(i => new Common.Dto.DocumentManagerResponseData(i)).FirstOrDefault();

        //        if (privateFolder != null)
        //            result.Add(privateFolder);

        //        var rootPublicFolder = new Common.Dto.DocumentManagerResponseData()
        //        {
        //            Id = "publicFolder",
        //            Open = false,
        //            Type = "folder",
        //            Value = "Public Folder",
        //            Webix_branch = false,
        //            Data = _objectRepository.GetPublicBranch()
        //                .Select(i => new Common.Dto.DocumentManagerResponseBase(i)).ToList()
        //        };

        //        result.Add(rootPublicFolder);

        //        return result;
        //    }
        //    else
        //    {
        //        var result = new Common.Dto.DocumentManagerResponse()
        //        {
        //            Parent = request.Source
        //        };

        //        var parentId = Guid.Parse(request.Source);
        //        result.Data = _objectRepository.GetParentBranch(parentId)
        //            .OrderBy(i => i.Name)
        //            .OrderByDescending(i => i.Type)
        //            .Select(i => new Common.Dto.DocumentManagerResponseBase(i)).ToList();

        //        return result;
        //    }
        //}
        //2.Create Folder

        //5.Create Document
        //

        //Context Menu
        //1.CheckIn
        //public Common.Dto.DocumentManagerResponseBase CreateRootFolder(Common.Dto.DocumentManagerRootFolderRequest request)
        //{
        //    try
        //    {
        //        var documentProfileId = request.DocumentProfileId;
        //        var currentUserId = _currentUserId;
        //        var fileName = request.Name;
        //        var dmsObject = new DmsObjects()
        //        {
        //            Id = Guid.NewGuid(),
        //            Description = request.Description,
        //            HierarchyLevel = 0,
        //            DocumentProfileId = documentProfileId,
        //            RecordStatus = "A",
        //            Type = "F",
        //            Name = fileName,
        //            OwnerId = currentUserId,
        //            CreatedAt = DateTime.UtcNow,
        //            CreatedBy = currentUserId,
        //            ParentId = null,
        //            RootId = null,
        //            LimitStorage = request.LimitStorage,
        //            DefaultStorageId = request.StorageId
        //        };
        //        _objectRepository.AddAsync(dmsObject);
        //        _objectRepository.SaveAsync();

        //        //dmsObject = _objectRepository.GetById(dmsObject.Id);
        //        return new Common.Dto.DocumentManagerResponseBase(dmsObject);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Common.Dto.DocumentManagerRootFolderRequest GetRootFolder(Guid id)
        //{
        //    try
        //    {
        //        return new Common.Dto.DocumentManagerRootFolderRequest(_objectRepository.GetRootById(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Common.Dto.DocumentManagerResponseBase UpdateRootFolder(Common.Dto.DocumentManagerRootFolderRequest request)
        //{
        //    try
        //    {
        //        var dmsObject = _objectRepository.GetRootById(Guid.Parse(request.Id));
        //        dmsObject.Name = request.Name;
        //        dmsObject.Description = request.Description;
        //        dmsObject.DocumentProfileId = request.DocumentProfileId;
        //        dmsObject.LimitStorage = request.LimitStorage;
        //        dmsObject.DefaultStorageId = request.StorageId;
        //        _objectRepository.SaveAsync();

        //        return new Common.Dto.DocumentManagerResponseBase(dmsObject);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Common.Dto.DocumentManagerResponseBase CreateDocument(Common.Dto.DocumentRequests request, IFormFile file = null)
        //{
        //    try
        //    {
        //        var currentUserId = _currentUserId;
        //        //var parentObject = null;// _objectRepository.GetById(request.LocationId.Value);
        //        var model = request.CreateModelObject();
        //        //model.RootId = parentObject.RootId ?? parentObject.Id;
        //        //model.HierarchyLevel = parentObject.HierarchyLevel + 1;
        //        model.OwnerId = currentUserId;
        //        model.CreatedBy = currentUserId;
        //        var rootObject = _objectRepository.GetById(model.RootId.Value);
        //        var uploadResult = true;
        //        // random filename
        //        //var convertedFileName = Path.GetRandomFileName();
        //        if (file != null)
        //        {

        //            var fileName = file.FileName;
        //            var fileSize = Convert.ToInt32(file.Length);
        //            var contentType = file.ContentType;
        //            var extension = Path.GetExtension(file.FileName);
        //            //model.DmsDocuments.Size = fileSize;
        //            //model.DmsDocuments.DmsDocumentFiles.Add(new DmsDocumentFiles()
        //            //{
        //            //    Id = Guid.NewGuid(),
        //            //    ContentType = contentType,
        //            //    OriginalFileName = fileName,
        //            //    ConvertedFilename = fileName,
        //            //    DocumentId = model.DmsDocuments.ObjectId,
        //            //    Extension = extension,
        //            //   StorageId = Guid.Parse(model.DefaultStorageId.ToString())
        //            //});
        //        }
        //        _objectRepository.AddAsync(model);

        //        //Uploading file
        //        if (file != null)
        //        {
        //            uploadResult = _storageService.UploadFile(Guid.Parse(model.DefaultStorageId.ToString()), file.FileName, file, request);
        //        }
        //        _objectRepository.SaveAsync();


        //        return new Common.Dto.DocumentManagerResponseBase(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Common.Dto.FileResponse CreateDocument(Common.Dto.DocumentRequests request)
        //{
        //    try
        //    {
        //        var currentUserId = _currentUserId;
        //        //var parentObject = _objectRepository.GetById(request.LocationId.Value);
        //        var model = request.CreateModelObject();
        //        //model.RootId = parentObject.RootId ?? parentObject.Id;
        //        //model.HierarchyLevel = parentObject.HierarchyLevel + 1;
        //        model.OwnerId = currentUserId;
        //        model.CreatedBy = currentUserId;
        //        var rootObject = _objectRepository.GetById(model.RootId.Value);
        //        var uploadResult = true;
        //        // random filename
        //        _objectRepository.AddAsync(model);
        //        _objectRepository.SaveAsync();


        //        //return new Common.Dto.DocumentManagerResponseBase(model);

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Common.Dto.DocumentManagerResponseBase UpdateDocument(Common.Dto.DocumentRequests request)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> Create(Common.Dto.DocumentRequest dto)
        //{
        //    try
        //    {
        //        var model = dto.CreateModel();
        //        _documentRepository.AddAsync(model);
        //        return _documentRepository.SaveAsync();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
