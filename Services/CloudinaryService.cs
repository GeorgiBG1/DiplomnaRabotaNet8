using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Contracts;

namespace Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        private readonly Cloudinary cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            this.configuration = configuration;
            var subsection = this.configuration
                .GetRequiredSection("Cloudinary");

            account = new Account(
              subsection["CloudName"],
              subsection["ApiKey"],
              subsection["ApiSecret"]);

            cloudinary = new Cloudinary(account);
        }

        public string UploadFileAndGetURL(IFormFile file, string directory)
        {
            //Upload files to Cloudinary
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName,
                file.OpenReadStream()),
                Folder = directory
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return "none";
        }
    }
}