﻿namespace HotelSystem.Domain.Entities.DbEntities;
public class RoomType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Room> Rooms { get; set; } = new List<Room>();
}

