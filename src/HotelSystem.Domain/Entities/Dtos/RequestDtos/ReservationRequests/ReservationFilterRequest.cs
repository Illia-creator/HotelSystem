namespace HotelSystem.Domain.Entities.Dtos.RequestDtos.ReservationRequests;
public class ReservationFilterRequest : IRequest
{
    public DateOnly RentStart { get; set; }
    public DateOnly RentEnd { get; set; }

    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }
}