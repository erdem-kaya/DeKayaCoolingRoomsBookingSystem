using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CoolingRoomEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string RoomName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CoolingRoomStatusId { get; set; }
    public CoolingRoomStatusEntity CoolingRoomStatus { get; set; } = null!;
    public int CoolingRoomPriceControlId { get; set; }
    public CoolingRoomPriceControlEntity CoolingRoomPriceControl { get; set; } = null!;
    public ICollection<BookingEntity> Bookings { get; set; } = [];
}
