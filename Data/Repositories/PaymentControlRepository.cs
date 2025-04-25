using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class PaymentControlRepository(DataContext context) : BaseRepository<PaymentControlEntity, PaymentControl>(context), IPaymentControlRepository
{
}
