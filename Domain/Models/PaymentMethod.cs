﻿namespace Domain.Models;

public class PaymentMethod
{
    public int Id { get; set; }
    public string MethodName { get; set; } = null!;
}
