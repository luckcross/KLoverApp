using KLoversApp.Models.Facebook;
using KLoversApp.Views.RendererViews;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginFacebookView), typeof(KLoversApp.iOS.Renderers.LoginFacebookRenderer))]

namespace KLoversApp.iOS.Renderers
{
    public class LoginFacebookRenderer : PageRenderer
    {
        bool done = false;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (done)
            {
                return;
            }

            var auth = new OAuth2Authenticator(
                clientId: "132175780806492",
                scope: "",
                authorizeUrl: new Uri("https://www.facebook.com/v2.8/dialog/oauth"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) =>
            {
                DismissViewController(true, null);
                App.LoginFacebookFail();

                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var profile = await GetFacebookProfileAsync(accessToken);
                    App.LoginFacebookSuccess(profile);
                }
                else
                {
                    App.LoginFacebookSuccess(null);
                }
            };

            done = true;
            PresentViewController(auth.GetUI(), true, null);

            // REVER: TODO ESSE CODIGO ABAIXO FOI COPIADO DO 
            // http://rasmustc.com/blog/Custom-Facebook-Authentication-with-webapi/
            // E PRECISA SER AVALIADO SE FUNCIONA E SE VALE A PENA
            UIViewController vc = auth.GetUI();

            ViewController.AddChildViewController(vc);
            ViewController.View.Add(vc.View);

            // add out custom cancel button, to be able to navigate back
            vc.ChildViewControllers[0].NavigationItem.LeftBarButtonItem = new UIBarButtonItem(
                UIBarButtonSystemItem.Cancel, async (o, eargs) => await App.Current.MainPage.Navigation.PopModalAsync()
            );
        }

        private async Task<FacebookResponse> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.8/me/?fields=" +
                "name,picture,cover,age_range,devices,email,gender," +
                "is_verified,birthday,languages,work,website,religion," +
                "location,locale,link,first_name,last_name," +
                "hometown&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookResponse = JsonConvert.DeserializeObject<FacebookResponse>(userJson);
            return facebookResponse;
        }
    }

}