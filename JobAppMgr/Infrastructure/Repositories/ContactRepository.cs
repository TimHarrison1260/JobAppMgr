using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Model;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public override IQueryable<Contact> Search(string queryString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of Contacts, excluding those on the supplied list.
        /// </summary>
        /// <param name="excludedContacts">List of Contacts to exclude</param>
        /// <returns>A list containing the remaining Contacts</returns>
        public IQueryable<Contact> GetAvailableContacts(IEnumerable<Contact> excludedContacts)
        {
            /*
             * Using the Except estension method caused the following exception:
             * "Unable to create a constant value of type 'Core.Model.Contact'. 
             * Only primitive types or enumeration types are supported in this context."
             * 
             * This occurs when in-memory objects are being combined database source objects
             * Evaluating the database sourced object (_db.contacts), to an in-memory object 
             * using the .AsEnumerable() method allows both objects to be successfully combined.
             * 
             * See "Marked as Answer" to this
             * http://stackoverflow.com/questions/18929483/unable-to-create-a-constant-value-of-type-only-primitive-types-or-enumeration-ty
             */
            var contacts = _db.Contacts
                .AsEnumerable()
                .Except(excludedContacts);

            return contacts.AsQueryable();
        }
    }
}
