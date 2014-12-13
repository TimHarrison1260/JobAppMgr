using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobAppMgr.ViewModels
{
    public class AddOrganisationContactsViewModel<T> where T : class 
    {
        /// <summary>
        /// Get details of Company the contact is to be added to
        /// </summary>
        public T Organisation { get; set; }

        /// <summary>
        /// Id of the contact selected to be added
        /// </summary>
        [DisplayName("Avaliable Contacts")]
        [Required(ErrorMessage = "You must select a Contact to add.")]
        public int ContactId { get; set; }

        /// <summary>
        /// List of contacts that are not already added to the Company
        /// </summary>
        public SelectList AvailableContacts { get; set; } 
    }
}