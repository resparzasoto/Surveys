using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surveys
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SurveysView : ContentPage
	{
		public SurveysView ()
		{
			InitializeComponent ();

            MessagingCenter.Subscribe<ContentPage, Survey>(this, Messages.NewSurveyComplete, (sender, args) =>
            {
                SurveysPanel.Children.Add(new Label()
                {
                    Text = args.ToString()
                });
            });
		}

        private async void AddSurveyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyDetailsView());
        }
    }
}