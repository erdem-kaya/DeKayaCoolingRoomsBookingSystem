using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class CoolingRoomRepository(DataContext context) : BaseRepository<CoolingRoomEntity, CoolingRoom>(context), ICoolingRoomRepository
{
}
