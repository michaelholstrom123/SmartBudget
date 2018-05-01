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
    public class MainPageViewModel : ViewModelBase
    {
        IRepository _repository;

        public DelegateCommand PullToRefreshCommand { get; set; }
        public DelegateCommand<Category> CategoryTappedCommand { get; set; }
        public DelegateCommand<Category> CategorySelectedCommand { get; set; }
        public DelegateCommand<Category> InfoCommand { get; set; }
   

        //Note:  This is bound to the ItemsSource for the ListView on MainPage.
        private ObservableCollection<Category> _category;
        public ObservableCollection<Category> Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
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
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }

        public MainPageViewModel(INavigationService navigationService, IRepository repository)
            : base(navigationService)
        {
            _repository = repository;
            Title = "Party People";

            PullToRefreshCommand = new DelegateCommand(OnPullToRefresh);
            CategoryTappedCommand = new DelegateCommand<Category>(OnCategoryTapped);
            CategorySelectedCommand = new DelegateCommand<Category>(OnCategorySelected);
            InfoCommand = new DelegateCommand<Category>(OnInfoTapped);

            RefreshPeopleList();
           
        }

        private void OnInfoTapped(Category categoryTapped)
        {
            string selectedCategoryString = SelectedCategory == null ? "null" : SelectedCategory.ToString();
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnInfoTapped)}:  {categoryTapped}\n\tFYI, the SelectedPerson is {selectedCategoryString}");
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
        /// <param name="categorySelected">Person selected.</param>
        private async void OnCategorySelected(Category categorySelected)
        {
            
            var categoryToBePassed = new Category { CategoryName = categorySelected.ToString() };
            
            var navParams = new NavigationParameters();
            navParams.Add("a", categoryToBePassed);
            
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnCategorySelected)}:  {categorySelected}");
            await _navigationService.NavigateAsync("CategoryPage",navParams);
        }

        /// <summary>
        /// Executed when the ItemTapped event is raised on the associated ListView in the MainPage.
        /// The ItemTapped event is raised every single time a ListView item is tapped, whether it is
        /// the currently selected item or not.
        /// </summary>
        /// <param name="categoryTapped">Person tapped.</param>
        private void OnCategoryTapped(Category categoryTapped)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnCategoryTapped)}:  {categoryTapped}");
        }
        /// <summary>
        /// Check the parameters for a new person key and add to list if there is one.
        /// </summary>
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters != null)
            {
                base.OnNavigatingTo(parameters);
            }
              
        }
        private async Task RefreshPeopleList()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(RefreshPeopleList)}");

            ShowIsBusySpinner = true;
            SelectedCategory = null;
            var listOfCategories = await _repository.GetCategories();
            Category = new ObservableCollection<Category>(listOfCategories);
            ShowIsBusySpinner = false;
        }

    }
}