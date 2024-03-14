using Contracts;
using Data;

namespace Services
{
    public class ImageService : IImageService
    {
        private readonly SkillBoxDbContext dbContext;

        public ImageService(SkillBoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
