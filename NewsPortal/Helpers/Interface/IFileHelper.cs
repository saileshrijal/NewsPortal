namespace NewsPortal.Helpers.Interface
{
    public interface IFileHelper
    {
        Task<string> UploadFile(IFormFile file, string folderName);
        //delete file from server
        void DeleteFile(string fileName, string folderName);
    }
}