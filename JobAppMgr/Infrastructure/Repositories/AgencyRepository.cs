using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Model;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class AgencyRepository : BaseRepository<Agency>, IAgencyRepository
    {
        public AgencyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public override IQueryable<Agency> Search(string queryString)
        {
            throw new NotImplementedException();
        }

        public override Agency Get(int id)
        {
            var agency = _db.Agencies
                .Include(c => c.Contacts)
                .FirstOrDefault(n => n.Id == id);
            return agency;
        }
    }
}
