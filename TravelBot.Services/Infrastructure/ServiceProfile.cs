using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TravelBot.Entities;
using TravelBot.Entities.Enums;
using TravelBot.Repositories.Contracts.Models;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.Enums;
using TravelBot.Services.Contracts.Models.RequestModels;

namespace TravelBot.Services.Infrastructure
{
    /// <summary>
    /// Сервисный автомаппер
    /// </summary>
    public class ServiceProfile : Profile
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ServiceProfile()
        {
            // Enums
            CreateMap<Season, SeasonModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            // Entity -> ResponseModel
            CreateMap<Admin, AdminModel>(MemberList.Destination);
            CreateMap<Category, CategoryModel>(MemberList.Destination);
            CreateMap<Passport, PassportModel>(MemberList.Destination)
                .ForMember(dest => dest.Places,
                    opt => opt.MapFrom(src => src.PassportPlaces
                        .Select(x => x.Place))).ReverseMap();
            CreateMap<Place, PlaceModel>(MemberList.Destination);
            CreateMap<Route, RouteModel>(MemberList.Destination)
                .ForMember(dest => dest.Places,
                    opt => opt.MapFrom(src => src.RoutePlaces
                        .Select(x => x.Place))).ReverseMap();
            CreateMap<RoutePlace, RoutePlaceModel>(MemberList.Destination);
            CreateMap<User, UserModel>(MemberList.Destination);
            CreateMap<PassportPlace, PassportPlaceModel>(MemberList.Destination);

            // CreateModel -> Entity
            CreateMap<AdminCreateModel, Admin>(MemberList.Source);
            CreateMap<CategoryCreateModel, Category>(MemberList.Source);
            CreateMap<PassportCreateModel, Passport>(MemberList.Source);
            CreateMap<PlaceCreateModel, Place>(MemberList.Source);
            CreateMap<RouteCreateModel, Route>(MemberList.Source);
            CreateMap<RoutePlaceCreateModel, RoutePlace>(MemberList.Source);
            CreateMap<UserCreateModel, User>(MemberList.Source);
            CreateMap<PassportPlaceCreateModel, PassportPlace>(MemberList.Source);

            // DbModel -> ResponseModel
            CreateMap<PassportDbModel, PassportModel>(MemberList.Destination).ReverseMap();
            CreateMap<PlaceDbModel, PlaceModel>(MemberList.Destination).ReverseMap();
            CreateMap<RouteDbModel, RouteModel>(MemberList.Destination).ReverseMap();
            CreateMap<UserDbModel, UserModel>(MemberList.Destination).ReverseMap();

            // ResponseModel -> CreateModel
            CreateMap<PassportModel, PassportCreateModel>(MemberList.Destination)
                .ForMember(dest => dest.PlaceIds,
                    opt => opt.MapFrom(src => src.Places
                        .Select(x => x.Id))).ReverseMap();
            CreateMap<RouteModel, RouteCreateModel>(MemberList.Destination)
                .ForMember(dest => dest.PlaceIds,
                    opt => opt.MapFrom(src => src.Places
                        .Select(x => x.Id))).ReverseMap();
        }
    }
}