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

        private string _transactionAmmount;
        public string TransactionAmmount
        {
            get { return _transactionAmmount; }
            set { SetProperty(ref _transactionAmmount, value); }
        }

        public override string ToString()
        {
            return $"CategoryName={TransactionName}";
        }
    }
}