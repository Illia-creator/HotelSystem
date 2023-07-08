namespace HotelSystem.Domain.Entities.DbEntities;
public class Hotel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Site { get; set; }
    public string PhoneNumber { get; set; }

    public List<Room> Rooms { get; set; } = new List<Room>();
}
