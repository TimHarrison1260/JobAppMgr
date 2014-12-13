using System.Collections.Generic;
using AutoMapper;

namespace JobAppMgr.Mappings
{
    public class BaseMapper<T, U>
    {
        public U Map(T obj)
        {
            var viewModel = Mapper.Map<T, U>(obj);
            return viewModel;
        }

        /// <summary>
        /// Map collections of objects
        /// </summary>
        /// <param name="obj">Object to be mapped</param>
        /// <returns>Mapped object</returns>
        /// <remarks>
        /// This is marked as "Virtual" to allow it to be overridden
        /// since mapping from the viewModel to the Domain model for
        /// a create or edit operation does not need the collections
        /// version so it can be overridden as "Not implemented"
        /// </remarks>
        public virtual IEnumerable<U> Map(IEnumerable<T> obj)
        {
            var viewModel = Mapper.Map<IEnumerable<T>, IEnumerable<U>>(obj);
            return viewModel;
        }
    }
}