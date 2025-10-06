using AutoMapper;
using ContactManager.Core.DTOs;
using ContactManagerApp.Models;

namespace ContactManagerApp.MappingProfiles
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<ContactDto, ContactViewModel>().ReverseMap();
        }
    }
}