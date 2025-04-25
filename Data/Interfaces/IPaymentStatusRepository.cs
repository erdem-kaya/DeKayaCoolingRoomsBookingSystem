﻿using Data.Entities;
using Domain.Models;

namespace Data.Interfaces;

public interface IPaymentStatusRepository : IBaseRepository<PaymentStatusEntity, PaymentStatus>
{
}