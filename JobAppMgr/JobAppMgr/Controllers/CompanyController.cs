using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Interfaces;
using Core.Model;
using JobAppMgr.Mappings;
using JobAppMgr.ViewModels;

namespace JobAppMgr.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _repository;
        private readonly IContactRepository _contactRepository;

        public CompanyController(ICompanyRepository repository, IContactRepository contactRepository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "No valid repository supplied to CompanyController.");
            if (contactRepository == null)
                throw new ArgumentNullException("contactRepository", "No valid ContactRepository supplied to Company Controller");
            _repository = repository;
            _contactRepository = contactRepository;
        }

        //
        // GET: /Company/

        public ActionResult Index()
        {
            var companies = _repository.GetAll();
            var count = companies.Count();
            ViewBag.Message = string.Format(" {0} Compan{1} found", count,
                (count > 1) ? "ies" : "y");
            var viewModel = (new MapCompanyToCompanyListViewModel()).Map(companies);
            return View(viewModel);
        }

        //
        // GET: /Company/Details/5

        public ActionResult Details(int id)
        {
            var company = _repository.Get(id);
            ViewBag.Message = company.Name;
            var viewModel = (new MapCompanyToCompanyViewModel()).Map(company);
            return View(viewModel);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            var viewModel = new CompanyViewModel()
            {
                Id = 0,
                Name = String.Empty,
                Address = string.Empty
            };
            return View(viewModel);
        }

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult Create(CompanyViewModel companyViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var company = (new MapCompanyViewModelToCompany()).Map(companyViewModel);
                    var result = _repository.Create(company);
                    TempData.Add("Msg", string.Format("Company '({0}) {1}' successfully created", result.Id, result.Name));
                    return RedirectToAction("Index");
                }
                return View(companyViewModel);      //  Model not valid
            }
            catch
            {
                return View(companyViewModel);
            }
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit(int id)
        {
            var company = _repository.Get(id);
            ViewBag.Message = company.Name;
            var viewModel = (new MapCompanyToCompanyViewModel()).Map(company);
            return View(viewModel);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CompanyViewModel companyViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var company = (new MapCompanyViewModelToCompany()).Map(companyViewModel);
                    var result = _repository.Update(company);
                    TempData.Add("Msg", string.Format("Company '{0} successfully updated", company.Name));
                    return RedirectToAction("Index");
                }
                return View(companyViewModel);      //  Model not valid
            }
            catch
            {
                return View(companyViewModel);
            }
        }

        ////
        //// GET: /Company/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Company/Delete/5

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
            var company = _repository.Get(id);
            var contacts = _contactRepository.GetAvailableContacts(company.Contacts);   //.ToEnum("Name");
            
            ViewBag.Message = company.Name;

            var viewModel = new AddOrganisationContactsViewModel<CompanyViewModel>()
            {
                Organisation = (new MapCompanyToCompanyViewModel()).Map(company),
                ContactId = 0,
                AvailableContacts = new SelectList(contacts, "Id", "Name")
            };
                
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddContact(int id, AddOrganisationContactsViewModel<CompanyViewModel> viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //  TODO: refactor contact adding logic to remove from Controller method.
                    var addedContact = _contactRepository.Get(viewModel.ContactId);
                    var company = _repository.Get(id);
                    company.Contacts.Add(addedContact);
                    var result = _repository.Update(company);
                    TempData.Add("Msg", string.Format("Contact '{0}' successfully added to Company '{1}'", addedContact.Name, company.Name));
                    return RedirectToAction("Index");
                }

                //  TODO: factor in retrieving the company information again as display only is lost by now.
                var reloadedCompany = _repository.Get(id);
                viewModel.Organisation = (new MapCompanyToCompanyViewModel()).Map(reloadedCompany);
                var contacts = _contactRepository.GetAvailableContacts(reloadedCompany.Contacts);   //.ToEnum("Name");
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
