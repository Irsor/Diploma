namespace Diploma.Models
{
    public interface IFileLoader
    {
        public void ConvertToJson(string fileName);

        public void DeleteFile(string fileName);

        public string GetFile(string fileName);

        public List<string> GetFileList();

        public void UploadFile(IFormFile file);
    }
}