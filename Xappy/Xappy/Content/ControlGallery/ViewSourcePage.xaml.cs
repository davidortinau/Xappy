using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Xappy.Content.ControlGallery
{
    public partial class ViewSourcePage : ContentPage
    {

        public string Source
        {
            set
            {
                //string text = GetXamlForType(value);
                if (!string.IsNullOrEmpty(value))
                {
                    SourceLabel.Text = value;
                }
                else
                {
                    SourceLabel.Text = "Can't find XAML for this page.";
                }
            }
        }

        public ViewSourcePage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
