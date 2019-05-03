using System;
using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xappy.CustomViews;

[assembly: ExportRenderer(typeof(GradientView), typeof(Xappy.Droid.Renderers.GradientViewRenderer))]
namespace Xappy.Droid.Renderers
{
    public class GradientViewRenderer : ViewRenderer
    {
        public GradientViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                GradientDrawable gradient = new GradientDrawable(
                GradientDrawable.Orientation.BottomTop,
                new Int32[] {
                    Color.Black.MultiplyAlpha(0.6).ToAndroid(),
                     Color.Black.MultiplyAlpha(0.1).ToAndroid()
                });
                this.ViewGroup.Background = gradient;
            }
        }
    }
}