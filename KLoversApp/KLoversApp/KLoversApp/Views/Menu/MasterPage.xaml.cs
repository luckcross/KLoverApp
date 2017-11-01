using KLoversApp.Models;
using KLoversApp.Views.DetailViews;
using KLoversApp.Views.DetailViews.SettingsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KLoversApp.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView;} }
        public List<MasterMenuItem> items;

        public MasterPage()
        {
            InitializeComponent();
            SetItems();
        }

        private void SetItems()
        {
            items = new List<MasterMenuItem>();
            items.Add(new MasterMenuItem("Info Screen1", "icon.png", Color.White, typeof(InfoScreen1)));
            items.Add(new MasterMenuItem("Info Screen2", "icon.png", Color.White, typeof(InfoScreen2)));
            items.Add(new MasterMenuItem("Settings", "icon.png", Color.White, typeof(SettingsScreen)));
            ListView.ItemsSource = items;
        }
    }
}