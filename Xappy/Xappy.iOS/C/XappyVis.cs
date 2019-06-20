using System;
using System.Diagnostics;
using System.Linq;
using Continuous;
using Continuous.Server;
using Xamarin.Forms;
using Xappy.Domain.Global;

namespace Xappy.iOS
{
    public class XappyVis : Continuous.Server.Visualizer, INeedToKnowAboutContinuousServer, IApp
    {
        public HttpServer ContinuousServer { get; set; }
        public App App { get; set; }
        public AppShell AppShell => App.MainPage as AppShell;

        public override void StopVisualizing()
        {
            try { base.StopVisualizing(); } catch { }

            Xamarin.Forms.Application.Current = App;
        }

        public override void Visualize(EvalResult res)
        {
            if (ReloadVisualizer(res))
                return;

            Debug.WriteLine($"Primary type is {res.PrimaryType}");

            var routeables =
                res.NewTypes
                   .Where(t => t.IsSubclassOf(typeof(ContentPage)) && t.GetCustomAttributes(true).Any(x => x is RouteAttribute));

            foreach (var type in routeables)
            {
                var routeAtt = type.GetCustomAttributes(false).OfType<RouteAttribute>().First();
                var route = routeAtt.Path; 

                var reloadRoute = $"//{Guid.NewGuid().ToString()}";

                Routing.RegisterRoute(route, type);
                Routing.RegisterRoute(reloadRoute, type);

                AppShell.GoToAsync(reloadRoute, false);

                return; 
            }

            // don't visualise ourselves
            if (!res.PrimaryType.Name.Contains(nameof(XappyVis)))
                base.Visualize(res);
        }

        private bool ReloadVisualizer(EvalResult res)
        {
            var newVisType =
                    res.NewTypes
                       .FirstOrDefault(x => x.IsSubclassOf(typeof(Visualizer)) && x != this.GetType());

            if (newVisType == null)
                return false;

            var newVis = (Visualizer)Activator.CreateInstance(newVisType);

            if (newVis is INeedToKnowAboutContinuousServer ntkacs)
                ntkacs.ContinuousServer = ContinuousServer;

            if (newVis is IApp app)
                app.App = App;

            ContinuousServer.ReplaceVisualizer(newVis);

            Console.WriteLine("Delegating visualization to updated visualiser");
            newVis.Visualize(res);

            return true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
    }
}