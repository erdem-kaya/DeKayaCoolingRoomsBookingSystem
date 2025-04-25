using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class PaymentMethodRepository(DataContext context) : BaseRepository<PaymentMethodEntity, PaymentMethod>(context), IPaymentMethodRepository
{
}
