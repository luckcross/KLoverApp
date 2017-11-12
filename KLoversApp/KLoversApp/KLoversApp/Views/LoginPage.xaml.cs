using KLoversApp.Models;
using KLoversApp.Views.Menu;
using KLoversApp.Views.RendererViews;
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
            act_Spinner.IsVisible = false;
            img_loginIcon.HeightRequest = Constants.LoginIconHeight;
            App.StartCheckIfInternet(lbl_NoInternet, this);

            entry_UserName.Completed += (s, e) => entry_Password.Focus();
            //entry_Password.Completed += (s, e) => SignInAsync(s, e);
            btn_SignIn.Clicked += async (s, e) => await SignInAsync(s, e);
            btn_SignInWithFB.Clicked += async (s, e) => await SigInWithFacebook(s, e);
        }

        private async Task SignInAsync(object sender, EventArgs e)
        {
            User user = new User(entry_UserName.Text, entry_Password.Text);
            if (user.CheckInformation())
            {
                act_Spinner.IsVisible = true;

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
                    act_Spinner.IsVisible = false;
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
                act_Spinner.IsVisible = false;
            }

        }

        private async Task SigInWithFacebook(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LoginFacebookView());
            Application.Current.MainPage = new LoginFacebookView();
        }
    }
}