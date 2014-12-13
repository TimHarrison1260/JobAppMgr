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
    public class EventTypeRepository : BaseRepository<EventType>, IEventTypeRepository
    {
        public EventTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }


        public override IQueryable<EventType> Search(string queryString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all Event types that can be used after the existing event Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<EventType> GetNextTypes(int id)
        {
            var types = _db.EventTypes
                .Where(t => t.Id >= id && !t.Initial);
            return types;
        }

        public IQueryable<EventType> GetInitialTypes()
        {
            var types = _db.EventTypes
                .Where(t => t.Initial);
            return types;
        }


    }
}
