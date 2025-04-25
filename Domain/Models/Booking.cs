namespace Domain.Models;

public class Booking
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public string? BookingDescription { get; set; }
    public Customer Customer { get; set; } = null!;
    public CoolingRoom CoolingRoom { get; set; } = null!;
    public PaymentControl PaymentControl { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}
