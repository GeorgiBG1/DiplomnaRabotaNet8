using Microsoft.AspNetCore.Http;
namespace Contracts
{
    public interface ICloudinaryService
    {
        public string UploadFileAndGetURL(IFormFile file, string directory);
    }
}
