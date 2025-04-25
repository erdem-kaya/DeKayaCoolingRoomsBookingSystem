using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class CoolingRoomStatusRepository(DataContext context) : BaseRepository<CoolingRoomStatusEntity, CoolingRoomStatus>(context), ICoolingRoomStatusRepository
{
}
