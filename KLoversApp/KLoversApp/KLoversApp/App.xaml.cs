using KLoversApp.Data;
using KLoversApp.Models;
using KLoversApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using KLoversApp.Utils;

namespace KLoversApp
{
    public partial class App : Application
    {
        private static TokenDatabaseController tokenDatabase;
        private static UserDatabaseController userDatabase;
        private static RestService restService;
        private static Label labelScreen;
        private static bool hasInternet;
        private static bool noInterShow;
        private static Page currentPage;
        private static Timer timer;

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }

                return userDatabase;
            }
        }

        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokenDatabase == null)
                {
                    tokenDatabase = new TokenDatabaseController();
                }

                return tokenDatabase;
            }
        }

        public static RestService RestService
        {
            get
            {
                if (restService == null)
                {
                    restService = new RestService();
                }

                return restService;
            }
        }

        // ------------------------------Internet Connection

        public static void StartCheckIfInternet(Label label, Page page)
        {
            labelScreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            hasInternet = true;
            currentPage = page;
            if (timer == null)
            {
                timer = new Timer((e) =>
                    {
                        CheckIfInternetOverTime();
                    }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);

            }
        }

        private static void CheckIfInternetOverTime()
        {
            INetworkConnection networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            
            if (!networkConnection.IsConnected)
            {
                Device.BeginInvokeOnMainThread(async() =>
                {
                    if (hasInternet)
                    {
                        if (!noInterShow)
                        {
                            hasInternet = false;
                            labelScreen.IsVisible = true;
                            await ShowDisplayAlertAsync();
                        }
                    }
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    hasInternet = true;
                    labelScreen.IsVisible = false;
                });
            }
        }

        public static async Task<bool> CheckIfInternetAsync()
        {
            INetworkConnection networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            return networkConnection.IsConnected;
        }

        public static async Task<bool> CheckIfInternetAlertAsync()
        {
            INetworkConnection networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();

            if (!networkConnection.IsConnected)
            {
                if (!noInterShow)
                {
                    await ShowDisplayAlertAsync();
                }
                return false;
            }
            return true;
        }

        private static async Task ShowDisplayAlertAsync()
        {
            noInterShow = false;
            await currentPage.DisplayAlert("Internet", "Device has no internet, please reconnect", "Oke");
            noInterShow = false;
        }
    }
}
