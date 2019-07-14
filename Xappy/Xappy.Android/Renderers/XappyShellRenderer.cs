using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xappy.Domain.Global;

[assembly: ExportRenderer(typeof(Shell), typeof(Xappy.Droid.Renderers.XappyShellRenderer))]
namespace Xappy.Droid.Renderers
{
    public class XappyShellRenderer : ShellRenderer
    {
        public XappyShellRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementSet(Shell element)
        {
            base.OnElementSet(element);
        }

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem)
        {
            var renderer = base.CreateShellItemRenderer(shellItem);


            return renderer;
        }

        protected override IShellFlyoutRenderer CreateShellFlyoutRenderer()
        {
            var flyout = base.CreateShellFlyoutRenderer();

            return flyout;
        }

        protected override IShellFlyoutContentRenderer CreateShellFlyoutContentRenderer()
        {
            var flyout = base.CreateShellFlyoutContentRenderer();

            GradientDrawable gradient = new GradientDrawable(
                GradientDrawable.Orientation.BottomTop,
                new Int32[] {
                    ((Color)App.LookupColor("flyoutGradientEnd")).ToAndroid(),
                    ((Color)App.LookupColor("flyoutGradientStart")).ToAndroid()
                }
            );
         
            var cl = ((CoordinatorLayout)flyout.AndroidView);
            cl.SetBackground(gradient);

            var g = (AppBarLayout)cl.GetChildAt(0);
            g.SetBackgroundColor(Color.Transparent.ToAndroid());
            g.OutlineProvider = null;

            var header = g.GetChildAt(0);
            header.SetBackgroundColor(Color.Transparent.ToAndroid());

            return flyout;
        }

    }
}