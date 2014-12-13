using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Model;
using Microsoft.Data.Edm.Validation;

namespace JobAppMgr.ViewModels
{
    public class JobEditViewModel
    {
        /// <summary>
        /// A Unique identifier for the job
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Title of the job
        /// </summary>
        [DisplayName("Job Application")]
        [Required(ErrorMessage = "The Job Title must be completed.")]
        [StringLength(50, MinimumLength = 1,ErrorMessage = "Too Many characters, Title cannot be longer than 50 characers")]
        public string Title { get; set; }

        /// <summary>
        /// The date the job aplycation was created
        /// </summary>
        [DisplayName("Created")]
        [Required(ErrorMessage = "The creation Date is required, please enter a valid date.")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Defines the current status of the job application
        /// DO NOT ALLOW EDIT OF THIS FIELD
        /// </summary>
        [DisplayName("Status")]
        public string Status { get; set; }

        /// <summary>
        /// Specifies the Closing Date for Applications
        /// </summary>
        /// <remarks>
        /// Closing date is Optional
        /// </remarks>
        [DisplayName("Closing Date")]
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Gives the Location of the Job
        /// </summary>
        [DisplayName("Location")]
        public string Location { get; set; }

        /// <summary>
        /// Defines the details of the job
        /// </summary>
        [DisplayName("Description")]
        [Required(ErrorMessage = "You must enter a description of the job application")]
        public string Description { get; set; }

        /// <summary>
        /// Defines the skills that are required for the job
        /// </summary>
        [DisplayName("Key Skills")]
        public string KeySkills { get; set; }

        /// <summary>
        /// Describes the skills identifed as beneficial or nice to have
        /// </summary>
        [DisplayName("Desired Skills")]
        public string NiceToHaveSkills { get; set; }

        /// <summary>
        /// Describes any Benefits that come with the Job
        /// </summary>
        [DisplayName("Benefits")]
        public string Benefits { get; set; }

        /// <summary>
        /// Describes any conditions that apply to the job
        /// </summary>
        [DisplayName("Conditions")]
        public string Conditions { get; set; }

        /// <summary>
        /// The company offering the Job
        /// </summary>
        [DisplayName("Company")]
        public int CompanyId { get; set; }
        public SelectList Companies { get; set; }

        [DisplayName("Agency")]
        public int AgencyId { get; set; }
        public SelectList Agencies { get; set; }

        public bool Deleted { get; set; }

    }
}