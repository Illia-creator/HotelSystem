namespace HotelSystem.Domain.Entities.Dtos.ReservationDtos;
public class CreateReservationDto
{
    public DateOnly RentStart { get; set; }
    public DateOnly RentEnd { get; set; }
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
}
