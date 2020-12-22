using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xappy.Content.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutFooterTemplate : Grid
    {
        public FlyoutFooterTemplate()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("settings");
        }
    }
}