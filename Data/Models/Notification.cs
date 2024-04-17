namespace Data.Models
{
    public class Notification : BaseEntity<string>
    {
        public Notification()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Content { get; set; }
        public string Type { get; set; }
        public string ImageURL { get; set; }
        public string OwnerId { get; set; }
        public virtual SkillBoxUser Owner { get; set; }
        public void SetImageURLAndSetContent(string type)
        {
            switch (type)
            {
                case "NewService":
                    Type = type;
                    Content = "Успешно създадохте услуга!";
                    ImageURL = "https://res.cloudinary.com/ddiyhqypo/image/upload/v1713384097/Notifications/u3au3kcncwwq3zzfehmg.png";
                    break;
                case "UpdateService":
                    Type = type;
                    Content = "Успешно редактирахте услугата!";
                    ImageURL = "https://res.cloudinary.com/ddiyhqypo/image/upload/v1713384174/Notifications/cowxaisdwklicylatgan.png";
                    break;
                case "UpdateUserProps":
                    Type = type;
                    Content = "Успешно направихте промени по профила си!";
                    ImageURL = "https://res.cloudinary.com/ddiyhqypo/image/upload/v1713384227/Notifications/bkslq1pst9bynsik32n3.png";
                    break;
                default:
                    Type = string.Empty;
                    Content = string.Empty;
                    ImageURL = string.Empty;
                    break;
            }
        }
    }
}
