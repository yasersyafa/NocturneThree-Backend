using AutoMapper;
using Backend.Application.DTOs.Auth;
using Backend.Application.DTOs.Game;
using Backend.Application.DTOs.GameData;
using Backend.Application.DTOs.Otp;
using Backend.Application.DTOs.Player;
using Backend.Application.DTOs.Session;
using Backend.Application.Models;
using Newtonsoft.Json;

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

        // Game Data
        // Entity -> Single Response
        CreateMap<PlayerGameData, GameDataResponseDto>()
            .ForMember(dest => dest.Data,
                opt => opt.MapFrom(src => 
                    JsonConvert.DeserializeObject<object>(src.Data) ?? new {}));

        // Entity -> List Response
        CreateMap<PlayerGameData, PlayerGameListResponseDto>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Game != null ? src.Game.Title : string.Empty));
    }
}