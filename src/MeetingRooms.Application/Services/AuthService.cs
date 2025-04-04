using Mapster;
using MeetingRooms.Domain.DTOs.Auth;
using MeetingRooms.Domain.DTOs.Auth.Response;
using MeetingRooms.Domain.DTOs.User;
using MeetingRooms.Domain.Interfaces;
using MeetingRooms.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeetingRooms.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly AuthSettings _authSettings;

    public AuthService(IUserService userService, IOptions<AuthSettings> authSettings)
    {
        _userService = userService;
        _authSettings = authSettings.Value;
    }

    public async Task<LoginResponseDTO> Login(LoginDTO loginDTO)
    {
        ValidateUserDTO validateUserDTO = loginDTO.Adapt<ValidateUserDTO>();

        await _userService.ValidateUser(validateUserDTO);

        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, loginDTO.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime)
        };

        DateTime expiration = DateTime.UtcNow.AddDays(1);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_authSettings.SecretKey));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiration,
            Issuer = _authSettings.Issuer,
            Audience = _authSettings.Audience,
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new();

        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

        string jwtToken = tokenHandler.WriteToken(securityToken);

        LoginResponseDTO loginResponseDTO = ValueTuple.Create(jwtToken, expiration).Adapt<LoginResponseDTO>();

        return loginResponseDTO;
    }
}
