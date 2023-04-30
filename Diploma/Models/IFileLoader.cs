namespace Diploma.Models
{
    public interface IFileLoader
    {
        public string GetFile(string fileName);

        public List<string> GetFileList();

        public void UploadFile(IFormFile file);
    }
}