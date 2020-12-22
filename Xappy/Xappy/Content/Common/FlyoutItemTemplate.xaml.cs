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
    public partial class FlyoutItemTemplate : Grid
    {
        public FlyoutItemTemplate()
        {
            InitializeComponent();
        }
    }
}