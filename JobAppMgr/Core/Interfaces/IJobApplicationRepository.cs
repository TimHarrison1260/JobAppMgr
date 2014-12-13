using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface <c>IJobApplicationRepository</c> describes the contract
    /// for the JobApplicationRepository class, that is responsible for 
    /// controlling access to the Model.
    /// </summary>
    public interface IJobApplicationRepository : IRepository<JobApplication>
    {
        IQueryable<Event> GetJobEvents(int id);
    }
}
