using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using digiman_common.Dto.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace digiman_service.DigiDocu.v1
{
    public class BaseService
    {

        protected IConfiguration _configuration;
        protected digiman_dal.Models.DigimanContext _context;
        protected UserLoginInfo _loggedUserInfo;
        public BaseService(UserLoginInfo loggedInfo)
        {
            _loggedUserInfo = loggedInfo;

            InitializeConfig();
            InitializeContext();

        }

        public BaseService()
        {
            InitializeConfig();
            InitializeContext();
        }

        private void InitializeConfig()
        {
            var builder = new ConfigurationBuilder()
         .SetBasePath(System.IO.Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            _configuration = builder.Build();
        }

        private void InitializeContext()
        {
            var connectionString = _configuration.GetConnectionString("dbSql");
            var optionsBuilder = new DbContextOptionsBuilder<digiman_dal.Models.DigimanContext>();
            optionsBuilder.UseSqlServer(connectionString);

            _context = new digiman_dal.Models.DigimanContext(optionsBuilder.Options);
        }

        //public async Task<string> SaveFile(IFormFile file,UploadTypeEnum uploadType)
        //{
        //    string filename = "";
        //    if (file.Length > 0)
        //    {
        //        filename = Guid.NewGuid().ToString().Replace("-", "");
        //        string filePath = "";

        //        switch (uploadType)
        //        {
        //            case UploadTypeEnum.Document:
        //                filePath = _parameterRepository.GetWithCondition(i=>i.Name=="temp.folder").FirstOrDefault().Value;
        //                break;
        //            case UploadTypeEnum.Watermark:
        //                filePath = Common.Settings.AppSystem.WatermarkFolder;
        //                filename += Path.GetExtension(file.FileName);
        //                break;
        //        }

        //        //check if folder has been created
        //        if (Directory.Exists(filePath) == false)
        //            Directory.CreateDirectory(filePath);

        //        string fullyFilename = Path.Combine(filePath,filename);
        //        using (var stream = File.Create(fullyFilename))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //    }

        //    return filename;
        //}


    }
}
