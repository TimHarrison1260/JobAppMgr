using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Model;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public override IQueryable<Status> Search(string queryString)
        {
            throw new NotImplementedException();
        }
    }
}
