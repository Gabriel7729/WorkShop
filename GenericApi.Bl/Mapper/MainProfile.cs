using AutoMapper;
using GenericApi.Bl.Dto;
using GenericApi.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Bl.Mapper
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Document, DocumentDto>().ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(dto => dto.PhotoFileName, config => config.MapFrom(entity => entity.Photo.FileName));
            
            CreateMap<UserDto, User>();

            CreateMap<WorkShop, WorkShopDto>().ReverseMap();

            CreateMap<WorkShopDay, WorkShopDayDto>().ReverseMap();

            CreateMap<WorkShopMember, WorkShopMemberDto>().ReverseMap();

        }
    }
}
