using System.ComponentModel;

namespace HotelSystem.Domain.Entities.Dtos.RequestDtos.RoomRequests;
public class RoomFilterRequest : IRequest
{
    [DefaultValue(null)]
    public double MinPrice { get; set; }
    [DefaultValue(null)]
    public double MaxPrice { get; set; }
    [DefaultValue(null)]
    public int RoomsNumber { get; set; }
    [DefaultValue(null)]
    public string Shower { get; set; }
    [DefaultValue(null)]
    public string Toilet { get; set; }
    [DefaultValue(null)]
    public string Bath { get; set; }
    [DefaultValue(null)]
    public Guid HotelId { get; set; }
}
