namespace HotelSystem.Domain.Entities.DbEntities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
    public string Role { get; set; }
    public bool IsDeleted { get; set; }
    public double MoneyBonuses { get; set; }

    public List<Payment> Payments { get; set; } = new List<Payment>();

    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}
