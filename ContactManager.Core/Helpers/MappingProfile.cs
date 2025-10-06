using ContactManager.Core.DTOs;
using ContactManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Helpers
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Contact, ContactDto>().ReverseMap();
        }
    }
}