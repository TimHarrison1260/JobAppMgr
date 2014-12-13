using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Interfaces;
using Core.Model;
using JobAppMgr.Mappings;
using JobAppMgr.ViewModels;

namespace JobAppMgr.Controllers
{
    public class AgencyController : Controller
    {
        private readonly IAgencyRepository _repository;
        private readonly IContactRepository _contactRepository;

        public AgencyController(IAgencyRepository repository, IContactRepository contactRepository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "No valid repository supplied to AgencyController.");
            if (contactRepository == null)
                throw new ArgumentNullException("contactRepository", "No valid ContactRepository supplied to Agency Controller");
            _repository = repository;
            _contactRepository = contactRepository;
        }


        //
        // GET: /Agency/

        public ActionResult Index()
        {
            var agencies = _repository.GetAll();
            var count = agencies.Count();
            ViewBag.Message = string.Format(" {0} Compan{1} found", count,
                (count > 1) ? "ies" : "y");
            var viewModel = (new MapAgencyToAgencyListViewModel()).Map(agencies);
            return View(viewModel);
        }

        //
        // GET: /Agency/Details/5

        public ActionResult Details(int id)
        {
            var agency = _repository.Get(id);
            ViewBag.Message = agency.Name;
            var viewModel = (new MapAgencyToAgencyViewModel()).Map(agency);
            return View(viewModel);
        }

        //
        // GET: /Agency/Create

        public ActionResult Create()
        {
            var viewModel = new AgencyViewModel()
            {
                Id = 0,
                Name = String.Empty,
                Address = string.Empty
            };
            return View(viewModel);
        }

        //
        // POST: /Agency/Create

        [HttpPost]
        public ActionResult Create(AgencyViewModel agencyViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var agency = (new MapAgencyViewModelToAgency()).Map(agencyViewModel);
                    var result = _repository.Create(agency);
                    TempData.Add("Msg", string.Format("Agency '({0}) {1}' successfully created", result.Id, result.Name));
                    return RedirectToAction("Index");
                }
                return View(agencyViewModel);      //  Model not valid
            }
            catch
            {
                return View(agencyViewModel);
            }
        }

        //
        // GET: /Agency/Edit/5

        public ActionResult Edit(int id)
        {
            var agency = _repository.Get(id);
            ViewBag.Message = agency.Name;
            var viewModel = (new MapAgencyToAgencyViewModel()).Map(agency);
            return View(viewModel);
        }

        //
        // POST: /Agency/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, AgencyViewModel agencyViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var agency = (new MapAgencyViewModelToAgency()).Map(agencyViewModel);
                    var result = _repository.Update(agency);
                    TempData.Add("Msg", string.Format("Agency '{0} successfully updated", agency.Name));
                    return RedirectToAction("Index");
                }
                return View(agencyViewModel);      //  Model not valid
            }
            catch
            {
                return View(agencyViewModel);
            }
        }

        ////
        //// GET: /Agency/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Agency/Delete/5

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
        public ActionResult AddContact(int id)
        {
            var agency = _repository.Get(id);
            var contacts = _contactRepository.GetAvailableContacts(agency.Contacts);   //.ToEnum("Name");

            ViewBag.Message = agency.Name;

            var viewModel = new AddOrganisationContactsViewModel<AgencyViewModel>()
            {
                Organisation = (new MapAgencyToAgencyViewModel()).Map(agency),
                ContactId = 0,
                AvailableContacts = new SelectList(contacts, "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddContact(int id, AddOrganisationContactsViewModel<AgencyViewModel> viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //  TODO: refactor contact adding logic to remove from Controller method.
                    var addedContact = _contactRepository.Get(viewModel.ContactId);
                    var agency = _repository.Get(id);
                    agency.Contacts.Add(addedContact);
                    var result = _repository.Update(agency);
                    TempData.Add("Msg", string.Format("Contact '{0}' successfully added to Agency '{1}'", addedContact.Name, agency.Name));
                    return RedirectToAction("Index");
                }

                //  TODO: factor in retrieving the company information again as display only is lost by now.
                var reloadedAgency = _repository.Get(id);
                viewModel.Organisation = (new MapAgencyToAgencyViewModel()).Map(reloadedAgency);
                var contacts = _contactRepository.GetAvailableContacts(reloadedAgency.Contacts);   //.ToEnum("Name");
                viewModel.AvailableContacts = new SelectList(contacts, "Id", "Name");
                return View(viewModel);
            }
            catch (Exception)
            {
                return View();
            }
        }

    }
}
