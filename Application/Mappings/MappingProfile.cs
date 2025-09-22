using AutoMapper;
using Backend.Application.DTOs.Auth;
using Backend.Application.DTOs.Player;
using Backend.Application.Models;

namespace Backend.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<RegisterRequestDto, Player>();
    }
}