using ContactManager.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllAsync();
        Task<ContactDto?> GetByIdAsync(int id);
        Task<ContactDto> AddAsync(ContactDto dto);
        Task<ContactDto> UpdateAsync(ContactDto dto);
        Task<bool> DeleteAsync(int id);
    }
}