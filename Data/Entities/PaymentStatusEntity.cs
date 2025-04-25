using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class PaymentStatusEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string StatusName { get; set; } = null!;

    public ICollection<PaymentControlEntity> PaymentControls { get; set; } = [];
}
