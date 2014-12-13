using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// Represents the Type of Event for a Job Application
    /// </summary>
    public class EventType
    {
        /// <summary>
        /// A unique identifier for the EventType
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Describes the Event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates if this event type is the start of a workflow
        /// </summary>
        public bool Initial { get; set; }

        /// <summary>
        /// The next type of event to follow this one
        /// </summary>
        /// <remarks>
        /// Store the Id of the Next Type also to faciltate the 
        /// configuration of the self reference with EF fluent
        /// configuration.
        /// </remarks>
        public virtual EventType NextType { get; set; }
        public int? NextId { get; set; }

        /// <summary>
        /// Indicates the previous event type to this event
        /// </summary>
        public virtual EventType PreviousType { get; set; }
        public int? PrevId { get; set; }

        /// <summary>
        /// Defines the Status associated with this event once applied
        /// </summary>
        public virtual Status CorrespondingStatus { get; set; }
    }
}

