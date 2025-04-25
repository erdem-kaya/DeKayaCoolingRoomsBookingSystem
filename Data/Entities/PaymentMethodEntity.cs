using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class PaymentMethodEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string MethodName { get; set; } = null!;

    public ICollection<PaymentControlEntity> PaymentControls { get; set; } = [];
}
