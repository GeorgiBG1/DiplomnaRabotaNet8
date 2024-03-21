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
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images));
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

            #region Services
            CreateMap<SkillBoxService, ServiceCardDTO>()
                 .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                 .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.OwnerName))
                 .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                 .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
                 .ForMember(d => d.MainImage, opt => opt.MapFrom(s => s.MainImage))
                 .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                 .ForMember(d => d.Discount, opt => opt.MapFrom(s => s.Discount));
            #endregion

            #region Categories
            CreateMap<Category, CategoryDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(d => d.Name))
                .ForMember(d => d.MainImage, opt => opt.MapFrom(d => d.MainImage))
                .ForMember(d => d.ParentCategoryId, opt => opt.MapFrom(d => d.ParentCategoryId))
                .ForMember(d => d.ParentCategory, opt => opt.MapFrom(d => d.ParentCategory))
                .ForMember(d => d.Kids, opt => opt.MapFrom(d => d.Kids))
                .ForMember(d => d.Services, opt => opt.MapFrom(d => d.Services));

            CreateMap<Category, CategoryCardDTO>()
                .ForMember(d => d.Name, opt => opt.MapFrom(d => d.Name))
                .ForMember(d => d.MainImage, opt => opt.MapFrom(d => d.MainImage))
                .ForMember(d => d.ServicesCount, opt => opt.MapFrom(c => c.Services!.Count()));
            //TODO Get ParentCategory (ServiceCount) from Kids (ServiceCount)
                
            CreateMap<Category, SelectListItem>()
                .ForMember(d => d.Value, opt => opt.MapFrom(d => d.Id))
                .ForMember(d => d.Text, opt => opt.MapFrom(d => d.Name));
            #endregion

            #region Users
            CreateMap<SkillBoxUser, UserCardDTO>()
                .ForMember(d => d.Username, opt => opt.MapFrom(u => u.UserName))
                .ForMember(d => d.Name, opt => opt.MapFrom(u => $"{u.FirstName} {u.LastName}"))
                .ForMember(d => d.ReviewAvgCoef, opt => opt.MapFrom((u, d) =>
                {
                    var reviews = u.Services.SelectMany(s => s.Reviews!).ToList();
                    var stars = reviews.Select(r => r.RatingStars).ToList();
                    var starsSum = stars.Sum();
                    var reviewAvgCoef = (double)starsSum! / stars.Count();
                    return reviewAvgCoef;
                })) //TODO Test
                .ForMember(d => d.ReviewsCount, opt => opt.MapFrom((u, d) =>
                {
                    var reviews = u.Services.SelectMany(s => s.Reviews!).ToList();
                    return reviews.Count();
                }))
                .ForMember(d => d.Skills, opt => opt.MapFrom(u => u.Skills!))
                .ForMember(d => d.City, opt => opt.MapFrom(u => u.City!));
            //TODO Add more members
            #endregion
        }
    }
}
