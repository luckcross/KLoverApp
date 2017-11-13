using Android.App;
using KLoversApp.Models.Facebook;
using KLoversApp.Views.RendererViews;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginFacebookView), typeof(KLoversApp.Droid.Renderers.LoginFacebookRenderer))]

namespace KLoversApp.Droid.Renderers
{
    public class LoginFacebookRenderer : PageRenderer
    {
        public LoginFacebookRenderer()
        {
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "132175780806492",
                scope: "",
                authorizeUrl: new Uri("https://www.facebook.com/v2.10/dialog/oauth"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var profile = await GetFacebookProfileAsync(accessToken);
                    App.LoginFacebookSuccess(profile);
                }
                else
                {
                    App.LoginFacebookFail();
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }

        async Task<FacebookResponse> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.10/me/?fields=" +
                "name,picture.width(999),cover,age_range,devices,email," +
                "gender,is_verified,birthday,languages,work,website," +
                "religion,location,locale,link,first_name,last_name," +
                "hometown&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookResponse = JsonConvert.DeserializeObject<FacebookResponse>(userJson);
            return facebookResponse;
        }
    }
}