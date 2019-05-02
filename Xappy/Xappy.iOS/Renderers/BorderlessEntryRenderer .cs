using System.ComponentModel;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using Xappy.CustomViews;
using Xappy.iOS.Renderers;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace Xappy.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Control == null)
            {
                return;
            }

            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}