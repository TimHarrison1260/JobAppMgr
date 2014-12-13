using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Infrastructure.Data;

namespace Infrastructure.Migrations
{
    public static class DataInitialiser
    {
        public static void Seed(JobApplicationContext context)
        {
            /* To allow the seed method to be debugged, uncomment the next two lines.
             * 
             * See 
             * http://stackoverflow.com/questions/16718510/debugging-package-manager-console-update-database-seed-method
             * and/or
             * http://stackoverflow.com/questions/17169020/debug-code-first-entity-framework-migration-codes
             */

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            //  Status'
            var statusOpen = new Status()
            {
                Id = 1,
                Name = "Open",
                Description = "Application not submitted.",
                EventType = new List<EventType>()
            };
            var statusActive = new Status()
            {
                Id = 2,
                Name = "Active",
                Description = "Application submitted",
                EventType = new List<EventType>()
            };
            var statusDormant = new Status()
            {
                Id = 3,
                Name = "Dormant",
                Description = "No reply received",
                EventType = new List<EventType>()
            };
            var statusExpired = new Status()
            {
                Id = 4,
                Name = "Expired",
                Description = "Closing date passed, no reply received",
                EventType = new List<EventType>()
            };
            var statusClosed = new Status()
            {
                Id = 5,
                Name = "Closed",
                Description = "Application closed",
                EventType = new List<EventType>()
            };

            //  Add the Status to the context
            context.Statuses.AddOrUpdate(statusOpen);
            context.Statuses.AddOrUpdate(statusActive);
            context.Statuses.AddOrUpdate(statusDormant);
            context.Statuses.AddOrUpdate(statusExpired);
            context.Statuses.AddOrUpdate(statusClosed);

            //  Define the Event Types
            var eventTypeClosed = new EventType()
            {
                Id = 160,
                Description = "Application closed",
                Initial = false,
                NextId = null,
                PrevId = null,
                CorrespondingStatus = statusClosed
            };
            var eventTypeRejection = new EventType()
            {
                Id = 150,
                Description = "Rejection Received",
                Initial = false,
                NextId = eventTypeClosed.Id,
                PrevId = null,
                CorrespondingStatus = statusClosed
            };
            var eventTypeOfferRefused = new EventType()
            {
                Id = 140,
                Description = "Offer Refused",
                Initial = false,
                NextId = eventTypeClosed.Id,
                PrevId = null,
                CorrespondingStatus = statusClosed
            };
            var eventTypeOfferAccepted = new EventType()
            {
                Id = 130,
                Description = "Offer Accepted",
                Initial = false,
                NextId = eventTypeClosed.Id,
                PrevId = null,
                CorrespondingStatus = statusClosed
            };
            var eventTypeUnconditionalReceived = new EventType()
            {
                Id = 120,
                Description = "Unconditional Offer Received",
                Initial = false,
                NextId = eventTypeOfferAccepted.Id,
                PrevId = null,
                CorrespondingStatus = statusActive
            };
            var eventTypeReferencesRequested = new EventType()
            {
                Id = 110,
                Description = "References Requested",
                Initial = false,
                NextId = eventTypeUnconditionalReceived.Id,
                PrevId = null,
                CorrespondingStatus = statusActive
            };
            var eventTypeConditionalOffer = new EventType()
            {
                Id = 100,
                Description = "Conditional Offer Received",
                Initial = false,
                NextId = eventTypeReferencesRequested.Id,
                PrevId = null,
                CorrespondingStatus = statusActive
            };
            var eventTypeInterviewArranged = new EventType()
            {
                Id = 90,
                Description = "Interview Arranged",
                Initial = false,
                NextId = 90,                //  Can loop this event as many times as needed
                PrevId = null,
                CorrespondingStatus = statusActive
            };
            var eventTypeShortListed = new EventType()
            {
                Id = 80,
                Description = "Shortlisted",
                Initial = false,
                NextId = 90,                //  Can loop this event as many times as needed
                PrevId = null,
                CorrespondingStatus = statusActive
            };
            var eventtypeSendApplication = new EventType()
            {
                Id = 70,
                Description = "Sent Application",
                Initial = false,
                NextId = eventTypeShortListed.Id,
                PrevId = null,
                CorrespondingStatus = statusActive
            };
            var eventTypeAddedLetter = new EventType()
            {
                Id = 60,
                Description = "Added Covering Letter",
                Initial = false,
                NextId = eventtypeSendApplication.Id,
                PrevId = null,
                CorrespondingStatus = statusActive
            };
            var eventTypeAddedCv = new EventType()
            {
                Id = 50,
                Description = "Added CV",
                Initial = false,
                NextId = eventTypeAddedLetter.Id,
                PrevId = null,
                CorrespondingStatus = statusActive
            };
            var eventTypeCreateCold = new EventType()
            {
                Id = 40,
                Description = "Created Cold Contact Application",
                Initial = true,
                NextId = eventTypeAddedCv.Id,
                PrevId = null,
                CorrespondingStatus = statusOpen
            };
            var eventTypeCreateAdvert = new EventType()
            {
                Id = 30,
                Description = "Created Advert Application",
                Initial = true,
                NextId = eventTypeAddedCv.Id,
                PrevId = null,
                CorrespondingStatus = statusOpen
            };
            var eventTypeCreateAgency = new EventType()
            {
                Id = 20,
                Description = "Created Agency Application",
                Initial = true,
                NextId = eventTypeAddedCv.Id,
                PrevId = null,
                CorrespondingStatus = statusOpen
            };
            var eventTypeDirect = new EventType()
            {
                Id = 10,
                Description = "Created Direct Application",
                Initial = true,
                NextId = eventTypeAddedCv.Id,
                PrevId = null,
                CorrespondingStatus = statusOpen
            };

            //  Add the Eventtypes to the contet.
            context.EventTypes.AddOrUpdate(eventTypeDirect);
            context.EventTypes.AddOrUpdate(eventTypeCreateAgency);
            context.EventTypes.AddOrUpdate(eventTypeCreateAdvert);
            context.EventTypes.AddOrUpdate(eventTypeCreateCold);
            context.EventTypes.AddOrUpdate(eventTypeAddedCv);
            context.EventTypes.AddOrUpdate(eventTypeAddedLetter);
            context.EventTypes.AddOrUpdate(eventtypeSendApplication);
            context.EventTypes.AddOrUpdate(eventTypeShortListed);
            context.EventTypes.AddOrUpdate(eventTypeInterviewArranged);
            context.EventTypes.AddOrUpdate(eventTypeConditionalOffer);
            context.EventTypes.AddOrUpdate(eventTypeReferencesRequested);
            context.EventTypes.AddOrUpdate(eventTypeUnconditionalReceived);
            context.EventTypes.AddOrUpdate(eventTypeOfferAccepted);
            context.EventTypes.AddOrUpdate(eventTypeOfferRefused);
            context.EventTypes.AddOrUpdate(eventTypeRejection);
            context.EventTypes.AddOrUpdate(eventTypeClosed);

            //  Contacts
            var tim = new Contact()
            {
                Id = 1,
                Name = "Tim Harrison",
                Phone = "07710 907345",
            };
            var nancy = new Contact()
            {
                Id = 2,
                Name = "Nancy Harrison",
                Phone = "01560 600457"
            };

            //  Add the Contacts
            context.Contacts.AddOrUpdate(tim);
            context.Contacts.AddOrUpdate(nancy);

            //  Organisations
            var cauldstanesRecruitment = new Agency()
            {
                Id = 1,
                Name = "Cauldstanes Recruitment",
                Address = "Cauldstanes, Fenwick, Ayrshire",
                PostCode = "KA3 6EX",
                Contacts = new List<Contact>()
                {
                    nancy
                }
            };

            var cauldstanesLtd = new Company()
            {
                Id = 2,
                Name = "Cauldstanes Software Ltd.",
                Address = "Cauldstanes, Fenwick, Ayrshire",
                PostCode = "KA3 6EX",
                Contacts = new List<Contact>()
                {
                    tim
                }
            };

            //  Add the Organisations
            context.Companies.AddOrUpdate(cauldstanesLtd);
            context.Agencies.AddOrUpdate(cauldstanesRecruitment);


            //  JobApplications
            var jobApp1 = new JobApplication()
            {
                Id = 1,
                Title = ".Net Developer C#",
                ClosingDate = DateTime.Now.AddDays(7),
                CreateDate = DateTime.Now,
                Location = "Ayrshire",
                Description = "Cauldstanes are looking for a .Net developer....",
                Agency = cauldstanesRecruitment,
                AgencyId = cauldstanesRecruitment.Id,
                Company = cauldstanesLtd,
                CompanyId = cauldstanesLtd.Id,
                Status = statusActive,
                Benefits = string.Empty,
                Conditions = string.Empty,
                Deleted = false,
                KeySkills = "ASP.Net MVC, C#",
                NiceToHaveSkills = string.Empty,
                Events = new List<Event>()
            };

            //  Add the JobApplocations
            context.JobApplications.AddOrUpdate(jobApp1);
        }

    }

}

