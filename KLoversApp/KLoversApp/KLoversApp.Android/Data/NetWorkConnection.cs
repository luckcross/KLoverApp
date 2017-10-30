using Android.App;
using Android.Content;
using Android.Net;
using KLoversApp.Data;
using KLoversApp.Droid.Data;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]

namespace KLoversApp.Droid.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }

        }
    }
}