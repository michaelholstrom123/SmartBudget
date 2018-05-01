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
        IList<Transaction> transactionsFromSomeDataSource = null;

       /* public Repository2()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(Repository2)}:  ctor");
        }*/

        // Method summary provided in interface.
        public async Task<IList<Transaction>> GetTransactions()
        {
            if (transactionsFromSomeDataSource == null)
            {
                transactionsFromSomeDataSource = GetCannedData2();
            }

            // Let's pretend this is calling out to a web service of some sort to get the data, 
            // so it will take some time...
            await Task.Delay(2000);

            return transactionsFromSomeDataSource;
        }
        /// <summary>
        /// Hard-coded data to seed our list of People.
        /// </summary>
        /// <returns>Pre-made data.</returns>
        private IList<Transaction> GetCannedData2()
        {
            var TransactionList = new List<Transaction>();
            TransactionList.Add(new Transaction { TransactionName = "Sprouts" });
            TransactionList.Add(new Transaction { TransactionName = "Autozone" });
            TransactionList.Add(new Transaction { TransactionName = "Walmart" });
            TransactionList.Add(new Transaction { TransactionName = "CSUSM" });

            return TransactionList;
        }
        // Method summary provided in interface.
        public async Task<IList<Transaction>> GetTransactions(int numberOfTransactions)
        {
            transactionsFromSomeDataSource = new List<Transaction>();


            for (int i = 0; i < numberOfTransactions; i++)
            {
                var newTransaction = new Transaction()
                {
                    TransactionName = $"TranactionName-{i}",

                };
                transactionsFromSomeDataSource.Add(newTransaction);
            }
            await Task.Delay(2000);

            return transactionsFromSomeDataSource;
        }


    }
    
}