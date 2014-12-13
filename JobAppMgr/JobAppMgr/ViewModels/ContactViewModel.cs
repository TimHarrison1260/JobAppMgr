using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JobAppMgr.ViewModels
{
    public class ContactViewModel
    {
        /// <summary>
        /// A unique identifier for the contact
        /// </summary>        
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The Name of the contact
        /// </summary>
        [Required(ErrorMessage = "Name is a required field, please enter the name of your contact.")]        
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The Name must be between 1 and 50 characters long.")]
        public string Name { get; set; }

        //  TODO: find a better REGEX to validate the phone number entered, currently only UK
        /// <summary>
        /// The phone number for the contact
        /// </summary>
        [StringLength(15,ErrorMessage = "Too long for a phone number, maximum of 15 digits allowed.")]
        [RegularExpression("^(?:(?:(?:0(?:0\\s?|11\\s)|\\+)44\\s?(?:\\(?0\\)?\\s?)?)|(?:\\(?0))(?:(?:\\d{5}\\)?\\s?\\d{4,5})|(?:\\d{4}\\)?\\s?(?:\\d{5}|\\d{3}\\s?\\d{3}))|(?:\\d{3}\\)?\\s?\\d{3}\\s?\\d{3,4})|(?:\\d{2}\\)?\\s?\\d{4}\\s?\\d{4}))(?:\\s?\\#\\d{3,4})?$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
    }
}