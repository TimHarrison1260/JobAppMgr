using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// Represents an Event that happens for a Job application
    /// </summary>
    public class Event
    {
        /// <summary>
        /// A unique identifier for the Event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Date of the Event
        /// </summary>
        public DateTime  Date { get; set; }

        /// <summary>
        /// Describes the specific details of the event
        /// </summary>
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
        public string Attachment { get; set; }

        /// <summary>
        /// Defines the Type of Event
        /// </summary>
        public virtual EventType Type { get; set; }
        public int EventTypeId { get; set; }

        /// <summary>
        /// Reference to the JobApplication the Event belongs to.
        /// </summary>
        public virtual JobApplication JobApplication { get; set; }
        public int JobApplicationId { get; set; }
    }
}