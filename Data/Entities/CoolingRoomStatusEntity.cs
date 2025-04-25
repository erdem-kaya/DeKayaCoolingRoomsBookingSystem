using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CoolingRoomStatusEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string StatusName { get; set; } = null!;

    public ICollection<CoolingRoomEntity> CoolingRooms { get; set; } = [];
}
