using Data.Entities;
using Domain.Extensions;
using Domain.Models;
using Domain.Models.UserProfileData;

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

    public static (ApplicationUserEntity appUser, UserProfileEntity userProfile) Create(UserProfileRegistrationForm form)
    {
        var appUser = form.MapTo<ApplicationUserEntity>();
        appUser.Email = form.Email;
        appUser.UserName = form.Email;

        var userProfile = form.MapTo<UserProfileEntity>();
        userProfile.Id = appUser.Id;

        return (appUser, userProfile);
    }

    public static UserProfile ModelOverload(ApplicationUserEntity appUser, UserProfile userProfile)
    {
        userProfile.Email = appUser.Email!;
        return userProfile;
    }

    public static void UpdateEntity(ApplicationUserEntity appUser, UserProfileEntity entity, UserProfileUpdateForm model)
    {
        if (!string.IsNullOrWhiteSpace(model.Email))
        {
            appUser.Email = model.Email;
            appUser.UserName = model.Email;
        }

        if (model.FirstName != null)
        {
            entity.FirstName = model.FirstName;
        }

        if (model.LastName != null)
        {
            entity.LastName = model.LastName;
        }
        if (model.JobTitle != null)
        {
            entity.JobTitle = model.JobTitle;
        }

        if (model.ProfilePicture != null)
        {
            entity.ProfilePicture = model.ProfilePicture;
        }
    }

}
