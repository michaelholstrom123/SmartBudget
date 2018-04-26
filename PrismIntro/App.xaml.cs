using Prism;
using Prism.Ioc;
using PrismIntro.ViewModels;
using PrismIntro.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using PrismIntro.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismIntro
{
    public partial class App : PrismApplication
    {

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()//Need for Prism to intialize component.
        {
           
            InitializeComponent();
            await NavigationService.NavigateAsync("PrismIntroPage");


        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry) //One of first called.
        {
            

            containerRegistry.RegisterForNavigation<RegisterPage>();
            containerRegistry.RegisterForNavigation<MainPage,MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PrismIntroPage, IntroToPrismsPageViewModel>();

            containerRegistry.RegisterSingleton<IRepository, Repository>();

        }
       

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
