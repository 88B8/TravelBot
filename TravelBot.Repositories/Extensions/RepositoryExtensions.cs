using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;

namespace TravelBot.Repositories.Extensions;

/// <summary>
///     Расширения для репозиториев
/// </summary>
public static class RepositoryExtensions
{
    /// <summary>
    ///     Приводит <see cref="Place" /> к <see cref="PlaceDbModel" />
    /// </summary>
    public static IQueryable<PlaceDbModel> SelectPlaceDbModel(this IQueryable<Place> query)
    {
        return query.Select(x => new PlaceDbModel
        {
            Id = x.Id,
            Address = x.Address,
            Category = x.Category,
            ChildFriendly = x.ChildFriendly,
            Description = x.Description,
            Link = x.Link,
            Metro = x.Metro,
            Name = x.Name
        });
    }

    /// <summary>
    ///     Приводит <see cref="Route" /> к <see cref="RouteDbModel" />
    /// </summary>
    public static IQueryable<RouteDbModel> SelectRouteDbModel(this IQueryable<Route> query)
    {
        return query.Select(x => new RouteDbModel
        {
            Id = x.Id,
            AverageTime = x.AverageTime,
            Budget = x.Budget,
            ReasonToVisit = x.ReasonToVisit,
            Season = x.Season,
            StartPoint = x.StartPoint,
            Places = x.RoutePlaces
                .Where(y => y.DeletedAt == null)
                .Select(y => new PlaceDbModel
                {
                    Id = y.Place.Id,
                    Address = y.Place.Address,
                    Category = y.Place.Category,
                    ChildFriendly = y.Place.ChildFriendly,
                    Description = y.Place.Description,
                    Link = y.Place.Link,
                    Metro = y.Place.Metro,
                    Name = y.Place.Name
                })
                .ToList()
        });
    }

    /// <summary>
    ///     Приводит <see cref="Passport" /> к <see cref="PassportDbModel" />
    /// </summary>
    public static IQueryable<PassportDbModel> SelectPassportDbModel(this IQueryable<Passport> query)
    {
        return query.Select(x => new PassportDbModel
        {
            Id = x.Id,
            Places = x.PassportPlaces
                .Where(y => y.DeletedAt == null)
                .Select(y => new PlaceDbModel
                {
                    Id = y.Place.Id,
                    Address = y.Place.Address,
                    Category = y.Place.Category,
                    ChildFriendly = y.Place.ChildFriendly,
                    Description = y.Place.Description,
                    Link = y.Place.Link,
                    Metro = y.Place.Metro,
                    Name = y.Place.Name
                })
                .ToList()
        });
    }

    /// <summary>
    ///     Приводит <see cref="User" /> к <see cref="UserDbModel" />
    /// </summary>
    public static IQueryable<UserDbModel> SelectUserDbModel(this IQueryable<User> query)
    {
        return query.Select(x => new UserDbModel
        {
            Id = x.Id,
            Name = x.Name,
            TelegramId = x.TelegramId,
            Passport = new PassportDbModel
            {
                Id = x.Passport.Id,
                Places = x.Passport.PassportPlaces
                    .Where(y => y.DeletedAt == null)
                    .Select(p => new PlaceDbModel
                    {
                        Id = p.Place.Id,
                        Address = p.Place.Address,
                        Category = p.Place.Category,
                        ChildFriendly = p.Place.ChildFriendly,
                        Description = p.Place.Description,
                        Link = p.Place.Link,
                        Metro = p.Place.Metro,
                        Name = p.Place.Name
                    })
                    .ToList()
            }
        });
    }
}