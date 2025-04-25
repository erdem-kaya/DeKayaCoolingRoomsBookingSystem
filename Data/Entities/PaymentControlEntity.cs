using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class PaymentControlEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PrePayment { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal RemainingAmount { get; set; }

    [MaxLength(500)]
    public string? PaymentDescription { get; set; }
    public DateTime ConfirmedAt { get; set; }
    
    public int PaymentStatusId { get; set; }
    public PaymentStatusEntity PaymentStatus { get; set; } = null!;

    public int PaymentMethodId { get; set; }
    public PaymentMethodEntity PaymentMethod { get; set; } = null!;
    public ICollection<BookingEntity> Bookings { get; set; } = [];

}
