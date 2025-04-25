namespace Domain.Models;

public class CoolingRoom
{
    public int Id { get; set; }
    public string RoomName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public CoolingRoomStatus CoolingRoomStatus { get; set; } = null!;
    public CoolingRoomPriceControl CoolingRoomPriceControl { get; set; } = null!;
}
