using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.OtherLogin
{
    public partial class LoginPage : ContentPage
    {
        private const int AnimationDuration = 200;

        public LoginPage()
        {
            InitializeComponent();

            SizeChanged += LoginPage_SizeChanged;
        }

        private void LoginPage_SizeChanged(object sender, EventArgs e)
        {
            if (SelectorBackground.Height < 0) return;

            SizeChanged -= LoginPage_SizeChanged;

            // Calculate corner radius depending on height
            SelectorBackground.CornerRadius = (float)SelectorBackground.Height / 2f;
            SelectorButton.CornerRadius = (float)SelectorButton.Height / 2f;
        }

        private async void SelectorOption_Tapped(object sender, System.EventArgs e)
        {
            if (!(sender is View view)) return;

            var index = Grid.GetColumn(view);

            SelectorButton.TranslateTo(index * view.Width, 0, AnimationDuration, Easing.CubicInOut).FireAndForget();

            await SelectorButtonLabel.FadeTo(0, AnimationDuration / 2);
            SelectorButtonLabel.Text = index == 1 ? "New" : "Existing";
            await SelectorButtonLabel.FadeTo(1, AnimationDuration / 2);

            var revealForm = index == 0 ? LoginForm : SignupForm;
            var hideForm = revealForm == LoginForm ? SignupForm : LoginForm;
            var direction = revealForm == LoginForm ? 1 : -1;

            await Task.WhenAll(
                hideForm.TranslateTo(direction * 200, 0, AnimationDuration, Easing.SinOut),
                hideForm.FadeTo(0, AnimationDuration));

            hideForm.IsVisible = false;

            revealForm.TranslationX = -direction * 200;
            revealForm.IsVisible = true;

            await Task.WhenAll(
                revealForm.TranslateTo(0, 0, AnimationDuration, Easing.SinOut),
                revealForm.FadeTo(1, AnimationDuration));

        }
    }
}
