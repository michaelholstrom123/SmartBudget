using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using PrismIntro.Models;

namespace PrismIntro.Services
{
    public class Repository : IRepository
    {
        /// <summary>
        /// The people fetched from some data source, normally. For this example, we'll just use
        /// some hard-coded data.
        /// </summary>
        IList<Category> categoryFromSomeDataSource = null;

        public Repository()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(Repository)}:  ctor");
        }

        // Method summary provided in interface.
        public async Task<IList<Category>> GetCategories()
        {
            if (categoryFromSomeDataSource == null)
            {
                categoryFromSomeDataSource = GetCannedData();
            }

            // Let's pretend this is calling out to a web service of some sort to get the data, 
            // so it will take some time...
            await Task.Delay(2000);

            return categoryFromSomeDataSource;
        }

        /// <summary>
        /// Hard-coded data to seed our list of People.
        /// </summary>
        /// <returns>Pre-made data.</returns>
        private IList<Category> GetCannedData()
        {
            var CategoryList = new List<Category>();
            CategoryList.Add(new Category { CategoryName = "Grocery Stores & Drugstores" });
            CategoryList.Add(new Category { CategoryName = "Automotive" });
            CategoryList.Add(new Category { CategoryName = "Shopping" });
            CategoryList.Add(new Category { CategoryName = "Travel & Entertainment" });
            CategoryList.Add(new Category { CategoryName = "Home Improvement & Maintenance" });
            CategoryList.Add(new Category { CategoryName = "Tax Deductible & Related Expenses" });
            CategoryList.Add(new Category { CategoryName = "Restaurants" });
            CategoryList.Add(new Category { CategoryName = "Other Expenditures " });
            return CategoryList;
        }

        // Method summary provided in interface.
        public async Task<IList<Category>> GetCategories(int numberOfCategories)
        {
            categoryFromSomeDataSource = new List<Category>();

            for (int i = 0; i < numberOfCategories; i++)
            {
                var newCategory = new Category()
                {
                    CategoryName = $"CategoryName-{i}",
                  
                };
                categoryFromSomeDataSource.Add(newCategory);
            }

            await Task.Delay(2000);

            return categoryFromSomeDataSource;
        }

        // Method summary provided in interface.
 
    }
}