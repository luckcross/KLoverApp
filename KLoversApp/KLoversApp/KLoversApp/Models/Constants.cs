using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KLoversApp.Models
{
    public class Constants
    {
        public static bool IsDev = true;

        public static Color BackgroundColor = Color.FromRgb(58, 153, 215);
        public static Color MainTextColor = Color.White;

        public static int LoginIconHeight = 120;

        public static string HttpClientBaseAddress = "http://localhost:50340";
        // -------------Login-------------
        public static string LoginUrl = "/api/AppUser/AddAppUser/";

        public static string NoInternetText = "No Internet, please reconnect.";

        public static string SettingsScreenTitle = "Settings";
    }
}
