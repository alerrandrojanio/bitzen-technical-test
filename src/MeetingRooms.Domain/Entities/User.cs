﻿using MeetingRooms.Domain.Entities.Base;

namespace MeetingRooms.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
     
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
}
