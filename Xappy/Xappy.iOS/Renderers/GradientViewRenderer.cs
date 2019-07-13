using System;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xappy.Content.Scenarios.OtherLogin.Controls;
using Xappy.iOS.Renderers;
using CoreAnimation;
using System.ComponentModel;
using System.Linq;

[assembly: ExportRenderer(typeof(GradientView), typeof(GradientViewRenderer))]

namespace Xappy.iOS.Renderers
{
    public class GradientViewRenderer : VisualElementRenderer<GradientView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<GradientView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
                SetNeedsDisplay();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            SetNeedsDisplay();
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (Element == null) return;

            var gradientLayer = new CAGradientLayer();
            gradientLayer.Colors = new CGColor[]
            {
                Element.StartColor.ToCGColor(),
                Element.EndColor.ToCGColor()
            };

            if (Element.Direction == GradientDirection.Vertical)
            {
                gradientLayer.StartPoint = new CGPoint(0.5, 0);
                gradientLayer.EndPoint = new CGPoint(0.5, 1);
            }
            else
            {
                gradientLayer.StartPoint = new CGPoint(0, 0.5);
                gradientLayer.EndPoint = new CGPoint(1, 0.5);
            }

            gradientLayer.Frame = rect;
            if (NativeView.Layer.Sublayers != null)
            {
                foreach (var sublayer in NativeView.Layer.Sublayers)
                {
                    sublayer.RemoveFromSuperLayer();
                }
            }

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}
