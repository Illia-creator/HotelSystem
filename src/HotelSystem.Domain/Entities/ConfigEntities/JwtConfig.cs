namespace HotelSystem.Domain.Entities.ConfigEntities;
public class JwtConfig
{
    public string Secret { get; set; }
    public TimeSpan ExpiryTimeFrame { get; set; }
}
