using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class BookingEntity
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

    public CustomerEntity Customer { get; set; } = null!;
    public int CustomerId { get; set; }

    public CoolingRoomEntity CoolingRoom { get; set; } = null!;
    public int CoolingRoomId { get; set; }

    public PaymentControlEntity PaymentControl { get; set; } = null!;
    public int PaymentControlId { get; set; }

    public ApplicationUserEntity ApplicationUser { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
}
