using Data.Entities;
using Domain.Models;

namespace Data.Interfaces;

public interface IPaymentControlRepository : IBaseRepository<PaymentControlEntity, PaymentControl>
{
}
