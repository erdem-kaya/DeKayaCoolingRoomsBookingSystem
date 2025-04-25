namespace Domain.Models;

public class PaymentControl
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public decimal PrePayment { get; set; }
    public decimal RemainingAmount { get; set; }
    public string? PaymentDescription { get; set; }
    public DateTime ConfirmedAt { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; } = null!;
}
