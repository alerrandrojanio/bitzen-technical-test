﻿namespace MeetingRooms.Domain.DTOs.Auth.Response;

public class LoginResponseDTO
{
    public string? Token { get; set; }

    public DateTime? Expiration { get; set; }
}
