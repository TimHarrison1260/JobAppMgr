using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Interfaces;
using JobAppMgr.Mappings;

namespace JobAppMgr.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobApplicationRepository _repository;

        public HomeController(IJobApplicationRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "No valid repository supplied to HomeController.");
            _repository = repository;
        }


        public ActionResult Index()
        {
            //  Get all jobs
            //  TODO: Convert this to FindJobs() with a status of 'Active'. (When Query object pattern used)
            var jobs = _repository.GetAll();
            //  Set appropriate message at top of page
            ViewBag.Message = string.Format("You currently have {0} active job application{1}.", jobs.Count(),(jobs.Count() > 1 ? "s": ""));
            //  Map to the page's view model
            //  TODO: Convert some/all of the mappers to ActionFilters to remove concern from controllers; both ways if possible.
            var viewModel = (new MapJobApplicationsToJobListViewModel()).Map(jobs);

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
