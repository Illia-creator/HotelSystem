namespace HotelSystem.Domain.Entities.Dtos.HotelDtos;
public class UpdateHotelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Site { get; set; }
    public string PhoneNumber { get; set; }
}