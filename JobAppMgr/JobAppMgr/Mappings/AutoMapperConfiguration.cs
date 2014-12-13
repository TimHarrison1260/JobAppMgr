using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;           //  Access to AutoMapper
using Core.Model; //  Access to the Core.Model: Business model.
using JobAppMgr.ViewModels;


namespace JobAppMgr.Mappings
{
    /// <summary>
    /// This <c>AutoMapperConfiguration</c> class is responsible for 
    /// initialising the AutoMapper instance.
    /// It registers an instance of an AutoMapper.Profile class, also in this
    /// module.
    /// </summary>
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //  Instantiate the maps
            Mapper.Initialize(c => c.AddProfile(new JobAppMgrProfile()));
        }
    }

    /// <summary>
    /// The profile used for this application.
    /// It contains the individual mappings for classes
    /// used within this application.
    /// </summary>
    public class JobAppMgrProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            //  Map by convention: where possible, and add specific rules where the object names don't match.

            //  Map the JobApplication to the JobListViewModel.
            Mapper.CreateMap<JobApplication, JobListViewModel>()
                //  Map the Category description to the display category
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.ClosingDate, opt =>opt.MapFrom(src => string.Format("{0:D}",src.ClosingDate)))
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(SurveyStatusEnum), src.Status)))   // Map to the enumeration getName
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency.Name))
                ;

            //  Map the Job application and Details View model
            Mapper.CreateMap<JobApplication, JobDetailsViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Agency.Name))
                ;

            //  Map JobApplications and JobviewModel.
            Mapper.CreateMap<JobApplication, JobEditViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                //.ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.Companies, opt => opt.Ignore()) //  Ignore the SelectList of companies
                .ForMember(dest => dest.AgencyId, opt => opt.MapFrom(src => src.Agency.Id))
                .ForMember(dest => dest.Agencies, opt => opt.Ignore())
                ;

            Mapper.CreateMap<JobEditViewModel, JobApplication>()
                //.ForMember(dest => dest.Company.Id, opt => opt.MapFrom(src => src.CompanyId))   // Store company Id in Company model
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Company, opt => opt.Ignore())
                .ForMember(dest => dest.Agency, opt => opt.Ignore())
                .ForMember(dest => dest.Events, opt => opt.Ignore())        // TODO: take into account later on perhaps
                .ForMember(dest => dest.Deleted, opt => opt.Ignore())
                //.AfterMap((src, dest) => dest.Company.Id = src.CompanyId)
                //.AfterMap((src, dest) => dest.Agency.Id = src.AgencyId)     
                ;

            //  Mapping for adding Events to Job Aopplication
            Mapper.CreateMap<Event, EventListviewModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => string.Format("{0:D}", src.Date)))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.Description))
                //.ForMember(dest => dest.Attachment, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Attachment) ? src.Attachment : "none"))
                ;

            Mapper.CreateMap<AddEventViewModel, Event>()
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.JobApplication, opt => opt.Ignore())
                ;



            //  Map Contact and ContactViewModel
            Mapper.CreateMap<Contact, ContactViewModel>();

            Mapper.CreateMap<ContactViewModel, Contact>()
                .ForMember(dest => dest.Organisation, opt => opt.Ignore())
                ;

            //  Map Companies and Company view models
            Mapper.CreateMap<Company, CompanyListViewModel>();

            //  Map the Company, which contains a list of Contacts.
            //  See http://automapper.codeplex.com/workitem/6044 on how to configure the
            //  mapping to facilitate mapping Contact to ContactViewModel, defined above.
            Mapper.CreateMap<Company, CompanyViewModel>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom<IEnumerable<Contact>>(src => src.Contacts))
                ;

            Mapper.CreateMap<CompanyViewModel, Company>()
                .ForMember(dest => dest.JobApplications, opt => opt.Ignore())   //  Nothing to do with JobApplications
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom<IEnumerable<ContactViewModel>>(src => src.Contacts))
                ;

            //  Configure the mappings for Agencies and corresponding viewModels
            Mapper.CreateMap<Agency, AgencyListViewModel>();

            Mapper.CreateMap<Agency, AgencyViewModel>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom<IEnumerable<Contact>>(src => src.Contacts))
                ;

            Mapper.CreateMap<AgencyViewModel, Agency>()
                .ForMember(dest => dest.JobApplications, opt => opt.Ignore())   //  Nothing to do with JobApplications
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom<IEnumerable<ContactViewModel>>(src => src.Contacts))
                ;



            //  Map the CreateSurvey view model to the survey model.
            //  Title, CategoryId, Status, StatusDate and Category all map by convention
            //Mapper.CreateMap<CreateSurveyViewModel, Survey>()
            //    .ForMember(dest => dest.SurveyId, opt => opt.Ignore())
            //    .ForMember(dest => dest.Status, opt => opt.Ignore())
            //    .ForMember(dest => dest.StatusDate, opt => opt.Ignore())
            //    .ForMember(dest => dest.User, opt => opt.Ignore())
            //    .ForMember(dest => dest.IsTemplate, opt => opt.Ignore())
            //    .ForMember(dest => dest.Respondents, opt => opt.Ignore())
            //    .ForMember(dest => dest.Questions, opt => opt.Ignore())
            //    .ForMember(dest => dest.Category, opt => opt.Ignore())
            //    ;

            //  For mapping the Likert Templated responses to the partial view
            //  displaying them as part of the Add Questions.
            //Mapper.CreateMap<TemplateResponse, LikertResponseViewModel>();

            //  For mapping the AddQuestions to the Question model.
            //Mapper.CreateMap<AddQuestionsViewModel, Question>()
            //    //  This .ConstructUsing allows us to use the QuestionFactory to create the instance of Question 
            //    //  (because  Question is an abstract class).
            //    .ConstructUsing((Func<ResolutionContext, Question>)(r => Core.Factories.QuestionFactory.CreateQuestion()))
            //    .ForMember(dest => dest.SequenceNumber, opt => opt.Ignore())
            //    .ForMember(dest => dest.Survey, opt => opt.Ignore())
            //    .ForMember(dest => dest.QuestionId, opt => opt.Ignore())
            //    .ForMember(dest => dest.AvailableResponses, opt => opt.Ignore())
            //    ;

            //  For mapping the Likert Templated Responses to the Available responses for a question
            //Mapper.CreateMap<TemplateResponse, AvailableResponse>()
            //    .ForMember(dest => dest.Question, opt => opt.Ignore())
            //    ;

            //  Map the questions to the Question list partial view, displayed in the AddQuestions
            //Mapper.CreateMap<Question, AddQuestionsListViewModel>()
            //    .ForMember(dest => dest.Title, opt => opt.UseValue(string.Empty))
            //    ;

            //  For mapping the survey to the ApprovalListViewModel
            //Mapper.CreateMap<Survey, ApprovalListViewModel>()
            //    .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Category.Description))
            //    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            //    //.ForMember(d => d.SurveyId, o => o.MapFrom(s => s.SurveyId))
            //    //.ForMember(dest => dest.Approve, opt => opt.UseValue(false))
            //    //.ForMember(dest => dest.Input.SurveyId, opt => opt.MapFrom(src => src.SurveyId))
            //    //.ForMember(dest => dest.Input.Approve, opt => opt.UseValue(false))
            //    .ForMember(dest => dest.Input, opt => opt.Ignore()) 
            //    .AfterMap((src, dest) => dest.Input = new ApprovalListViewModel.ApprovalInputViewModel())
            //    .AfterMap((src, dest) => dest.Input.SurveyId = src.SurveyId )
            //    .AfterMap((src, dest) => dest.Input.Approve = false)
            //    ;
            //  See https://groups.google.com/forum/?fromgroups=#!topic/automapper-users/YG13vBf9lX8 for explanation of 
            //  the above use of .AfterMap, the dest side cannot contain child classes.

            //  Assert the mappings are valid and can be used.
            Mapper.AssertConfigurationIsValid();
        }
    }

}