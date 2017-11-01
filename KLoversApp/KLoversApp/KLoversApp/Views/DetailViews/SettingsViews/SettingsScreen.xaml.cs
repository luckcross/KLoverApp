using KLoversApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KLoversApp.Views.DetailViews.SettingsViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsScreen : ContentPage
    {
        private Settings settings;
        private User currentUser;

        private SwitchCell switchCell1;
        private SwitchCell switchCell2;

        public SettingsScreen()
        {
            InitializeComponent();
            BackgroundColor = Constants.BackgroundColor;
            LoadSettings();
            App.StartCheckIfInternet(lblNoInternet, this);

            Title = Constants.SettingsScreenTitle;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.StartCheckIfInternet(lblNoInternet, this);
        }

        private void LoadSettings()
        {
            settings = App.SettingsDatabase.GetSettings();
            //currentUser = App.UserDatabase.GetUser();
            
            switchCell1 = new SwitchCell
            {
                Text = "SwitchCell 1",
                On = settings.Switch1
            };
            switchCell1.OnChanged += (object sender, ToggledEventArgs e) =>
            {
                SwitchCell1Switched(sender, e);
            };

            switchCell2 = new SwitchCell
            {
                Text = "SwitchCell 2",
                On = settings.Switch2
            };
            switchCell2.OnChanged += (object sender, ToggledEventArgs e) =>
            {
                SwitchCell2Switched(sender, e);
            };

            TableView table = new TableView
            {
                Root = new TableRoot
                {
                    new TableSection
                    {
                        switchCell1,
                        switchCell2
                    }
                }
            };

            table.VerticalOptions = LayoutOptions.FillAndExpand;

            mainLayout.Children.Add(table);
        }
       
        private void SwitchCell1Switched(object sender, ToggledEventArgs e)
        {
            settings.Switch1 = e.Value;
        }

        private void SwitchCell2Switched(object sender, ToggledEventArgs e)
        {
            settings.Switch2 = e.Value;
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            bool action = await DisplayAlert("Settings", "Do you want to save the settings?", "Oke", "Cancel");
            if (action)
                SaveSettings();
        }

        private void SaveSettings()
        {
            App.SettingsDatabase.SaveSettings(settings);
        }
    }
}