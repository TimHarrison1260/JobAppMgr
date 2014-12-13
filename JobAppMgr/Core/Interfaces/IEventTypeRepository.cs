using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interfaces
{
    public interface IEventTypeRepository : IRepository<EventType>
    {

        IQueryable<EventType> GetNextTypes(int id);

        IQueryable<EventType> GetInitialTypes();
    }
}
