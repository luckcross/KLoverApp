using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KLoversApp.Views.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoScreen1 : ContentPage
    {
        public InfoScreen1()
        {
            InitializeComponent();
            Init();
            btn_teste.Clicked += Btn_teste_ClickedAsync;
        }

        private async void Btn_teste_ClickedAsync(object sender, EventArgs e)
        {
            waitActivityIndicator.IsRunning = true;
            try
            {
                Models.Token tk = await App.RestService.LoginProvisorio(App.facebookResponse.Id);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.ToString(), "Aceitar");
                activitySpinner.IsVisible = false;
                activitySpinner.IsRunning = false;
            }

            waitActivityIndicator.IsRunning = false;

        }

        private void Init()
        {
            activitySpinner.IsRunning = true;
            activitySpinner.IsVisible = true;

            if (App.facebookResponse != null)
            {
                lbl_teste.Text = App.facebookResponse.FirstName
                    + " -- " + App.facebookResponse.Gender
                    + " -- " + App.facebookResponse.Cover.Id
                    + " -- " + App.facebookResponse.Cover.OffsetY
                    + " -- " + App.facebookResponse.Devices
                    + " -- " + App.facebookResponse.AgeRange
                    + " -- " + App.facebookResponse.Name
                    + " -- " + App.facebookResponse.Locale;
            }

            activitySpinner.IsVisible = false;
            activitySpinner.IsRunning = false;
        }
    }
}