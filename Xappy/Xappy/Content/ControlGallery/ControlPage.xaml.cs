using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xappy.ControlGallery
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class ControlPage : ContentPage
    {
        public ControlPage()
        {
            InitializeComponent();

            BindingContext = new ControlPageViewModel();
        }


    }
}
