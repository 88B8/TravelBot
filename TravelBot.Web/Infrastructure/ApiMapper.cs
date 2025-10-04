using AutoMapper;
using TravelBot.Services.Contracts.Models.Auth;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Web.Models.Auth;
using TravelBot.Web.Models.CreateApiModels;
using TravelBot.Web.Models.ResponseApiModels;

namespace TravelBot.Web.Infrastructure
{
    /// <summary>
    /// Настройка автомаппера
    /// </summary>
    public class ApiMapper : Profile
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ApiMapper()
        {
            // DTO Model -> Api Model
            CreateMap<AdminModel, AdminApiModel>(MemberList.Destination);
            CreateMap<CategoryModel, CategoryApiModel>(MemberList.Destination);
            CreateMap<LoginRequestModel, LoginRequestApiModel>(MemberList.Destination).ReverseMap();
            CreateMap<PassportModel, PassportApiModel>(MemberList.Destination);
            CreateMap<PlaceModel, PlaceApiModel>(MemberList.Destination);
            CreateMap<RouteModel, RouteApiModel>(MemberList.Destination);
            CreateMap<RoutePlaceModel, RoutePlaceApiModel>(MemberList.Destination);
            CreateMap<UserModel, UserApiModel>(MemberList.Destination);
            CreateMap<PassportPlaceModel, PassportPlaceApiModel>(MemberList.Destination);

            // Create API Models -> Create Models
            CreateMap<AdminCreateApiModel, AdminCreateModel>(MemberList.Destination);
            CreateMap<CategoryCreateApiModel, CategoryCreateModel>(MemberList.Destination);
            CreateMap<PassportCreateApiModel, PassportCreateModel>(MemberList.Destination);
            CreateMap<PlaceCreateApiModel, PlaceCreateModel>(MemberList.Destination);
            CreateMap<RouteCreateApiModel, RouteCreateModel>(MemberList.Destination);
            CreateMap<RoutePlaceCreateApiModel, RoutePlaceCreateModel>(MemberList.Destination);
            CreateMap<UserCreateApiModel, UserCreateModel>(MemberList.Destination);
            CreateMap<PassportPlaceCreateApiModel, PassportPlaceCreateModel>(MemberList.Destination);
        }
    }
}