using Prism.Ioc;
using Prism.Unity;
using Surveys.ServiceInterfaces;
using Surveys.Services;
using Surveys.ViewModels;
using Surveys.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Surveys
{
    public partial class App : PrismApplication
    {
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(LoginView)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RootNavigationView>();
            containerRegistry.RegisterForNavigation<SurveysView, SurveysViewModel>();
            containerRegistry.RegisterForNavigation<SurveyDetailsView, SurveyDetailsViewModel>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
            containerRegistry.RegisterForNavigation<SyncView, SyncViewModel>();
            containerRegistry.RegisterForNavigation<TeamSelectionView, TeamSelectionViewModel>();

            containerRegistry.RegisterInstance<ILocalDbService>(new LocalDbService());
            containerRegistry.RegisterInstance<IWebApiService>(new WebApiService());
        }
    }
}
