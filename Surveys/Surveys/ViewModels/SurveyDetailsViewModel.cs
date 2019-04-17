using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Models;
using Surveys.ServiceInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Surveys.ViewModels
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;

        private IPageDialogService pageDialogService = null;

        private ILocalDbService localDbService = null;

        #region Propiedades
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

        private ObservableCollection<string> teams;

        public ObservableCollection<string> Teams
        {
            get { return teams; }
            set
            {
                if (teams == value)
                {
                    return;
                }
                teams = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILocalDbService localDbService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            this.localDbService = localDbService;

            Teams = new ObservableCollection<string>(new[]
            {
                "Alianza Lima",
                "Ámerica",
                "Boca Juniors",
                "Caracas FC",
                "Colo-Colo",
                "Peñarol",
                "Real Madrid",
                "Saprissa"
            });

            SelectTeamCommand = new DelegateCommand(SelectTeamCommandExecute);

            EndSurveyCommand = new DelegateCommand(EndSurveyCommandExecute, EndSurveyCommandCanExecute)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => FavoriteTeam)
                .ObservesProperty(() => BirthDate);
        }

        private async void SelectTeamCommandExecute()
        {
            var team = await pageDialogService.DisplayActionSheetAsync(Literals.FavoriteTeamTitle, null, null, Teams.ToArray());
            FavoriteTeam = team;
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Birthdate = BirthDate,
                FavoriteTeam = FavoriteTeam
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
            return ((!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam)) && (BirthDate != default(DateTime) && BirthDate != Convert.ToDateTime("01/01/1900")));
        }
    }
}
