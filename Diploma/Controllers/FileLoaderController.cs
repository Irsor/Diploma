using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Json;
using DevExpress.Pdf.Native;
using Diploma.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class FileLoaderController : Controller
    {
        public FileLoaderController(IFileLoader loader, DashboardConfigurator configurator, IConfiguration configuration)
        {
            _loader = loader;
            _configurator = configurator;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            foreach (var file in _loader.GetFileList())
            {
                Console.WriteLine(file);
            }
            _fileDataViewModel.FileNames = _loader.GetFileList();
            return View(_fileDataViewModel);
        }

        [HttpPost]
        public IActionResult UploadFiles(IFormFileCollection uploadingFiles)
        {
            foreach (var file in uploadingFiles)
            {
                _loader.UploadFile(file);
            }
            _fileDataViewModel.FileNames = _loader.GetFileList();
            return View("Index", _fileDataViewModel);
        }

        public IActionResult DeleteFile(string fileName)
        {
            _loader.DeleteFile(fileName);
            _fileDataViewModel.FileNames = _loader.GetFileList();
            return View("Index", _fileDataViewModel);
        }

        public IActionResult Analyze(string fileName)
        {
            _loader.ConvertToJson(fileName); 
            _fileDataViewModel.FileNames = _loader.GetFileList();
            return View("Index", _fileDataViewModel);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult File(string DataSourceFileName)
        {
            string json = _loader.GetFile(DataSourceFileName);
            if (json != "File not exist!") 
            {
                DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();             
                DashboardJsonDataSource jsonDataSourceString = new CustomDataSource($"JSON Data Source ({DataSourceFileName})", json);
                dataSourceStorage.RegisterDataSource(DataSourceFileName, jsonDataSourceString.SaveToXml());
                _configurator.SetDataSourceStorage(dataSourceStorage);
                _configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(_configuration));
            }
            else
            {
                Console.WriteLine("File not exist");
            }
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult ReturnToMenu(string Result)
        {
            return View("Index", _fileDataViewModel);
        }

        private FileDataViewModel _fileDataViewModel = new();

        private IFileLoader _loader;

        private DashboardConfigurator _configurator;

        private IConfiguration _configuration;

    }
}
