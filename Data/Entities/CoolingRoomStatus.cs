using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CoolingRoomStatus
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string StatusName { get; set; } = null!;
}
