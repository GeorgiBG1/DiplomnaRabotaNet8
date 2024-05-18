using Contracts;
using Data;

namespace Services
{
    public class DashboardService : IDashboardService
    {
        private readonly SkillBoxDbContext dbContext;

        public DashboardService(SkillBoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int GetMyServicesCount(string username)
        {
            return dbContext.Services.Where(s => s.Owner.UserName == username).Count();
        }
        int IDashboardService.GetMyFavServicesCount(string username)
        {
            //TODO dbContext.Favourites
            return 0;
        }
        int IDashboardService.GetMyFavSkillersCount(string username)
        {
            //TODO dbContext.Favourites
            return 0;
        }
        int IDashboardService.GetMyReviewsCount(string username)
        {
            return dbContext.Reviews.Where(s => s.User.UserName == username).Count();
        }
    }
}
