using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// Represents the Status of the Job Application
    /// </summary>
    public class Status
    {
        /// <summary>
        /// A unique identifier for the Status
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Describes the Status
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A short description of the Status
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents all Job Applications that are at this Status
        /// </summary>
        public virtual IEnumerable<JobApplication> JobApplication { get; set; }

        /// <summary>
        /// Represents the Event Type that this is the Status for.
        /// </summary>
        public virtual IEnumerable<EventType> EventType { get; set; }
    }
}

