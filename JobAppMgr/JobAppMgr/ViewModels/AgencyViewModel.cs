using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobAppMgr.ViewModels
{
    public class AgencyViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Agency Name cannot be blank, please enter a Name.")]
        [StringLength(50, ErrorMessage = "Too long, the Agency Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [DisplayName("Address")]
        [StringLength(100, ErrorMessage = "Too long, the Address cannot be longer than 100 characters.", MinimumLength = 0)]
        public string Address { get; set; }

        [DisplayName("Post Code")]
        [RegularExpression(@"[a-zA-Z]{2}[0-9]{1,2}\s{0,1}[0-9]{1,2}[a-zA-Z]{2}", ErrorMessage = "Not a valid UK Post code.")]
        public string PostCode { get; set; }

        [DisplayName("Contacts:")]
        public IEnumerable<ContactViewModel> Contacts { get; set; }
    }
}