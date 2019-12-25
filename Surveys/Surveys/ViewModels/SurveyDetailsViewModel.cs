using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Entities;
using Surveys.ServiceInterfaces;
using Surveys.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Surveys.ViewModels
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService = null;

        private readonly IPageDialogService pageDialogService = null;

        private readonly ILocalDbService localDbService = null;

        private IEnumerable<Team> localDbTeams = null;

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                {
                    return;
                }
                name = value;
                RaisePropertyChanged();
            }
        }

        private DateTime birthDate;

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate == value)
                {
                    return;
                }
                birthDate = value;
                RaisePropertyChanged();
            }
        }

        private string favoriteTeam;

        public string FavoriteTeam
        {
            get { return favoriteTeam; }
            set
            {
                if (favoriteTeam == value)
                {
                    return;
                }
                favoriteTeam = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILocalDbService localDbService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            this.localDbService = localDbService;

            SelectTeamCommand = new DelegateCommand(SelectTeamCommandExecute);

            EndSurveyCommand = new DelegateCommand(EndSurveyCommandExecute, EndSurveyCommandCanExecute)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => FavoriteTeam)
                .ObservesProperty(() => BirthDate);
        }

        private async void SelectTeamCommandExecute()
        {
            await navigationService.NavigateAsync(nameof(TeamSelectionView), parameters: null, useModalNavigation: true, animated: true);
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Birthdate = BirthDate,
                TeamId = localDbTeams.First(t => t.Name == FavoriteTeam).Id
        };

            var geolocationService = Xamarin.Forms.DependencyService.Get<IGeolocationService>();

            if (geolocationService != null)
            {
                try
                {
                    var currentLocation = await geolocationService.GetCurrentLocationAsync();
                    newSurvey.Lat = currentLocation.Item1;
                    newSurvey.Lon = currentLocation.Item2;
                }
                catch (Exception)
                {
                    newSurvey.Lat = 0;
                    newSurvey.Lon = 0;
                }
            }

            await localDbService.InsertSurveyAsync(newSurvey);

            await navigationService.GoBackAsync();
        }

        private bool EndSurveyCommandCanExecute()
        {
            return ((!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam)) && (BirthDate != default && BirthDate != Convert.ToDateTime("01/01/1900")));
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            localDbTeams = await localDbService.GetAllTeamsAsync();

            if (parameters.ContainsKey("id"))
            {
                FavoriteTeam = localDbTeams.First(t => t.Id == (int)parameters["id"]).Name;
            }
        }
    }
}
