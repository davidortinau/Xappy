using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xappy.Content.Scenarios.OtherLogin.Controls;
using Xappy.Droid.Renderers;

[assembly: ExportRenderer(typeof(GradientView), typeof(GradientViewRenderer))]
namespace Xappy.Droid.Renderers
{
    public class GradientViewRenderer : VisualElementRenderer<GradientView>
    {
        public GradientViewRenderer(Context context) : base(context)
        {
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            var box = Element;

            var gradient = new LinearGradient(0, 0,
                                              box.Direction == GradientDirection.Horizontal ? Width : 0,
                                              box.Direction == GradientDirection.Vertical ? Height : 0,
                                              box.StartColor.ToAndroid(),
                                              box.EndColor.ToAndroid(),
                                              Shader.TileMode.Mirror);

            var paint = new Paint()
            {
                Dither = true,
            };

            paint.SetShader(gradient);
            canvas.DrawPaint(paint);

            base.DispatchDraw(canvas);
        }
    }
}
