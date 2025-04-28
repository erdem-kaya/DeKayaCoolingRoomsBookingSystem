using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class UserProfileRepository(DataContext context) : BaseRepository<UserProfileEntity, UserProfile>(context), IUserProfileRepository
{
    public async Task<UserProfileEntity?> GetEntityAsync(Expression<Func<UserProfileEntity, bool>> predicate)
    {
        return await _context.UserProfiles
            .Include(x => x.ApplicationUser) 
            .FirstOrDefaultAsync(predicate);
    }
}