using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpForMarkup;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using Xappy.Domain.Global;

namespace Xappy.Content.Scenarios.Login
{
    [Route(Path = "login")]
    public class LoginPage : ContentPage
    {
        private Grid MainGrid;
        private StackLayout LoginControls, SignupControls;
        private CircleImage AvatarImage;
        private Label ToggleModeLabel;

        public LoginViewModel ViewModel = new LoginViewModel();

        public LoginPage()
        {
            // wire up viewmodel
            BindingContext = ViewModel;

            // set content
            Visual = VisualMarker.Material;
            Content = GetContent();

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

        private View GetContent() => new ScrollView
        {
            Content = new Grid
            {
                RowDefinitions = new Dictionary<LoginRow, GridLength>
                {
                    [LoginRow.Avatar] = 140,
                    [LoginRow.Controls] = GridLength.Auto,
                    [LoginRow.ToggleMode] = GridLength.Auto,
                }
                .ToRowDefinitions(),

                Children =
                {
                    new CircleImage { }
                        .Center() .Size(140)
                        .Assign(out AvatarImage)
                        .Row(LoginRow.Avatar)
                        .Bind(nameof(ViewModel.AvatarUri)),

                    LoginControls_Declarative()
                        .Assign(out LoginControls)
                        .Row(LoginRow.Controls),

                    SignupControls_DSL()
                        .Assign(out SignupControls)
                        .Row(LoginRow.Controls),

                    new Label { Text = ViewModel.TextForToggleTitle }
                        .Center() .FontSize(14)
                        .Margin(12)
                        .Row(LoginRow.ToggleMode)
                        .Assign(out ToggleModeLabel)
                        .BindTapGesture(nameof(ViewModel.ToggleModeCommand)),

                    new ActivityIndicator { }
                        .Bind(nameof(ViewModel.IsBusy)),
                }
            }
            .Center()
            .Margin(20)
            .Assign(out MainGrid)
        };

        // returns the controls for the login form in a declarative, CSharpForMarkup manner
        // this style balances conciseness, consistency and reusability
        private StackLayout LoginControls_Declarative() => new StackLayout
        {
            Children =
            {
                new Entry { Placeholder = "Username" }
                    .Bind(nameof(ViewModel.Username)),

                new Entry { Placeholder = "Password", IsPassword = true }
                    .Bind(nameof(ViewModel.Password)),

                new Button { Text = "Log in", Command = ViewModel.LoginCommand }
                    .Bind(sourcePropertyName: nameof(IsBusy),
                            targetProperty: IsEnabledProperty,
                            converter: BoolNotConverter.Instance),
            }
        }
        .Invoke(x => x.IsVisible = ViewModel.Mode == LoginScreenMode.Login);

        // returns ths controls for the login form in using a dedicated DSL syntax for the page
        // this format is concise and readable, but potentially specific to the page
        private StackLayout SignupControls_DSL()
            => Stack(
                Entry("Username", nameof(ViewModel.Username)),
                Validation(nameof(ViewModel.UsernameValidation)),

                Entry("Email Address", nameof(ViewModel.EmailAddress)),
                Validation(nameof(ViewModel.EmailAddressValidation)),

                Entry("Password", nameof(ViewModel.Password), true),
                Validation(nameof(ViewModel.PasswordValidation)),

                Entry("Confirm Password", nameof(ViewModel.ConfirmPassword), true),
                Validation(nameof(ViewModel.ConfirmPasswordValidation)),

                Button("Sign up", ViewModel.SignupCommand)
            )
            .Invoke(x => x.IsVisible = ViewModel.Mode == LoginScreenMode.Signup);

        private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Mode))
                await TransitionToMode(ViewModel.Mode);
        }

        // performs an animated transition between the signup and login controls, or vice-versa
        private async Task TransitionToMode(LoginScreenMode mode)
        {
            var outgoingControls = mode == LoginScreenMode.Login
                ? SignupControls
                : LoginControls;

            var incomingControls = mode == LoginScreenMode.Signup
                ? SignupControls
                : LoginControls;

            await Task.WhenAll(AvatarImage.FadeTo(0), outgoingControls.FadeTo(0));

            outgoingControls.IsVisible = false;
            incomingControls.Opacity = 0;
            incomingControls.IsVisible = true;

            Title = ViewModel.TitleForMode;
            ToggleModeLabel.Text = ViewModel.TextForToggleTitle;

            await Task.WhenAll(AvatarImage.FadeTo(1), incomingControls.FadeTo(1));
        }

        // DSL-style helpers for the signup controls
        public Entry Entry(string placeholder, string bindTo, bool isPassword = false)
            => new Xamarin.Forms.Entry { Placeholder = placeholder, IsPassword = isPassword }
                .Bind(bindTo);

        public Button Button(string text, Command command)
            => new Xamarin.Forms.Button { Text = text, Command = command }
                .Bind(sourcePropertyName: nameof(IsBusy),
                        targetProperty: IsEnabledProperty,
                        converter: BoolNotConverter.Instance);

        public Label Validation(string bindTo)
            => new Label { TextColor = Color.Red }
            .FontSize(12)
            .Bind(bindTo)
            .Bind(sourcePropertyName: bindTo, targetProperty: IsVisibleProperty,
                converter: new FuncConverter<string, bool>(x => !String.IsNullOrWhiteSpace(x)));

        public StackLayout Stack(params View[] args)
             => new StackLayout { }
                    .Invoke(sl => args.ToList().ForEach(sl.Children.Add));        
    }
}
