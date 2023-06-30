using HotelSystem.Domain.Entities.Dtos;
using HotelSystem.Domain.Entities.Dtos.PaymentDtos;

namespace HotelSystem.Domain.Repositories;
public interface IPaymentRepository
{
    Task Pay(PayPaymentDto paymentDto);
}