using AutoMapper;
using Data.Models;
using DTOs.INPUT;
using DTOs.OUTPUT;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace SkillBox.App.AutoMapperConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //IN
            CreateMap<ServiceInDTO, SkillBoxService>()
                .ForMember(s => s.Images, opt => opt.MapFrom(d => d.Images));
            //TODO add more members

            //OUT
            #region Chats
            CreateMap<Chat, ChatMiniDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(c => c.Name))
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

            #region Services
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
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Schedule, opt => opt.MapFrom(s => s.Schedule))
                //TODO add Skill or Career
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
                .ForMember(d => d.OwnerProfilePhoto, opt => opt.MapFrom(s => s.Owner.ProfilePhoto))
                .ForMember(d => d.OwnerCurrentLocation, opt => opt.MapFrom(s => s.Owner.City.BGName));
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
                }));

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
                .ForMember(d => d.City, opt => opt.MapFrom(u => u.City!.BGName));

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
