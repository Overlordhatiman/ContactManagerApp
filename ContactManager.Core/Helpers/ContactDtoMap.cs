using ContactManager.Core.DTOs;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Helpers
{
    public sealed class ContactDtoMap : ClassMap<ContactDto>
    {
        public ContactDtoMap()
        {
            Map(m => m.Name);
            Map(m => m.DateOfBirth);
            Map(m => m.Married);
            Map(m => m.Phone);
            Map(m => m.Salary);
        }
    }
}
