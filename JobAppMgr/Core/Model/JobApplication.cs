using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// Defines a Job Application
    /// </summary>
    public class JobApplication
    {
        /// <summary>
        /// A nique identifier for the job
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Title of the job
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The date the job aplycation was created
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Defines the current status of the job application
        /// </summary>
        public virtual Status Status { get; set; }

        /// <summary>
        /// Specifies the Closing Date for Applications
        /// </summary>
        /// <remarks>
        /// Closing date is Optional
        /// </remarks>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Gives the Location of the Job
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Defines the details of the job
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Defines the skills that are required for the job
        /// </summary>
        public string KeySkills { get; set; }

        /// <summary>
        /// Describes the skills identifed as beneficial or nice to have
        /// </summary>
        public string NiceToHaveSkills { get; set; }

        /// <summary>
        /// Describes any Benefits that come with the Job
        /// </summary>
        public string Benefits { get; set; }

        /// <summary>
        /// Describes any conditions that apply to the job
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// Determines if the JobApplication has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The company offering the Job
        /// </summary>
        public virtual Company Company { get; set; }
        public int? CompanyId { get; set; }

        public virtual Agency Agency { get; set; }
        public int? AgencyId { get; set; }

        public virtual ICollection<Event> Events { get; set; }


        public bool IsActive()
        {
            return !this.Deleted;
        }

        public bool IsDeleted()
        {
            return this.Deleted;
        }
    }

}