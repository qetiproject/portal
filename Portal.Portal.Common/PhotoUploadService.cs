using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Common
{
    public class PhotoUploadService : IPhotoUploadService
    {
        private readonly string _photoPath;

        public PhotoUploadService()
        {
            _photoPath = Path.Combine("App_Data", "Photos");
            if (!Directory.Exists(_photoPath))
            {
                Directory.CreateDirectory(_photoPath);
            }
        }

        public async Task<PhotoUploadResponse> UploadPhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            if (!IsSupportedFileType(file.FileName))
            {
                throw new ArgumentException("Invalid file type");
            }

            var fileName = GenerateUniqueFileName(file.FileName);
            var filePath = Path.Combine(_photoPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return new PhotoUploadResponse(fileName, filePath);
        }

        private static bool IsSupportedFileType(string fileName)
        {
            var supportedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return supportedExtensions.Contains(extension);
        }

        private static string GenerateUniqueFileName(string originalFileName)
        {
            var uniqueId = Guid.NewGuid().ToString();
            return $"{uniqueId}_{originalFileName}";
        }
    }

    public interface IPhotoUploadService
    {
        Task<PhotoUploadResponse> UploadPhotoAsync(IFormFile file);
    }

    public class PhotoUploadResponse
    {
        public PhotoUploadResponse(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
        }

        public string FileName { get; set; }

        public string FilePath { get; set; }
    }
}
