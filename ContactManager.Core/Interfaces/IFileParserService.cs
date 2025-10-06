using ContactManager.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Interfaces
{
    public interface IFileParserService
    {
        Task<List<ContactDto>> ParseCsvAsync(Stream csvStream);
    }
}