namespace HotelSystem.Domain.Entities.DbEntities;
public class Room
{
    public Guid Id { get; set; }
    public double Price { get; set; }
    public int RoomsNumber { get; set; }
    public string Shower { get; set; }
    public string Toilet { get; set; }
    public string Bath { get; set; }

    public Guid RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }

    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }

    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}
