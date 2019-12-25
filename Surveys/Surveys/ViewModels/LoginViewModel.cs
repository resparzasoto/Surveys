using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.ServiceInterfaces;
using Surveys.Views;
using System;
using System.Windows.Input;

namespace Surveys.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService = null;

        private readonly IWebApiService webApiService = null;

        private readonly IPageDialogService pageDialogService = null;

        #region Propiedades
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                {
                    return;
                }
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                if (username == value)
                {
                    return;
                }
                username = value;
                RaisePropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (password == value)
                {
                    return;
                }
                password = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigationService, IWebApiService webApiService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.webApiService = webApiService;
            this.pageDialogService = pageDialogService;

            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCommandCanExecute)
                .ObservesProperty(() => Username)
                .ObservesProperty(() => Password);
        }

        private async void LoginCommandExecute()
        {
            IsBusy = true;

            try
            {
                var loginResult = await webApiService.LoginAsync(Username, Password);

                if (loginResult)
                {
                    await navigationService.NavigateAsync($"app:///{nameof(MainView)}/{nameof(RootNavigationView)}/{nameof(SurveysView)}");
                }
                else
                {
                    await pageDialogService.DisplayAlertAsync(Literals.LoginTitle, Literals.AccessDenied, Literals.Ok);
                }
            }
            catch (Exception e)
            {
                await pageDialogService.DisplayAlertAsync(Literals.LoginTitle, e.Message, Literals.Ok);
            }

            IsBusy = false;
        }

        private bool LoginCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
