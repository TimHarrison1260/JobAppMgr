using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Interfaces;
using JobAppMgr.Mappings;
using JobAppMgr.ViewModels;

namespace JobAppMgr.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "No valid repository supplied to ContactController.");
            _repository = repository;
        }


        //
        // GET: /Contact/

        public ActionResult Index()
        {
            var contacts = _repository.GetAll();
            ViewBag.Message = string.Format("There are {0} contact{1}.", contacts.Count(), (contacts.Count() > 1 ? "s" : ""));
            var viewModel = (new MapContactToContactViewModel()).Map(contacts);
            return View(viewModel);
        }

        //
        // GET: /Contact/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            var viewModel = new ContactViewModel()
            {
                Id = 0,
                Name = string.Empty,
                Phone = string.Empty,
            };
            return View(viewModel);
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(ContactViewModel contactViewModel)             //FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var contact = (new MapContactViewModelToContact()).Map(contactViewModel);

                var result = _repository.Create(contact);

                TempData.Add("Msg", string.Format("Contact '({0}) {1}' successfully created.", result.Id, result.Name));

                return RedirectToAction("Index");
            }
            else
            {
                return View(contactViewModel);
            }
        }

        //
        // GET: /Contact/Edit/5

        public ActionResult Edit(int id)
        {
            var contact = _repository.Get(id);
            var viewModel = (new MapContactToContactViewModel()).Map(contact);
            ViewBag.Message = contact.Name;
            return View(viewModel);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                var contact = (new MapContactViewModelToContact()).Map(contactViewModel);
                var result = _repository.Update(contact);
                TempData.Add("Msg", string.Format("Contact '{0}' successfully updated", contact.Name));
                return RedirectToAction("Index");
            }
            else
            {
                return View(contactViewModel);
            }
        }

        //
        // GET: /Contact/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
