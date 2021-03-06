﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Prism.Navigation;
using Prism.Commands;
using Prism.Unity;
using PrismIntro.ViewModels;
using PrismIntro.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace PrismIntro.ViewModels
{

    public class IntroToPrismsPageViewModel : BindableBase, INavigationAware
    {
        INavigationService _navigationService;
     
        public DelegateCommand NavToRegisterCommand { get; set; }
        public DelegateCommand LoginPageCommand { get; set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _userC;
        public string userC
        {
            get { return _userC; }
            set { SetProperty(ref _userC, value); }
        }
        private string _userPass;
        public string userPass
        {
            get { return _userPass; }
            set { SetProperty(ref _userPass, value); }
        }

        private string _resultText;
        public string ResultText
        {
            get { return _resultText; }
            set { SetProperty(ref _resultText, value); }
        }

        public IntroToPrismsPageViewModel(INavigationService navigationService)
        {

            Debug.WriteLine($"****{this.GetType()}:  ctor");


            Title = "Launch Page";
            _navigationService = navigationService;
 
            NavToRegisterCommand = new DelegateCommand(OnNavToRegisterPage);
            LoginPageCommand = new DelegateCommand(OnNavToMainPage);

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnNavigatedFrom)}");
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnNavigatedTo)}");
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnNavigatingTo)}");
        }

        private void OnNavToRegisterPage()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnNavToRegisterPage)}");
            _navigationService.NavigateAsync("RegisterPage");
        }
        private async void OnNavToMainPage()
        {


            bool flag = false;

            await Task.Run(async () =>
            {
                try
                {
                    string Querey = $"SELECT PASSWORD FROM USERS WHERE USERNAME = '{userC}'";
                    Debug.WriteLine($"**** {Querey}");
                    List<string> Result = DependencyService.Get<IDbDataFetcher>().GetData(Querey);

                    if (Result.Count() == 0)
                    {
                        ResultText = "Username Not Found";

                    }
                    else if (Result[0] == userPass)
                    {
                        ResultText = "LOGIN SUCCESFULL";
                        flag = true;

                    }
                    else
                    {
                        ResultText = "INCORRECT PASSWORD";
                    }

                }
                catch (System.OperationCanceledException ex)
                {
                    Debug.WriteLine($"Text load cancelled: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            if(flag == true)
            {
                NavigationParameters navParams = new NavigationParameters();
                navParams.Add(Constants.Constants.USER_KEY, userC);

                await _navigationService.NavigateAsync("MainPage", navParams);
            }



        }
               
    }
}