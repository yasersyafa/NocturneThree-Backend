using AutoMapper;
using Backend.Application.DTOs.Auth;
using Backend.Application.DTOs.Game;
using Backend.Application.DTOs.Otp;
using Backend.Application.DTOs.Player;
using Backend.Application.DTOs.Session;
using Backend.Application.Models;

namespace Backend.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Player
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<RegisterRequestDto, Player>();

        // Session
        CreateMap<Session, SessionDto>().ReverseMap();
        CreateMap<SessionRequestDto, Session>();

        // OTP Code
        CreateMap<OtpCode, OtpCodeDto>().ReverseMap();
        CreateMap<OtpRequestDto, OtpCode>();

        // Game
        CreateMap<Game, GameResponseDto>().ReverseMap();
        CreateMap<CreateGameDto, Game>();
        CreateMap<UpdateGameDto, Game>();
    }
}