using Microsoft.AspNetCore.Http;
namespace Contracts
{
    public interface ICloudinaryService
    {
        string UploadFileAndGetURL(IFormFile file);
    }
}
