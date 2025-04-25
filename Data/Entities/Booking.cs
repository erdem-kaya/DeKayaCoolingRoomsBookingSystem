using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class Booking
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Range(0, 100)]
    public decimal Discount { get; set; }
    public string? BookingDescription { get; set; }

    public Customer Customer { get; set; } = null!;
    public int CustomerId { get; set; }

    public CoolingRoom CoolingRoom { get; set; } = null!;
    public int CoolingRoomId { get; set; }

    public PaymentControl PaymentControl { get; set; } = null!;
    public int PaymentControlId { get; set; }

    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
}
