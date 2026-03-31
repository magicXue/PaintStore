using System;
using AutoMapper;
using PaintStore.Models;
using PaintStore.Models.DTOs;

namespace PaintStore.API.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        // 2 types, type 1 is source, type 2 is destination
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserResponseDto>();
    }
}
