namespace HotelSystem.Domain.Entities.Dtos.RoomDtos;
public class CreateRoomDto
{
    public double StartPrice { get; set; }
    public int RoomsNumber { get; set; }
    public string Shower { get; set; }
    public string Toilet { get; set; }
    public string Bath { get; set; }

    public Guid RoomTypeId { get; set; }

    public Guid HotelId { get; set; }
}