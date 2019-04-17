using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Xappy.ControlGallery
{
    public partial class IndexPage : ContentPage
    {
        public IndexPage()
        {
            InitializeComponent();

            BindingContext = new IndexPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //LayoutsCV.SelectedItem = null;
        }
    }
}
