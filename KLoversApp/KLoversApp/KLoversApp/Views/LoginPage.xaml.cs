using KLoversApp.Models;
using KLoversApp.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KLoversApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            lbl_Username.TextColor = Constants.MainTextColor;
            lbl_Password.TextColor = Constants.MainTextColor;
            activitySpinner.IsVisible = false;
            loginIcon.HeightRequest = Constants.LoginIconHeight;
            App.StartCheckIfInternet(lblNoInternet, this);

            entry_UserName.Completed += (s, e) => entry_Password.Focus();
            entry_Password.Completed += (s, e) => SignInAsync(s, e);
        }

        private async Task SignInAsync(object sender, EventArgs e)
        {
            User user = new User(entry_UserName.Text, entry_Password.Text);
            if (user.CheckInformation())
            {
                activitySpinner.IsVisible = true;

                // TODO: Usar este quando o web api estiver funcionando retornando um token
                //Token tokenResult = await App.RestService.Login(user);
                Token tokenResult = new Token();
                await DisplayAlert("Login", "Login Success", "Oke");

                if (App.SettingsDatabase.GetSettings() == null)
                {
                    Settings settings = new Settings();
                    App.SettingsDatabase.SaveSettings(settings);
                }
                

                //if (tokenResult.AcessToken != null)
                if (tokenResult != null)
                {
                    activitySpinner.IsVisible = false;
                    // TODO: Usar este quando o web api estiver funcionando
                    //App.UserDatabase.SaveUser(user);
                    //App.TokenDatabase.SaveToken(tokenResult);

                    if (Device.OS == TargetPlatform.Android)
                    {
                        Application.Current.MainPage = new Menu.MainPage();
                    }
                    else if (Device.OS == TargetPlatform.iOS)
                    {
                        await Navigation.PushModalAsync(new Menu.MainPage());
                    }
                }
            }
            else
            {
                await DisplayAlert("Login", "Login Not Correct, empty username or password.", "Ok");
                activitySpinner.IsVisible = false;
            }

        }
    }
}