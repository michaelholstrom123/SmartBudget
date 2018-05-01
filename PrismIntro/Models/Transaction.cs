using System;
using Prism.Mvvm;

namespace PrismIntro.Models
{
    /// <summary>
    /// Simple model class for a Person. This is the data type our ListView contains.
    /// </summary>
    public class Transaction : BindableBase
    {
        private string _transactionName;
        public string TransactionName
        {
            get { return _transactionName; }
            set { SetProperty(ref _transactionName, value); }
        }

        public override string ToString()
        {
            return $"CategoryName={TransactionName}";
        }
    }
}