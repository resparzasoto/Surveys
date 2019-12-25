using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.ServiceInterfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surveys.ViewModels
{
    public class SurveysViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService = null;

        private readonly IPageDialogService pageDialogService = null;

        private readonly ILocalDbService localDbService = null;

        #region Propiedades
        private ObservableCollection<SurveyViewModel> surveys;

        public ObservableCollection<SurveyViewModel> Surveys
        {
            get { return surveys; }
            set
            {
                if (surveys == value)
                {
                    return;
                }
                surveys = value;
                RaisePropertyChanged();
            }
        }

        private SurveyViewModel selectedSurvey;

        public SurveyViewModel SelectedSurvey
        {
            get { return selectedSurvey; }
            set
            {
                if (selectedSurvey == value)
                {
                    return;
                }
                selectedSurvey = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Surveys == null || (Surveys.Count <= 0);
            }
        }
        #endregion

        public ICommand NewSurveyCommand { get; set; }

        public ICommand DeleteSurveyCommand { get; set; }

        public SurveysViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILocalDbService localDbService = null)
        {
            this.navigationService = navigationService;
            this.localDbService = localDbService;
            this.pageDialogService = pageDialogService;

            Surveys = new ObservableCollection<SurveyViewModel>();

            NewSurveyCommand = new DelegateCommand(NewSurveyCommandExecute);

            DeleteSurveyCommand = new DelegateCommand(DeleteSurveyCommandExecute, DeleteSurveyCommandCanExecute)
                .ObservesProperty(() => SelectedSurvey);
        }

        private async void NewSurveyCommandExecute()
        {
            await navigationService.NavigateAsync(nameof(SurveyDetailsView));
        }

        private async void DeleteSurveyCommandExecute()
        {
            if (SelectedSurvey == null)
            {
                return;
            }

            var result = await pageDialogService.DisplayAlertAsync(
                Literals.DeleteSurveyTitle, 
                Literals.DeleteSurveyConfirmation, 
                Literals.Ok, 
                Literals.Cancel);

            if (result)
            {
                await localDbService.DeleteSurveyAsync(SurveyViewModel.GetEntityFromViewModel(SelectedSurvey));

                await LoadSurveysAsync();
            }
        }

        private bool DeleteSurveyCommandCanExecute()
        {
            return SelectedSurvey != null;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await LoadSurveysAsync();
        }

        private async Task LoadSurveysAsync()
        {
            var allTeams = await localDbService.GetAllTeamsAsync();

            var allSurveys = await localDbService.GetAllSurveysAsync();

            if (allSurveys != null)
            {
                Surveys = new ObservableCollection<SurveyViewModel>(allSurveys.Select(s => SurveyViewModel.GetViewModelFromEntity(s, allTeams)));
            }
            RaisePropertyChanged(nameof(IsEmpty));
        }
    }
}
