using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Domain.Entities.Dtos.RoomDtos;
public class UpdateRoomDto
{
    public double StartPrice { get; set; }
    public int RoomsNumber { get; set; }
    public string Shower { get; set; }
    public string Toilet { get; set; }
    public string Bath { get; set; }

    public Guid RoomTypeId { get; set; }

    public Guid HotelId { get; set; }
}
