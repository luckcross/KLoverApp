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

        // -------------Login-------------
        public static string LoginUril = "http://localhost:50340/api/AppUser/AddAppUser/";

        public static string NoInternetText = "No Internet, please reconnect.";
    }
}
