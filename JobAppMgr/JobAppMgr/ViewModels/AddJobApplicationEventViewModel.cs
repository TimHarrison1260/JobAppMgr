using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobAppMgr.ViewModels
{
    public class AddJobApplicationEventViewModel
    {
        public JobListViewModel JobApplication { get; set; }

        public IEnumerable<EventListviewModel> Events { get; set; }

        public AddEventViewModel NewEvent { get; set; }

        public SelectList PossibleEventTypes { get; set; }

    }
}