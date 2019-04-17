using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Xappy.ControlGallery
{
    public partial class ControlPage : ContentPage
    {
        public ControlPage()
        {
            InitializeComponent();

            BindingContext = new ControlPageViewModel();
        }


    }
}
