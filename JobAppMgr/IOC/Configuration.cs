using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;                      // Ninject stuff.
using Ninject.Web.Common;           // Ninject extension method, InRequestScope()
using Ninject.Web;

using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Data;

namespace IOC
{
    public static class Configuration
    {
        public static void RegisterServices(IKernel kernel)
        {
            //  Inject the DbContext
            kernel.Bind<IUnitOfWork>().To<JobApplicationContext>().InRequestScope();

            //  Inject the concrete Repositories
            kernel.Bind<IJobApplicationRepository>().To<JobApplicationRepository>();
            kernel.Bind<IContactRepository>().To<ContactRepository>();
            kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
            kernel.Bind<IAgencyRepository>().To<AgencyRepository>();
            kernel.Bind<IEventTypeRepository>().To<EventTypeRepository>();
            kernel.Bind<IStatusRepository>().To<StatusRepository>();
        }
    }
}
