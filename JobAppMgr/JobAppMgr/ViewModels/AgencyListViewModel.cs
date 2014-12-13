using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JobAppMgr.ViewModels
{
    public class AgencyListViewModel
    {
        /// <summary>
        /// A unique identifier for the Contact
        /// </summary>   )]
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Contact.
        /// </summary>
        [DisplayName("Agency")]
        public string Name { get; set; }

        /// <summary>
        /// The Address of the Contact
        /// </summary>
        [DisplayName("Address")]
        public string Address { get; set; }

        /// <summary>
        /// Give the post code for the contact address
        /// </summary>
        [DisplayName("Post Code")]
        public string PostCode { get; set; }
    }
}