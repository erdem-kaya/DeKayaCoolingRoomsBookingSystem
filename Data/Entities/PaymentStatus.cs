using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class PaymentStatus
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string StatusName { get; set; } = null!;
}
