namespace HotelSystem.Domain.Entities.Dtos;
public class UpdateReservationDto
{
    public Guid Id { get; set; }
    public DateOnly RentStart { get; set; }
    public DateOnly RentEnd { get; set; }

    public Guid UserId { get; set; }

    public Guid RoomId { get; set; }
}