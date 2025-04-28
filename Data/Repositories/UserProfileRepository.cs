using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class UserProfileRepository(DataContext context) : BaseRepository<UserProfileEntity, UserProfile>(context), IUserProfileRepository
{
}