using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Interface <c>IUnitOfWork</c> is responsible for managing
    /// access to the underlying database, using Entity Framework
    /// and populating the Model Objects.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Provides access to the JobApplication Entity
        /// </summary>
        IDbSet<JobApplication> JobApplications { get; set; }

        int SaveChanges();
    }
}
