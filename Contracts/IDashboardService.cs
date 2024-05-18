namespace Contracts
{
    public interface IDashboardService
    {
        public int GetMyServicesCount(string username);
        public int GetMyReviewsCount(string username);
        public int GetMyFavServicesCount(string username);
        public int GetMyFavSkillersCount(string username);
    }
}
