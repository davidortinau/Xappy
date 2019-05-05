using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xappy.About.ViewModels;

namespace Xappy.About
{
    public partial class IndexPage : ContentPage
    {
        public IndexPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if(sender is Button button)
                ((IndexViewModel)this.BindingContext).HeaderSelectedCommand.Execute(button.CommandParameter);
        }
    }
}
