using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// Defines a Contact for an organisation
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// A unique identifier for the contact
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the contact
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The phone number for the contact
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The Ogranisation this contact is working for
        /// </summary>
        public virtual Organisation Organisation { get; set; }
    }
}

