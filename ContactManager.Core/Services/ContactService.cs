using AutoMapper;
using ContactManager.Core.DTOs;
using ContactManager.Core.Interfaces;
using ContactManager.Data.Entities;
using ContactManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Services
{
    public class ContactService : IContactService
    {
        private readonly IContact _contactRepo;
        private readonly IMapper _mapper;

        public ContactService(IContact contactRepo, IMapper mapper)
        {
            _contactRepo = contactRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactDto>> GetAllAsync()
        {
            var contacts = await _contactRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);
        }

        public async Task<ContactDto?> GetByIdAsync(int id)
        {
            var contact = await _contactRepo.GetByIdAsync(id);
            return _mapper.Map<ContactDto?>(contact);
        }

        public async Task<ContactDto> AddAsync(ContactDto dto)
        {
            var entity = _mapper.Map<Contact>(dto);
            var added = await _contactRepo.AddAsync(entity);
            return _mapper.Map<ContactDto>(added);
        }

        public async Task<ContactDto> UpdateAsync(ContactDto dto)
        {
            var entity = _mapper.Map<Contact>(dto);
            await _contactRepo.UpdateAsync(entity);

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _contactRepo.DeleteAsync(id);
            return true;
        }
    }
}
