using Data.Entities;
using Domain.Extensions;
using Domain.Models;

namespace Business.Factories;

public class UserProfileFactory
{
    public static UserProfile ToModel(UserProfileEntity entity)
    {
        return entity.MapTo<UserProfile>();
    }

    public static UserProfileEntity ToEntity(UserProfile model)
    {
        return model.MapTo<UserProfileEntity>();
    }
}
