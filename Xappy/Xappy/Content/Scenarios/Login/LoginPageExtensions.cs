using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.Login
{
    public static class LoginPageExtensions
    {
        // performs a staggered upward translation/fade entrance animation,
        // where each subsequent child begins animating delay seconds after the previous 
        public static void StaggerIn(this IEnumerable<View> children, float translation, double delay)
        {
            var i = 0;
            foreach (var v in children)
            {
                v.Opacity = 0;
                v.TranslationY = translation;

                Task.Delay(TimeSpan.FromSeconds(i++ * delay))
                    .ContinueWith(_ =>
                    {
                        v.FadeTo(1);
                        v.TranslateTo(0, 0, easing: Easing.CubicOut);
                    });
            }
        }
    }
}
