using Data.Entities;
using Domain.Models;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IUserProfileRepository : IBaseRepository<UserProfileEntity, UserProfile>
{
    Task<UserProfileEntity?> GetEntityAsync(Expression<Func<UserProfileEntity, bool>> predicate);
}

