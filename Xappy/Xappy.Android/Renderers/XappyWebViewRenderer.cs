using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xappy.CustomViews;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(Xappy.Droid.Renderers.XappyWebViewRenderer))]
namespace Xappy.Droid.Renderers
{
    public class XappyWebViewRenderer : WebViewRenderer
    {
        public XappyWebViewRenderer(Context context) : base(context)
        {
        }

        // Workaround for https://github.com/xamarin/Xamarin.Forms/issues/5205
        public override bool DispatchTouchEvent(MotionEvent e)
        {
            Parent.RequestDisallowInterceptTouchEvent(true);
            return base.DispatchTouchEvent(e);
        }
    }
}