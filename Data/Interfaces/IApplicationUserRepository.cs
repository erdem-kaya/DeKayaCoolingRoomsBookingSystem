using Data.Entities;
using Domain.Models;

namespace Data.Interfaces;

public interface IApplicationUserRepository : IBaseRepository<ApplicationUserEntity, UserProfile>
{
}
