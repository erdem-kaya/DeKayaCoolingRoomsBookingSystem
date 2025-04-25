using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class CoolingRoomPriceControlRepository(DataContext context) : BaseRepository<CoolingRoomPriceControlEntity, CoolingRoomPriceControl>(context), ICoolingRoomPriceControlRepository
{
}
