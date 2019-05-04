using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xappy.Content.Blog;
using Xappy.Content.Scenarios.Conversation;
using Xappy.ControlGallery;
using Xappy.Scenarios;

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
            Routing.RegisterRoute("map", typeof(MapPage));
            Routing.RegisterRoute("login", typeof(LoginPage));
            //Routing.RegisterRoute("todo", typeof(Todo));
            Routing.RegisterRoute("conversation", typeof(ConversationPage));
            Routing.RegisterRoute("blog", typeof(BlogPage));
            Routing.RegisterRoute("blogDetail", typeof(BlogDetailPage));
        }
    }
}
