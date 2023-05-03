using DevExpress.Pdf.Native;
using Diploma.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    public class FileLoaderController : Controller
    {
        public FileLoaderController(IFileLoader loader)
        {
            _loader = loader;
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
            Console.WriteLine("Hello, World!");
            _loader.ConvertToJson(fileName); 
            _fileDataViewModel.FileNames = _loader.GetFileList();
            return View("Index", _fileDataViewModel);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        private FileDataViewModel _fileDataViewModel = new();

        private IFileLoader _loader;
    }
}
