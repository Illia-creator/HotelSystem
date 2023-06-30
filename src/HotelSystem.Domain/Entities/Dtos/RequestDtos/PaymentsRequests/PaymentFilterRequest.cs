namespace HotelSystem.Domain.Entities.Dtos.RequestDtos.PaymentsRequests;
public class PaymentFilterRequest
{
    public bool IsPayed { get; set; }
    public DateOnly PaymentDate { get; set; }
    public int CardNumber { get; set; }
    public double PaymentAmount { get; set; }

    public Guid ReservationId { get; set; }
}
