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

        public List<string> FileNames { get; set; } = new();

        private FileDataViewModel _fileDataViewModel = new();

        private IFileLoader _loader;
        
    }
}
