using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        //  TODO: Refactor this to use the Search method with the appropriate Query Object
        IQueryable<Contact> GetAvailableContacts(IEnumerable<Contact> excludedContacts);

    }
}
