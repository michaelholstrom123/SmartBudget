using System;
using System.Diagnostics;
using Prism.Commands;
using Prism.Navigation;
using PrismIntro.Models;
using Xamarin.Forms;

namespace PrismIntro.ViewModels
{
    public class AddExpensePageViewModel : ViewModelBase
    {
        public DelegateCommand SubmitCommand { get; set; }

        private string _newTransactionName;
        public string NewTransactionName
        {
            get { return _newTransactionName; }
            set { SetProperty(ref _newTransactionName, value); }
        }

        private string _transactionAmmount;
        public string TransactionAmmount
        {
            get { return _transactionAmmount; }
            set { SetProperty(ref _transactionAmmount, value); }
        }

        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        private Category _currentCategory;
        public Category CurrentCategory
        {
            get { return _currentCategory; }
            set { SetProperty(ref _currentCategory, value); }
        }

        public AddExpensePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(AddExpensePageViewModel)}");
            SubmitCommand = new DelegateCommand(OnSubmitPressed);
        }

        private async void OnSubmitPressed()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnSubmitPressed)}");
            string query = $"INSERT INTO EXPENSES(USER,EXPENSE_CAT,EXPENSE_TITLE,EXPENSE_VALUE) VALUES('{CurrentUser.UserName}','{CurrentCategory.CategoryName}','{NewTransactionName}','{TransactionAmmount}')";
            DependencyService.Get<IDbDataWriter>().WriteData(query);
            await _navigationService.GoBackAsync();
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnNavigatingTo)}");
            base.OnNavigatingTo(parameters);

            if (parameters != null && parameters.ContainsKey(Constants.Constants.USER_KEY) && parameters.ContainsKey(Constants.Constants.CATEGORY_KEY))
            {
                CurrentUser = (User)parameters[Constants.Constants.USER_KEY];
                CurrentCategory = (Category)parameters[Constants.Constants.CATEGORY_KEY];
            }

        }
    }
}
