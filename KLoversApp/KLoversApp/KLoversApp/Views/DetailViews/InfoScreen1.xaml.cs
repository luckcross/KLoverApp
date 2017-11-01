using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KLoversApp.Views.DetailViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoScreen1 : ContentPage
    {
        public InfoScreen1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            activitySpinner.IsVisible = true;
        }
    }
}