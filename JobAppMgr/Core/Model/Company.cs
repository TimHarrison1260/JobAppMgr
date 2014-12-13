using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.ComponentModel;

namespace Core.Model
{
    /// <summary>
    /// Defines the Company as an Organisaton
    /// </summary>
    public class Company : Organisation
    {
        /// <summary>
        /// The collection of JobApplications the Company offers
        /// </summary>
        ///<remarks>
        /// NB!!!!!!!!
        /// ----------
        /// For Lazy/Eager loading to work with navigation properties,
        /// coollections MUST be declared as ICollection{T} and not
        /// IEnumerable{T}.  IEnumberable{T} will cause "A specified
        /// Include path is not valid" 
        /// see http://stackoverflow.com/questions/10038037/how-do-i-resolve-a-specified-include-path-is-not-valid#answers-header.
        /// </remarks>
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}

