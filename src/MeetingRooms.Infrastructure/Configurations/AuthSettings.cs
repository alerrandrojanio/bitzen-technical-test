﻿namespace MeetingRooms.Infrastructure.Configurations;

public class AuthSettings
{
    public string SecretKey { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string PublicRoutes { get; set; } = string.Empty;
}
