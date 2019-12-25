using Prism.Commands;
using Prism.Navigation;
using Surveys.ServiceInterfaces;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Surveys.ViewModels
{
    public class SyncViewModel : ViewModelBase
    {
        private readonly IWebApiService webApiService = null;

        private readonly ILocalDbService localDbService = null;

        private string status;

        public string Status
        {
            get { return status; }
            set
            {
                if (status == value)
                {
                    return;
                }
                status = value;
                RaisePropertyChanged();
            }
        }

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

        public ICommand SyncCommand { get; set; }

        public SyncViewModel(IWebApiService webApiService, ILocalDbService localDbService)
        {
            this.webApiService = webApiService;
            this.localDbService = localDbService;
            SyncCommand = new DelegateCommand(SyncCommandExecuteAsync);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            Status = Application.Current.Properties.ContainsKey("lastSync") ? 
                $"Última actualización: {Convert.ToDateTime(Application.Current.Properties["lastSync"]).ToString("dd/MM/yyyy HH:mm:ss")}" : 
                "No se han sincronizado los datos";
        }

        private async void SyncCommandExecuteAsync()
        {
            IsBusy = true;

            //Envía las encuestas
            var allSurveys = await localDbService.GetAllSurveysAsync();

            if (allSurveys != null && allSurveys.Any())
            {
                await webApiService.SaveSurveysAsync(allSurveys);

                await localDbService.DeleteAllSurveysAsync();
            }

            //Consulta los equipos
            var allTeams = await webApiService.GetTeamsAsync();

            if (allTeams != null && allTeams.Any())
            {
                await localDbService.DeleteAllTeamsAsync();

                await localDbService.InsertTeamsAsync(allTeams);
            }

            Application.Current.Properties["lastSync"] = DateTime.Now;

            await Application.Current.SavePropertiesAsync();

            Status = $"Se enviarón {allSurveys.Count()} encuestas y se obtuvierón {allTeams.Count()} equipos";

            IsBusy = false;
        }
    }
}
