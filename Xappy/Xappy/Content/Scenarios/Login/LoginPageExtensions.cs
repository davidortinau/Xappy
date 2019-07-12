using CSharpForMarkup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.Login
{
    public static class LoginPageExtensions
    {
        // DSL-style helpers for the signup controls
        public static Entry Entry(string placeholder, string bindTo, bool isPassword = false) =>
            new Xamarin.Forms.Entry { Placeholder = placeholder, IsPassword = isPassword }
            .Bind (bindTo);

        public static Button Button(string text, Command command) =>
            new Xamarin.Forms.Button { Text = text, Command = command }
            .Bind (Xamarin.Forms.Button.IsEnabledProperty, nameof(Page.IsBusy), converter: BoolNotConverter.Instance);

        public static Label Validation(string bindTo) =>
            new Label { TextColor = Color.Red } .FontSize (12)
            .Bind (bindTo)
            .Bind (Xamarin.Forms.Label.IsVisibleProperty, bindTo, converter: new FuncConverter<string, bool>(x => !String.IsNullOrWhiteSpace(x)));

        public static Label Link(string before, string link, string bindTo) =>
            new Label { } .FontSize (18) .FormattedText (
                new Span { Text = before },
                new Span { Text = link, TextColor = Color.Blue, TextDecorations = TextDecorations.Underline }
                .BindTap (bindTo)
            ) .CenterH () .Margin (12);

        public static StackLayout Stack(params View[] args) =>
            new StackLayout { }
            .Invoke (sl => args.ToList().ForEach(sl.Children.Add));

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
