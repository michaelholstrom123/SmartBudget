using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using PrismIntro.Views;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.ComponentModel;
using PrismIntro.ViewModels;
using PrismIntro.Models;
using PrismIntro.Services;

namespace PrismIntro.ViewModels
{
    public class CategoryPageViewModel : ViewModelBase
    {
        IRepository _repository;
        public Category _category;


        public DelegateCommand PullToRefreshCommand { get; set; }
        public DelegateCommand<Transaction> TransactionTappedCommand { get; set; }
        public DelegateCommand<Transaction> TransactionSelectedCommand { get; set; }
        public DelegateCommand<Transaction> InfoCommand { get; set; }


        //Note:  This is bound to the ItemsSource for the ListView on MainPage.
        private ObservableCollection<Transaction> _transaction;
        public ObservableCollection<Transaction> Transaction
        {
            get { return _transaction; }
            set { SetProperty(ref _transaction, value); }
        }

        //Note:  Bound to both the ContentPage's AND the ListView's busy properties:  IsBusy is the
        //          property for the ContentPage, and IsRefreshing is the property for the ListView.
        private bool _showIsBusySpinner;
        public bool ShowIsBusySpinner
        {
            get { return _showIsBusySpinner; }
            set { SetProperty(ref _showIsBusySpinner, value); }
        }

        //Note:  This is bound to the currently selected person in the ListView.
        private Transaction _selectedTransaction;
        public Transaction SelectedTransaction
        {
            get { return _selectedTransaction; }
            set { SetProperty(ref _selectedTransaction, value); }
        }
        private String _headerText;
        public String HeaderText
        {
            get { return _headerText; }
            set { SetProperty(ref _headerText, value); }
        }

        public CategoryPageViewModel(INavigationService navigationService, IRepository repository)
            : base(navigationService)
        {
            _repository = repository;
            Title = "Party People";

            PullToRefreshCommand = new DelegateCommand(OnPullToRefresh);
            TransactionTappedCommand = new DelegateCommand<Transaction>(OnTransactionTapped);
            TransactionSelectedCommand = new DelegateCommand<Transaction>(OnTransactionSelected);
            InfoCommand = new DelegateCommand<Transaction>(OnInfoTapped);

            RefreshPeopleList();

        }

        private void OnInfoTapped(Transaction transactionTapped)
        {
            string selectedTransactionString = SelectedTransaction == null ? "null" : SelectedTransaction.ToString();
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnInfoTapped)}:  {transactionTapped}\n\tFYI, the SelectedPerson is {selectedTransactionString}");
        }

        private async void OnPullToRefresh()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnPullToRefresh)}");

            await RefreshPeopleList();
        }

        /// <summary>
        /// Executed when the ItemSelected event is raised on the associated ListView in the MainPage.
        /// Remember that the ItemSelected event only gets raised if the item that is tapped on is not
        /// the currently selected item.
        /// </summary>
        /// <param name="transactionSelected">Person selected.</param>
        private void OnTransactionSelected(Transaction transactionSelected)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnTransactionSelected)}:  {transactionSelected}");
        }

        /// <summary>
        /// Executed when the ItemTapped event is raised on the associated ListView in the MainPage.
        /// The ItemTapped event is raised every single time a ListView item is tapped, whether it is
        /// the currently selected item or not.
        /// </summary>
        /// <param name="transactionTapped">Person tapped.</param>
        private void OnTransactionTapped(Transaction transactionTapped)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnTransactionTapped)}:  {transactionTapped}");
        }
        /// <summary>
        /// Check the parameters for a new person key and add to list if there is one.
        /// </summary>
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            
            base.OnNavigatingTo(parameters);
            if (parameters != null && parameters.ContainsKey("a"))
            {
                _category = (Category)parameters["a"];
            }
            HeaderText = _category.ToString();

        }
        private async Task RefreshPeopleList()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(RefreshPeopleList)}");

            ShowIsBusySpinner = true;
            SelectedTransaction = null;
            var listOfTransactions = await _repository.GetTransactions();
            Transaction = new ObservableCollection<Transaction>(listOfTransactions);
            ShowIsBusySpinner = false;
        }

    }
}