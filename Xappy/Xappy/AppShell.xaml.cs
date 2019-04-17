using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xappy.ControlGallery;

namespace Xappy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("control", typeof(ControlPage));
        }
    }
}
