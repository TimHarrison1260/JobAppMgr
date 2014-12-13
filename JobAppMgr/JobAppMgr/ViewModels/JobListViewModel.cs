using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Core.Model;

namespace JobAppMgr.ViewModels
{
    public class JobListViewModel
    {

        /// <summary>
        /// A nique identifier for the job
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Title of the job
        /// </summary>
        [DisplayName("Job")]
        public string Title { get; set; }

        /// <summary>
        /// Defines the current status of the job application
        /// </summary>
        [DisplayName("Status")]
        public string StatusName { get; set; }

        /// <summary>
        /// Specifies the Closing Date for Applications
        /// </summary>
        [DisplayName("Closing Date")]
        public string ClosingDate { get; set; }

        /// <summary>
        /// Gives the Location of the Job
        /// </summary>
        [DisplayName("Location")]
        public string Location { get; set; }

        /// <summary>
        /// The company offering the Job
        /// </summary>
        [DisplayName("Company")]
        public string CompanyName { get; set; }

        [DisplayName("Agency")]
        public string AgencyName { get; set; }

    }
}