using System.Security.Cryptography;
using System.Text;

namespace Diploma.Models
{
    public class DefaultFileLoader : IFileLoader
    {
        private List<string> _fileNames = new();
        private string _folderName = "AppData";
        private MD5 _md5 = MD5.Create();

        public string GetFile(string fileName)
        {
            var hash = BitConverter.ToString(_md5.ComputeHash((Encoding.UTF8.GetBytes(fileName)))).Replace("-", "");
            var path = $"{_folderName}\\{hash.Substring(0, 2)}\\{hash.Substring(2, 2)}\\{hash}";
            string result = File.ReadAllText(fileName);
            return result;
        }

        public List<string> GetFileList()
        {
            return _fileNames;
        }

        public void UploadFile(IFormFile file)
        {
            string fileName = file.FileName;
            if (_fileNames.Contains(file.FileName))
            {
                fileName = GetIndex(file.FileName);
            }
            _fileNames.Add(fileName);
            var hash = BitConverter.ToString(_md5.ComputeHash(Encoding.UTF8.GetBytes(fileName))).Replace("-", "");
            var path = $"{_folderName}\\{hash.Substring(0, 2)}\\{hash.Substring(2, 2)}\\";
            Directory.CreateDirectory(path);
            using (FileStream fileStream = File.Create($"{path}{fileName}"))
            {
                try
                {
                    file.CopyTo(fileStream);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error - ");
                    Console.WriteLine(hash);
                    throw;
                }
            }
        }

        private string GetIndex(string fileName, int index = 1)
        {
            var modifiedFileName = $"{Path.GetFileNameWithoutExtension(fileName)}({index}){Path.GetExtension(fileName)}";
            if (_fileNames.Contains(modifiedFileName))
            {
                index++;
                return GetIndex(fileName, index);
            }
            else
            {
                return modifiedFileName;
            }
        }
    }
}