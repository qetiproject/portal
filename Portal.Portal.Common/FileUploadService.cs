using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Portal.Portal.Common
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _filePath;

        public FileUploadService()
        {
            _filePath = Path.Combine("App_Data", "Files");
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }
        }

        public async Task<FileUploadResponse> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var fileName = GenerateUniqueFileName(file.FileName);
            var filePath = Path.Combine(_filePath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var fileExtension = Path.GetExtension(file.FileName);

            return new FileUploadResponse(file.FileName, filePath, GetFileTypeFromExtension(fileExtension));
        }

        private static string GenerateUniqueFileName(string originalFileName)
        {
            var uniqueId = Guid.NewGuid().ToString();
            return $"{uniqueId}_{originalFileName}";
        }

        private FileType GetFileTypeFromExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            switch (extension)
            {
                case ".docx":
                case ".doc":
                    return FileType.Word;
                case ".xlsx":
                case ".xls":
                    return FileType.Excel;
                case ".pptx":
                case ".ppt":
                    return FileType.PowerPoint;
                case ".pdf":
                    return FileType.PDF;
                case ".png":
                case ".jpg":
                case ".jpeg":
                    return FileType.Image;
                default:
                    return FileType.File;
            }
        }
    }

    public interface IFileUploadService
    {
        Task<FileUploadResponse> UploadFileAsync(IFormFile file);
    }

    public class FileUploadResponse
    {
        public FileUploadResponse(
            string fileName,
            string filePath,
            FileType type)
        {
            FileName = fileName;
            FilePath = filePath;
            Type = type;
        }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public FileType Type { get; set; }
    }

    public enum FileType
    {
        Word,
        Excel,
        PowerPoint,
        PDF,
        Image,
        File
    }
}
