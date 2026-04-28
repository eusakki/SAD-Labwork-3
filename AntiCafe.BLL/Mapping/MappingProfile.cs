using AutoMapper;
using AntiCafe.DAL.Entities;
using AntiCafe.BLL.DTOs;

namespace AntiCafe.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Activity, ActivityDto>().ReverseMap();
        }
    }
}
