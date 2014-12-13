using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        /// <summary>
        /// Use Case: Display all entities
        /// </summary>
        /// <returns>Collection of Entities</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Ue Case: Search all Entities
        /// </summary>
        /// <param name="queryString">The Query parameters</param>
        /// <returns>Collection of Entityes</returns>
        IQueryable<T> Search(string queryString);

        /// <summary>
        /// Use Case: Various: Gets an Entity for the specified Id
        /// </summary>
        /// <param name="id">Id of the Entity</param>
        /// <returns>Eneity instance</returns>
        T Get(int id);

        /// <summary>
        /// Use Case: Creates a New Entity
        /// </summary>
        /// <param name="obj">The new entity</param>
        /// <returns>Id of the newly created entity</returns>
        T Create(T obj);

        /// <summary>
        /// Use Case: Edit or Update Entity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Update(T obj);

        /// <summary>
        /// Use Case: Delete the Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

    }
}
