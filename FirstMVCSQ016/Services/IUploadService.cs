namespace FirstMVCSQ016.Services
{
    public interface IUploadService
    {
        Task<Dictionary<string, string>> UploadImage(IFormFile photo, string folderName);
    }
}
