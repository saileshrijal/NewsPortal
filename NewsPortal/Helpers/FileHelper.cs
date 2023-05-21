using System.Transactions;
using NewsPortal.Helpers.Interface;

namespace NewsPortal.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFile(IFormFile file, string folderName)
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", folderName);
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(directoryPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            tx.Complete();
            return fileName;
        }
    }
}