using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xappy.Domain.Global;

[assembly: ExportRenderer(typeof(Shell), typeof(Xappy.iOS.Renderers.XappyShellRenderer))]
namespace Xappy.iOS.Renderers
{
    public class XappyShellRenderer : ShellRenderer
    {
        private CAGradientLayer _flyoutBackground = null;

        protected override void OnElementSet(Shell element)
        {
            base.OnElementSet(element);
        }

        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);
            if (renderer != null)
            {
                (renderer as ShellSectionRenderer).NavigationBar.SetBackgroundImage(new UIImage(),
                    UIBarMetrics.Default);
                (renderer as ShellSectionRenderer).NavigationBar.ShadowImage = new UIImage();

                //UINavigationBar.Appearance.BarTintColor = Color.FromHex("#11313F").ToUIColor(); //bar background
                //UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items
                //UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
                //{
                //    Font = UIFont.FromName("HelveticaNeue-Light", (nfloat)20f),
                //    TextColor = UIColor.White
                //});
            }

            return (IShellSectionRenderer)renderer;
        }

        protected override IShellFlyoutContentRenderer CreateShellFlyoutContentRenderer()
        {

            var flyout = base.CreateShellFlyoutContentRenderer();
            flyout.WillAppear += OnFlyoutWillAppear;

            var tv = (UITableView)flyout.ViewController.View.Subviews[0];
            tv.ScrollEnabled = false;

            var tvs = (ShellTableViewSource)tv.Source;
            tvs.Groups.RemoveAt(1); // this is a total hack to hide the separator that appears when there are multiple groups

            return flyout;
        }

        /// <summary>
        /// Only grab the bounds of the View when View rendering calculations are already done
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnFlyoutWillAppear(object sender, EventArgs e)
        {
            if (_flyoutBackground == null && sender != null && sender is IShellFlyoutContentRenderer flyout)
            {

                var theme = DependencyService.Get<AppTheme>();

                var v = flyout.ViewController.View;

                _flyoutBackground = new CAGradientLayer();
                _flyoutBackground.Frame = new CGRect(0, 0, v.Bounds.Width, v.Bounds.Height); ;
                _flyoutBackground.Colors = new CoreGraphics.CGColor[]
                {
                    ((Color)theme.LookupColor("flyoutGradientStart")).ToCGColor(),
                    ((Color)theme.LookupColor("flyoutGradientEnd")).ToCGColor()
                };

                flyout.ViewController.View.Layer.InsertSublayer(_flyoutBackground, 0);

                flyout.WillAppear -= OnFlyoutWillAppear;
            }
        }
    }
}