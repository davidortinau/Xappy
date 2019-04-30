using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Shell), typeof(Xappy.iOS.Renderers.XappyShellRenderer))]
namespace Xappy.iOS.Renderers
{
    public class XappyShellRenderer : ShellRenderer
    {
        protected override void OnElementSet(Shell element)
        {
            base.OnElementSet(element);
        }

        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);
            //if (renderer != null)
            //{
            //    (renderer as ShellSectionRenderer).NavigationBar.SetBackgroundImage(new UIImage(),
            //        UIBarMetrics.Default);
            //    (renderer as ShellSectionRenderer).NavigationBar.ShadowImage = new UIImage();

            //    UINavigationBar.Appearance.BarTintColor = Color.FromHex("#11313F").ToUIColor(); //bar background
            //    UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items
            //    UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            //    {
            //        Font = UIFont.FromName("HelveticaNeue-Light", (nfloat)20f),
            //        TextColor = UIColor.White
            //    });
            //}

            return (IShellSectionRenderer)renderer;
        }

        protected override IShellFlyoutContentRenderer CreateShellFlyoutContentRenderer()
        {

            var flyout = base.CreateShellFlyoutContentRenderer();

            var v = flyout.ViewController.View;

            var gradient = new CAGradientLayer();
            gradient.Frame = new CGRect(0, 0, 332, v.Bounds.Height); // TODO fix this hardcoded value
            gradient.Colors = new CoreGraphics.CGColor[]
            {
                UIColor.FromRGB(1, 126, 216).CGColor,
                UIColor.FromRGB(20, 217, 217).CGColor
            };

            flyout.ViewController.View.Layer.InsertSublayer(gradient, 0);

            var tv = (UITableView)flyout.ViewController.View.Subviews[0];
            tv.ScrollEnabled = false;

            var tvs = (ShellTableViewSource)tv.Source;
            tvs.Groups.RemoveAt(1); // this is a total hack to hide the separator that appears when there are multiple groups

            return flyout;
        }
    }
}