using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CoolingRoom
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string RoomName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CoolingRoomStatusId { get; set; }
    public CoolingRoomStatus CoolingRoomStatus { get; set; } = null!;
    public int CoolingRoomPriceControlId { get; set; }
    public CoolingRoomPriceControl CoolingRoomPriceControl { get; set; } = null!;
}
