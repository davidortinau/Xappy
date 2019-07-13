using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Xappy.Content.Scenarios.ProductDetails
{
    public partial class ProductDetailsPage : ContentPage
    {
        public ProductDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as ProductDetailsViewModel).SkeletonCommand.Execute(null);
        }
    }
}
