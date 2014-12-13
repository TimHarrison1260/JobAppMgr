using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// Defines the information for an Organisation
    /// </summary>
    public abstract class Organisation
    {
        /// <summary>
        /// A unique identifier for the Contact
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Contact.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Address of the Contact
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Give the post code for the contact address
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// The contacts associated with this organisation
        /// </summary>
        ///<remarks>
        /// NB!!!!!!!!
        /// ----------
        /// For Lazy/Eager loading to work with navigation properties,
        /// coollections MUST be declared as ICollection{T} and not
        /// IEnumerable{T}.  IEnumberable{T} will cause "A specified
        /// Include path is not valid" 
        /// see http://stackoverflow.com/questions/10038037/how-do-i-resolve-a-specified-include-path-is-not-valid#answers-header.
        /// </remarks>
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}

