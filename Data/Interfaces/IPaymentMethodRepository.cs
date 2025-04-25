using Data.Entities;
using Domain.Models;

namespace Data.Interfaces;

public interface IPaymentMethodRepository : IBaseRepository<PaymentMethodEntity, PaymentMethod>
{
}
