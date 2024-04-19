using AutoMapper;
using Data.Models;
using DTOs.INPUT;
using DTOs.OUTPUT;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace SkillBox.App.AutoMapperConfiguration
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //IN
            #region Services
            CreateMap<ServiceInDTO, SkillBoxService>()
                .ForMember(s => s.Name, opt => opt.MapFrom(d => d.Title))
                .ForMember(s => s.Description, opt => opt.MapFrom(d => d.Description))
                .ForMember(s => s.PhoneNumber, opt => opt.MapFrom(d => d.PhoneNumber))
                .ForMember(s => s.WebsiteName, opt => opt.MapFrom(d => d.WebsiteName))
                .ForMember(s => s.Price, opt => opt.MapFrom(d => d.Price))
                .ForMember(s => s.UnitPrice, opt => opt.MapFrom(d => d.UnitPrice))
                .ForMember(s => s.Category, opt => opt.MapFrom(d => d.Category))
                .ForMember(s => s.City, opt => opt.MapFrom(d => d.City))
                .ForMember(s => s.Owner, opt => opt.MapFrom(d => d.User))
                .ForMember(s => s.OwnerName, opt => opt.MapFrom(d => $"{d.User.FirstName} {d.User.LastName}"))
                .ForMember(s => s.ServiceStatus, opt => opt.MapFrom(d => d.Status))
                .ForMember(s => s.MainSkill, opt => opt.MapFrom(d => d.Skills[d.SkillId].Name))
                .ForMember(s => s.Schedule, opt => opt.MapFrom(d => d.DaysAsString()))
                .ForMember(s => s.MainImage, opt => opt.MapFrom(d => d.MainImage))
                .ForMember(s => s.Images, opt => opt.MapFrom(d => d.Images));

            CreateMap<ServiceUpdateDTO, SkillBoxService>()
                .ForMember(s => s.Name, opt => opt.MapFrom(d => d.Title))
                .ForMember(s => s.Description, opt => opt.MapFrom(d => d.Description))
                .ForMember(s => s.PhoneNumber, opt => opt.MapFrom(d => d.PhoneNumber))
                .ForMember(s => s.WebsiteName, opt => opt.MapFrom(d => d.WebsiteName))
                .ForMember(s => s.Price, opt => opt.MapFrom(d => d.Price))
                .ForMember(s => s.UnitPrice, opt => opt.MapFrom(d => d.UnitPrice))
                .ForMember(s => s.Discount, opt => opt.MapFrom(d => d.Discount))
                .ForMember(s => s.Category, opt => opt.MapFrom(d => d.Category))
                .ForMember(s => s.City, opt => opt.MapFrom(d => d.City))
                .ForMember(s => s.ServiceStatus, opt => opt.MapFrom(d => d.Status))
                .ForMember(s => s.MainSkill, opt => opt.MapFrom(d => d.Skills[d.SkillId].Name))
                .ForMember(s => s.Schedule, opt => opt.MapFrom(d => d.DaysAsString()))
                .ForMember(s => s.MainImage, opt => opt.MapFrom(d => d.MainImage))
                .ForMember(s => s.Images, opt => opt.MapFrom(d => d.Images));
            #endregion

            //OUT
            #region Chats
            CreateMap<Chat, ChatMiniDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.ServiceName, opt => opt.MapFrom(c => c.Service.Name))
                .ForMember(d => d.Photo, opt => opt.MapFrom(c => c.ChatUsers.Select(c => c.User.ProfilePhoto).First()))
                .ForMember(d => d.ParticipantsCount, opt => opt.MapFrom(c => c.ChatUsers.Count()));

            CreateMap<Chat, ChatDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.ServiceName, opt => opt.MapFrom(c => c.Service.Name))
                .ForMember(d => d.ChatUsers, opt => opt.MapFrom(c => c.ChatUsers))
                .ForMember(d => d.Messages, opt => opt.MapFrom(c => c.Messages));
            #endregion

            #region Messages
            CreateMap<UserMessage, MessageDTO>()
                .ForMember(d => d.Content, opt => opt.MapFrom(m => m.Content))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(m => m.CreatedOn.ToString("dd.MM.yyyy г. HH:mm")))
                .ForMember(d => d.UserFullName, opt => opt.MapFrom(m => $"{m.Owner.FirstName} {m.Owner.LastName}"))
                .ForMember(d => d.UserProfilePhoto, opt => opt.MapFrom(m => m.Owner.ProfilePhoto));
            #endregion

            #region Reviews
            CreateMap<Review, ReviewDTO>()
                .ForMember(d => d.User, opt =>
                opt.MapFrom(r => new UserMiniDTO
                {
                    Name = $"{r.User.FirstName} {r.User.LastName}",
                    ProfilePhoto = r.User.ProfilePhoto
                }
                ))
                .ForMember(d => d.StarsCount, opt => opt.MapFrom(r => r.RatingStars))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(r => r.CreatedOn.ToString("MMMM dd, yyyy")))
                .ForMember(d => d.Content, opt => opt.MapFrom(r => r.Comment));
            #endregion

            #region Services
            CreateMap<SkillBoxService, ServiceInfoDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => $"{s.Price:F2}"))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.City.BGName))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.ServiceStatus.BGName));

            CreateMap<SkillBoxService, ServiceMiniDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.City.BGName))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(s => s.CreatedOn.ToString("MMMM dd, yyyy")))
                .ForMember(d => d.OfferingsCount, opt => opt.MapFrom(s => s.Owner.Offerings.Count))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => $"{s.Price:F2}"))
                .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.UnitPrice))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(s => s.ServiceStatus.Id));

            CreateMap<SkillBoxService, ServiceCardDTO>()
                 .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                 .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                 .ForMember(d => d.AuthorUsername, opt => opt.MapFrom(s => s.Owner.UserName))
                 .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.OwnerName))
                 .ForMember(d => d.AuthorProfilePhoto, opt => opt.MapFrom(s => s.Owner.ProfilePhoto))
                 .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                 .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
                 .ForMember(d => d.MainImage, opt => opt.MapFrom(s => s.MainImage))
                 .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                 .ForMember(d => d.Discount, opt => opt.MapFrom(s => s.Discount))
                 .ForMember(d => d.ReviewsCount, opt => opt.MapFrom(s => s.Reviews!.Count()))
                 .ForMember(d => d.ReviewAvgCoef, opt => opt.MapFrom((s, d) =>
                 {
                     var reviews = s.Reviews!.ToList();
                     var stars = reviews.Select(r => r.RatingStars).ToList();
                     var starsSum = stars.Sum();
                     var reviewAvgCoef = (double)starsSum! / stars.Count();
                     return reviewAvgCoef;
                 }));

            CreateMap<SkillBoxService, ServiceDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Schedule, opt => opt.MapFrom(s => s.Schedule))
                .ForMember(d => d.MainSkill, opt => opt.MapFrom(s => s.MainSkill))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.City.BGName))
                .ForMember(d => d.Images, opt => opt.MapFrom((s, d) =>
                {
                    var imageCollection = new List<string> { s.MainImage };
                    var images = s.Images.Split('|').ToList();
                    images.RemoveAt(images.Count - 1);
                    images.ForEach(i => imageCollection.Add(i));
                    return imageCollection;
                }))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.ReviewsCount, opt => opt.MapFrom(s => s.Reviews!.Count()))
                .ForMember(d => d.ReviewAvgCoef, opt => opt.MapFrom((s, d) =>
                {
                    var reviews = s.Reviews!.ToList();
                    var stars = reviews.Select(r => r.RatingStars).ToList();
                    var starsSum = stars.Sum();
                    var reviewAvgCoef = (double)starsSum! / stars.Count();
                    return reviewAvgCoef;
                }))
                .ForMember(d => d.Reviews, opt => opt.MapFrom((s, d) =>
                {
                    var reviews = s.Reviews!.OrderByDescending(r => r.CreatedOn)
                    .Take(2).ToList();
                    var reviewDTOs = new List<ReviewDTO>();
                    reviews.ForEach(r => reviewDTOs.Add(new ReviewDTO
                    {
                        User = new UserMiniDTO
                        {
                            Name = $"{r.User.FirstName} {r.User.LastName}",
                            ProfilePhoto = r.User.ProfilePhoto
                        },
                        Content = r.Comment,
                        CreatedOn = r.CreatedOn.ToString("dd MMMM yyyy"),
                    }));
                    return reviewDTOs;
                }))
                .ForMember(d => d.OwnerUsername, opt => opt.MapFrom(s => s.Owner.UserName))
                .ForMember(d => d.OwnerName, opt => opt.MapFrom(s => s.OwnerName))
                .ForMember(d => d.OwnerCareer, opt => opt.MapFrom(s => s.Owner.Career))
                .ForMember(d => d.OwnerProfilePhoto, opt => opt.MapFrom(s => s.Owner.ProfilePhoto))
                .ForMember(d => d.OwnerCurrentLocation, opt => opt.MapFrom(s => s.Owner.City.BGName));

            CreateMap<SkillBoxService, ServiceUpdateDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.WebsiteName, opt => opt.MapFrom(s => s.WebsiteName))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(d => d.Discount, opt => opt.MapFrom(s => s.Discount))
                .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.UnitPrice))
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                .ForMember(d => d.User, opt => opt.MapFrom(s => s.Owner))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.ServiceStatus))
                .ForMember(d => d.Days, opt => opt.MapFrom((s, d) =>
                {
                    d.Days = s.DaysFromString(s.Schedule!);
                    return d.Days;
                }))
                .ForMember(d => d.MainSkill, opt => opt.MapFrom(s => s.MainSkill))
                .ForMember(d => d.Skills, opt => opt.MapFrom(s => s.Owner.Skills))
                .ForMember(d => d.MainImage, opt => opt.MapFrom(d => d.MainImage))
                .ForMember(d => d.Images, opt => opt.MapFrom(d => d.Images));
            #endregion

            #region Categories
            CreateMap<Category, CategoryMiniDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name));

            CreateMap<Category, SelectListItem>()
                .ForMember(d => d.Value, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Text, opt => opt.MapFrom(c => c.Name));

            CreateMap<Category, CategoryCardDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.MainImage, opt => opt.MapFrom(c => c.MainImage))
                .ForMember(d => d.ServicesCount, opt => opt.MapFrom((c, d) =>
                {
                    if (c.Kids!.Count != 0)
                    {
                        return d.ServicesCount = c.Kids.Select(k => k.Services!.Count).Sum();
                    }
                    return d.ServicesCount = c.Services!.Count;
                }))
                .ForMember(d => d.VisitsCount, opt => opt.MapFrom(c => c.Services!.Select(s => s.VisitsCount).Sum()));

            CreateMap<Category, CategoryDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(d => d.MainImage, opt => opt.MapFrom(c => c.MainImage))
                .ForMember(d => d.ParentCategoryId, opt => opt.MapFrom(c => c.ParentCategoryId))
                .ForMember(d => d.ParentCategory, opt => opt.MapFrom(c => c.ParentCategory))
                .ForMember(d => d.Kids, opt => opt.MapFrom(c => c.Kids))
                .ForMember(d => d.Services, opt => opt.MapFrom(c => c.Services));
            #endregion

            #region Users
            CreateMap<SkillBoxUser, UserInfoDTO>()
                .ForMember(d => d.Username, opt => opt.MapFrom(u => u.UserName))
                .ForMember(d => d.Name, opt => opt.MapFrom(u => $"{u.FirstName} {u.LastName}"))
                .ForMember(d => d.Location, opt => opt.MapFrom(u => u.City.BGName))
                .ForMember(d => d.ServicesCount, opt => opt.MapFrom(u => u.Services.Count()))
                .ForMember(d => d.ReviewsCount, opt => opt.MapFrom((u => u.Services.SelectMany(s => s.Reviews!).Count())));

            CreateMap<SkillBoxUser, UserCardDTO>()
                .ForMember(d => d.Username, opt => opt.MapFrom(u => u.UserName))
                .ForMember(d => d.Name, opt => opt.MapFrom(u => $"{u.FirstName} {u.LastName}"))
                .ForMember(d => d.ProfilePhoto, opt => opt.MapFrom(u => u.ProfilePhoto))
                .ForMember(d => d.ReviewAvgCoef, opt => opt.MapFrom((u, d) =>
                {
                    var reviews = u.Services.SelectMany(s => s.Reviews!).ToList();
                    var stars = reviews.Select(r => r.RatingStars).ToList();
                    var starsSum = stars.Sum();
                    var reviewAvgCoef = (double)starsSum! / stars.Count();
                    return reviewAvgCoef;
                }))
                .ForMember(d => d.ReviewsCount, opt => opt.MapFrom((u, d) =>
                {
                    var reviews = u.Services.SelectMany(s => s.Reviews!).ToList();
                    return reviews.Count;
                }))
                .ForMember(d => d.ServicesCount, opt => opt.MapFrom(u => u.Services.Count))
                .ForMember(d => d.Career, opt => opt.MapFrom(u => u.Career))
                .ForMember(d => d.Skills, opt => opt.MapFrom(u => u.Skills!.Select(s => s.Name)))
                .ForMember(d => d.City, opt => opt.MapFrom(u => u.City!.BGName))
                .ForMember(d => d.IsBlocked, opt => opt.MapFrom((u, d) =>
                {
                    if (u.LockoutEnd != null && u.LockoutEnd > DateTime.Now)
                    {
                        d.IsBlocked = true;
                    }
                    return d.IsBlocked;
                }));

            CreateMap<SkillBoxUser, UserDTO>()
                .ForMember(d => d.Username, opt => opt.MapFrom(u => u.UserName))
                .ForMember(d => d.Name, opt => opt.MapFrom(u => $"{u.FirstName} {u.LastName}"))
                .ForMember(d => d.ProfilePhoto, opt => opt.MapFrom(u => u.ProfilePhoto))
                .ForMember(d => d.Location, opt => opt.MapFrom(u => u.City.BGName))
                .ForMember(d => d.JoinedOn, opt => opt.MapFrom(u => u.CreatedOn.ToString("MMMM yyyy")))
                .ForMember(d => d.Gender, opt => opt.MapFrom(u => u.Gender.BGName))
                .ForMember(d => d.Career, opt => opt.MapFrom(u => u.Career))
                .ForMember(d => d.Bio, opt => opt.MapFrom(u => u.Bio))
                .ForMember(d => d.WebsiteName, opt => opt.MapFrom(u => u.WebsiteName))
                .ForMember(d => d.Skills, opt => opt.MapFrom(u => u.Skills!.Select(s => $"{s.Name} {s.Level.BGName}")));
            #endregion
        }
    }
}
