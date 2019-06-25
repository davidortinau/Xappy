using CSharpForMarkup;
using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.Login
{
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
            Build();

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

        // DSL-style helpers for the signup controls
        public Entry Entry(string placeholder, string bindTo, bool isPassword = false) =>
            new Xamarin.Forms.Entry { Placeholder = placeholder, IsPassword = isPassword }
            .Bind (bindTo);

        public Button Button(string text, Command command) =>
            new Xamarin.Forms.Button { Text = text, Command = command }
            .Bind (Xamarin.Forms.Button.IsEnabledProperty, nameof(IsBusy), converter: BoolNotConverter.Instance);

        public Label Validation(string bindTo) =>
            new Label { TextColor = Color.Red } .FontSize (12)
            .Bind (bindTo)
            .Bind (Xamarin.Forms.Label.IsVisibleProperty, bindTo, converter: new FuncConverter<string, bool>(x => !String.IsNullOrWhiteSpace(x)));

        public Label Link(string before, string link, string bindTo) => 
            new Label { } .FontSize (18) .FormattedText (
                new Span { Text = before },
                new Span { Text = link, TextColor = Color.Blue, TextDecorations = TextDecorations.Underline } 
                .BindTap (bindTo)
            ) .CenterH () .Margin (12);

        public StackLayout Stack(params View[] args) => 
            new StackLayout { }
            .Invoke (sl => args.ToList().ForEach(sl.Children.Add));
    }
}
