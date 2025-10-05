using ContactManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApp.Data.Repositories.Interfaces;

namespace ContactManager.Data.Interfaces
{
    public interface IContact : IRepository<Contact>
    {
    }
}
