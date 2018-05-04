using System;
using System.Diagnostics;
using Prism.Commands;
using Prism.Navigation;
using PrismIntro.Models;
using PrismIntro.Services;
using Xamarin.Forms;

namespace PrismIntro.ViewModels
{
    public class AddCategoryPageViewModel : ViewModelBase
    {

        public DelegateCommand SubmitCommand { get; set; }

        private string _newCategoryName;
        public string NewCategoryName
        {
            get { return _newCategoryName; }
            set { SetProperty(ref _newCategoryName, value); }
        }

        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        public AddCategoryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(AddCategoryPageViewModel)}");
            SubmitCommand = new DelegateCommand(OnSubmitPressed);
        }

        private async void OnSubmitPressed()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnSubmitPressed)}");
            string query = $"INSERT INTO EXPENSE_CATEGORY VALUES('{CurrentUser.UserName}','{NewCategoryName}')";
            DependencyService.Get<IDbDataWriter>().WriteData(query);
            await _navigationService.GoBackAsync();
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnNavigatingTo)}");
            base.OnNavigatingTo(parameters);

            if (parameters != null && parameters.ContainsKey(Constants.Constants.USER_KEY))
            {
                CurrentUser = (User)parameters[Constants.Constants.USER_KEY];
            }

        }
    }
}
