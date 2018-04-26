using System.Collections.Generic;
using System.Threading.Tasks;
using PrismIntro.Models;

namespace PrismIntro.Services
{
    public interface IRepository
    {
        /// <summary>
        /// Fetches people from our data source, which could be a database or web service of some sort.
        /// </summary>
        /// <returns>A list of Person objects.</returns>
        Task<IList<Category>> GetCategories();

        /// <summary>
        /// An overload of GetPeople to allow you ro return any number of people so you can experiment with
        /// a much longer list without having to create a hundred entries of your own.
        /// </summary>
        /// <returns>As many Person objects as you specify.</returns>
        /// <param name="numberOfCategories">Number of Person objects you'd like to retrieve.</param>
        Task<IList<Category>> GetCategories(int numberOfCategories);

        
    }
}