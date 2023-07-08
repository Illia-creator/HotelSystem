namespace HotelSystem.Domain.Entities.DbEntities;

public class Reservation
{
    public Guid Id { get; set; }
    public DateOnly RentStart { get; set; }
    public DateOnly RentEnd { get; set; }

    public Payment Payment { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid RoomId { get; set; }
    public Room Room { get; set; }
}
