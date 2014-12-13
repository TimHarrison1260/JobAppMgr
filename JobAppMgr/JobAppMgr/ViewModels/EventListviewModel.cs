using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JobAppMgr.ViewModels
{
    public class EventListviewModel
    {
        /// <summary>
        /// A unique identifier for the Event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Date of the Event
        /// </summary>
        [DisplayName("Date")]
        public string Date { get; set; }

        /// <summary>
        /// Describes the specific details of the event
        /// </summary>
        [DisplayName("Details")]
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
        [DisplayName("Attachment")]
        public string Attachment { get; set; }

        /// <summary>
        /// Defines the Type of Event
        /// </summary>
        [DisplayName("Type")]
        public string TypeName { get; set; }
    }
}