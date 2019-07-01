using ImageCircle.Forms.Plugin.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.Login
{
    // Animation logic
    public partial class LoginPage : ContentPage
    {
        private Grid MainGrid;
        private StackLayout LoginControls, SignupControls;
        private CircleImage AvatarImage;

        public LoginViewModel ViewModel = new LoginViewModel();

        public LoginPage()
        {
            // wire up viewmodel
            BindingContext = ViewModel;

            // set content
            Visual = VisualMarker.Material;
            Shell.SetTabBarIsVisible(this, false);
            Build();
            LoginControls.IsVisible = ViewModel.Mode == LoginViewModel.Modes.Login;
            SignupControls.IsVisible = ViewModel.Mode == LoginViewModel.Modes.Signup;

            // prepare for entrance animation
            MainGrid.Opacity = 0;
            MainGrid.TranslationY = 50;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            // perform entrance animation
            MainGrid.FadeTo(1);
            MainGrid.TranslateTo(0, 0);
            MainGrid.Children.StaggerIn(50, 0.05);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Mode))
                await TransitionToMode(ViewModel.Mode);
        }

        // performs an animated transition between the signup and login controls, or vice-versa
        private async Task TransitionToMode(LoginViewModel.Modes mode)
        {
            var outgoingControls = mode == LoginViewModel.Modes.Login
                ? SignupControls
                : LoginControls;

            var incomingControls = mode == LoginViewModel.Modes.Signup
                ? SignupControls
                : LoginControls;

            await Task.WhenAll(AvatarImage.FadeTo(0), outgoingControls.FadeTo(0));

            outgoingControls.IsVisible = false;
            incomingControls.Opacity = 0;
            incomingControls.IsVisible = true;

            Title = ViewModel.TitleForMode;

            await Task.WhenAll(AvatarImage.FadeTo(1), incomingControls.FadeTo(1));
        }
    }
}
