using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Contracts;

namespace SkillBox.App.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private Account account;
        private Cloudinary cloudinary;

        public CloudinaryService()
        {
            account = new Account(
                "",
                "",
                "");
            cloudinary = new Cloudinary(account);
        }
        public string UploadFileAndGetURL(IFormFile file)
        {
            //Upload files to Cloudinary
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName,
                file.OpenReadStream())
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
    }
}