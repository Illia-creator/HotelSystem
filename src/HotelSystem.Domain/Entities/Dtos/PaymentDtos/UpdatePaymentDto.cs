namespace HotelSystem.Domain.Entities.Dtos.PaymentDtos;

public class UpdatePaymentDto
{
    public Guid ReservationId { get; set; }

    public bool IsPayed { get; set; }
    public double PaymentAmount { get; set; }
}