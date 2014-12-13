using System;
using System.Data.Entity;
using System.Linq;
using Core.Interfaces;
using Core.Model;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class JobApplicationRepository : BaseRepository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        { }

        public override IQueryable<JobApplication> GetAll()
        {
            var result = _db.JobApplications
                .Include(a => a.Status)
                .Include(a => a.Company)
                .Include(a => a.Agency);
            return result;
        }

        public override IQueryable<JobApplication> Search(string queryString)
        {
            throw new NotImplementedException();
        }


        public override JobApplication Get(int id)
        {
            //  Override so the include statements can be added to the linq statement.
            var result = _db.JobApplications
                .Include(j => j.Status)
                .Include(j => j.Company)
                .Include(j => j.Agency)
                .Include(j => j.Events)
                .FirstOrDefault(j => j.Id == id);
            return result;
        }

        public IQueryable<Event> GetJobEvents(int id)
        {
            var jobEvents = _db.Events
                .Include(e => e.Type)
                .Where(e => e.JobApplicationId == id);
            return jobEvents;
        }
    }
}
