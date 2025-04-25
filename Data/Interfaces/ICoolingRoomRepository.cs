using Data.Entities;
using Domain.Models;

namespace Data.Interfaces;

public interface ICoolingRoomRepository : IBaseRepository<CoolingRoomEntity, CoolingRoom>
{
}
