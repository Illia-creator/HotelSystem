﻿using System.ComponentModel;

namespace HotelSystem.Domain.Entities.Dtos.RequestDtos.HotelRequests;
public class HotelFilterRequest : IRequest
{
    [DefaultValue(null)]
     public string Name { get; set; }
    [DefaultValue(null)]
    public string City { get; set; }
}

