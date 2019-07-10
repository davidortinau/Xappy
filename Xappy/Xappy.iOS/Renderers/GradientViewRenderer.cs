using System;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xappy.Content.Scenarios.Login.Controls;
using Xappy.iOS.Renderers;
using CoreAnimation;

[assembly: ExportRenderer(typeof(GradientView), typeof(GradientViewRenderer))]

namespace Xappy.iOS.Renderers
{
    public class GradientViewRenderer : VisualElementRenderer<GradientView>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

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
            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}
