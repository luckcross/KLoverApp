﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KLoversApp.Views.RendererViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginFacebookView : ContentPage
    {
        public LoginFacebookView()
        {
            InitializeComponent();
        }
    }
}