namespace HotelSystem.Domain.Entities;
public class Payment
{
    public bool IsPayed { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string CardNumber { get; set; }
    public double PaymentAmount { get; set; }

    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; }
}