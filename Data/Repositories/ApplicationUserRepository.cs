using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class ApplicationUserRepository(DataContext context) : BaseRepository<ApplicationUserEntity, ApplicationUser>(context), IApplicationUserRepository
{
}
