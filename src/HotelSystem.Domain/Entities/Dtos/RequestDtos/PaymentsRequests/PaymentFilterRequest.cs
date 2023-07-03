namespace HotelSystem.Domain.Entities.Dtos.RequestDtos.PaymentsRequests;
public class PaymentFilterRequest : IRequest
{
    public bool IsPayed { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string CardNumber { get; set; }
    public double PaymentAmount { get; set; }

    public Guid ReservationId { get; set; }
}
