using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    /// <summary>
    /// Defines a Job Agency as an Organisation
    /// </summary>
    public class Agency : Organisation
    {
        /// <summary>
        /// The collection of Jobapplications dealt with by the Agency
        /// </summary>
        public virtual IEnumerable<JobApplication> JobApplications { get; set; }
    }
}

