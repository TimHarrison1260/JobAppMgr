using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Interfaces;
using Core.Model;
using JobAppMgr.Mappings;
using JobAppMgr.ViewModels;

namespace JobAppMgr.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobApplicationRepository _repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IEventTypeRepository _eventTypeRepository;

        private const string DocsPath = "~/Documents";

        public JobController(IJobApplicationRepository repository, 
            ICompanyRepository companyRepository, 
            IAgencyRepository agencyRepository,
            IStatusRepository statusRepository,
            IEventTypeRepository eventTypeRepository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "No valid JobApplicationRepository supplied to Job Controller");
            if (companyRepository == null)
                throw new ArgumentNullException("companyRepository", "No valid CompanyRepository supplied to JobController");
            if (agencyRepository == null)
                throw new ArgumentNullException("agencyRepository", "No valid AgencyRepository supplied to JobController");
            if (statusRepository == null)
                throw new ArgumentNullException("statusRepository", "No valid StatusRepository supplied to JobController");
            if (eventTypeRepository == null)
                throw new ArgumentNullException("eventTypeRepository", "No valid EventTypeRepository supplied to JoobController");
            _repository = repository;
            _companyRepository = companyRepository;
            _agencyRepository = agencyRepository;
            _statusRepository = statusRepository;
            _eventTypeRepository = eventTypeRepository;
        }

        //
        // GET: /Job/

        public ActionResult Index()
        {
            var jobs = _repository.GetAll();
            var count = jobs.Count();
            ViewBag.Message = string.Format(" {0} Application{1} found", count, (count > 1) ? "s" : "");
            var viewModel = (new Mappings.MapJobApplicationsToJobListViewModel()).Map(jobs);
            return View(viewModel);
        }

        //
        // GET: /Job/Details/5

        public ActionResult Details(int id)
        {
            var job = _repository.Get(id);

            var mapper = new MapJobApplicationToJobDetailsViewModel();
            var viewModel = mapper.Map(job);

            ViewBag.Message = string.Format("{0}, {1}", job.Title, job.Company.Name);
            return View(viewModel);
        }

        //
        // GET: /Job/Create

        public ActionResult Create()
        {
            var agencies = _agencyRepository.GetAll();
            var companies = _companyRepository.GetAll();

            var viewModel = new JobEditViewModel()
            {
                CreateDate = DateTime.Now,
                Companies = new SelectList(companies, "Id", "Name"),
                Agencies = new SelectList(agencies, "Id", "Name"),
            };

            return View(viewModel);
        }

        //
        // POST: /Job/Create

        [HttpPost]
        public ActionResult Create(JobEditViewModel jobEditViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mapper = new MapJobViewModelToJobApplication();
                    var job = mapper.Map(jobEditViewModel);

                    //  Retrieve the company in case it's been updated.
                    var company = _companyRepository.Get(jobEditViewModel.CompanyId);
                    job.Company = company;

                    var agency = _agencyRepository.Get(jobEditViewModel.AgencyId);
                    job.Agency = agency;

                    job.Status = _statusRepository.Get(1);

                    var result = _repository.Create(job);

                    TempData.Add("Msg", string.Format("Job Application '{0}' successfully created", job.Title));
                    return RedirectToAction("Index");
                }
                return View(jobEditViewModel);      //  Model not valid
            }
            catch
            {
                return View(jobEditViewModel);
            }
        }

        //
        // GET: /Job/Edit/5

        public ActionResult Edit(int id)
        {
            var job = _repository.Get(id);
            var companies = _companyRepository.GetAll();
            var agencies = _agencyRepository.GetAll();

            var mapper = new MapJobApplicationToJobEditViewModel();
            var viewModel = mapper.Map(job);
            
            viewModel.Companies = new SelectList(companies, "Id", "Name");
            viewModel.Agencies = new SelectList(agencies, "Id", "Name");

            ViewBag.Message = string.Format("{0}, {1}", job.Title, job.Company.Name);
            return View(viewModel);
        }

        //
        // POST: /Job/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, JobEditViewModel jobEditViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mapper = new MapJobViewModelToJobApplication();
                    var job = mapper.Map(jobEditViewModel);

                    //  Retrieve the company in case it's been updated.
                    var company = _companyRepository.Get(jobEditViewModel.CompanyId);
                    job.Company = company;

                    var agency = _agencyRepository.Get(jobEditViewModel.AgencyId);
                    job.Agency = agency;

                    var result = _repository.Update(job);

                    TempData.Add("Msg", string.Format("Job Application '{0}' successfully updated", job.Title));
                    return RedirectToAction("Index");
                }
                return View(jobEditViewModel);      //  Model not valid
            }
            catch
            {
                return View(jobEditViewModel);
            }
        }

        ////
        //// GET: /Job/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Job/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult AddEvent(int id)
        {
            var job = _repository.Get(id);

            var viewModel = new AddJobApplicationEventViewModel();
            viewModel.JobApplication = (new MapJobApplicationsToJobListViewModel()).Map(job);
            viewModel.Events = (new MapEventToEventListviewModel()).Map(_repository.GetJobEvents(id));
            viewModel.PossibleEventTypes = PopulateEventTypes(job.Events);

            viewModel.NewEvent = new AddEventViewModel()
            {
                Id = 0,
                Date = DateTime.Now.ToString(),
                Details = string.Empty,
                Attachment = string.Empty
            };

            ViewBag.Message = string.Format("{0}, {1}", job.Title, job.Company.Name);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEvent(int id, AddJobApplicationEventViewModel viewModel,  HttpPostedFileBase myfile)
        {
            try
            {
                JobApplication job;
                if (ModelState.IsValid)
                {
                    job = _repository.Get(id);

                    var savedFiles = SaveAnyFiles(Request.Files, job.Id);

                    var addedEvent = (new MapAddEventViewModelToEvent()).Map(viewModel.NewEvent);
                    addedEvent.JobApplicationId = id;
                    addedEvent.Attachment = savedFiles.FirstOrDefault();

                    job.Events.Add(addedEvent);

                    _repository.Update(job);

                    TempData.Add("Msg", string.Format("Event successfully added to job application "));
                    return RedirectToAction("AddEvent");

                }
                //  Model invalid, return the view
                job = _repository.Get(id);
                //  Repopulate the static content.
                viewModel.JobApplication = (new MapJobApplicationsToJobListViewModel()).Map(job);
                viewModel.Events = (new MapEventToEventListviewModel()).Map(job.Events);
                viewModel.PossibleEventTypes = PopulateEventTypes(job.Events);

                return View(viewModel);
            }
            catch (Exception)
            {
                return View();
            }
        }


        private SelectList PopulateEventTypes(IEnumerable<Event> jobEvents)
        {
            IQueryable<EventType> possibleEventTypes = null;

            if (jobEvents == null)
                possibleEventTypes = _eventTypeRepository.GetInitialTypes();
            else
            {
                var events = jobEvents.ToList();        //  Avoid multiple iterations of IEnumerable<>
                if (events.Count == 0)
                    possibleEventTypes = _eventTypeRepository.GetInitialTypes();
                else
                {
                    var lastEvent = events.LastOrDefault();
                    var lastEventType = _eventTypeRepository.Get(lastEvent.EventTypeId);
                    possibleEventTypes = _eventTypeRepository.GetNextTypes(Convert.ToInt32(lastEventType.NextId));  //  NextId is nullable.
                }
            }

            var eventTypeSelectList = new SelectList(possibleEventTypes, "Id", "Description");

            return eventTypeSelectList;
        }

        /// <summary>
        /// Save any files uploaded in the Httprequest
        /// </summary>
        /// <param name="files">collection of files in request</param>
        /// <param name="jobId">Id of the job the event is being attached to</param>
        /// <returns>The relative path (to be stored in the DB) for each file saved</returns>
        private IList<string> SaveAnyFiles(HttpFileCollectionBase files, int jobId)
        {
            var savedFileNames = new List<string>();

            if (files.Count == 0)
                return savedFileNames;


            for (int i = 0; i < files.Count; i++)
            {
                if (Request.Files[i] != null && Request.Files[i].ContentLength > 0)
                {
                    //  It actually contains something, so save it
                    var filename = Path.GetFileName(Request.Files[i].FileName);
                    var saveFileName = Path.Combine(GetDocumentsFolder(jobId), filename);
                    Request.Files[i].SaveAs(saveFileName);

                    var savedRelativeFileName = GetRelativeDocumentPath(jobId, filename);
                    savedFileNames.Add(savedRelativeFileName);
                }
            }
 
            return savedFileNames;
        }

        private string GetDocumentsFolder(int jobId)
        {
            var docsPath = Path.Combine(Server.MapPath(DocsPath), string.Format("J{0}", jobId));

            if (Directory.Exists(docsPath)) return docsPath;

            try
            {
                Directory.CreateDirectory(docsPath);
                return docsPath;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetRelativeDocumentPath(int jobId, string fileName)
        {
            var relativePath = string.Format("{0}/J{1}/{2}", DocsPath.TrimStart('~'), jobId, fileName);
            return relativePath; 
        }
    }
}