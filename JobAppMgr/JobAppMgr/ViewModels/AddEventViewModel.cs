using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Model;

namespace JobAppMgr.ViewModels
{
    public class AddEventViewModel
    {
        /// <summary>
        /// A unique identifier for the Event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Date of the Event
        /// </summary>
        [DisplayName("Event Date")]
        [Required(ErrorMessage = "You must supply a date of the event, default is todays date")]
        public string Date { get; set; }

        /// <summary>
        /// Describes the specific details of the event
        /// </summary>
        [DisplayName("Details")]
        [Required(ErrorMessage = "YOu must supply some details of the event")]
        public string Details { get; set; }

        /// <summary>
        /// Shows links to the documents associated with the event
        /// </summary>
        /// <remarks>
        /// To add multiple document to an Application, add multiple
        /// events.  This reduces the relationships for the events,
        /// thgey re simply a collection of records for each Job
        /// Application.
        /// </remarks>
        [DisplayName("Attachment (optional)")]
        public string Attachment { get; set; }

        /// <summary>
        /// Defines the Type of Event
        /// </summary>
        [DisplayName("Type of Event")]
        [Required(ErrorMessage = "You must select a Type of Event from the list")]
        public int EventTypeId { get; set; }

        public int JobApplicationId { get; set; }



    }
}