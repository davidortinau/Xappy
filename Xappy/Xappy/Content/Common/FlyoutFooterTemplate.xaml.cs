using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xappy.Content.Settings;

namespace Xappy.Content.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutFooterTemplate : Grid
    {
        public FlyoutFooterTemplate()
        {
            InitializeComponent();
        }
        
        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("settings", true);
        }
    }
}