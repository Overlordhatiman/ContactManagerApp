using ContactManager.Data.Context;
using ContactManager.Data.Entities;
using ContactManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApp.Data.Repositories;

namespace ContactManager.Data.Repositories
{
    public class ContactRepository : Repository<Contact>, IContact
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }
    }
}