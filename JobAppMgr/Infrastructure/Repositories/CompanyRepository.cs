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
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        public override IQueryable<Company> Search(string queryString)
        {
            throw new NotImplementedException();
        }

        public override Company Get(int id)
        {
            var company = _db.Companies
                .Include(c => c.Contacts)
                .FirstOrDefault(n => n.Id == id);
            return company;
        }
    }
}
