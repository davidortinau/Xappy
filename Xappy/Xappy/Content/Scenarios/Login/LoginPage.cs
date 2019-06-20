using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CSharpForMarkup;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using Xappy.Domain.Global;

namespace Xappy.Content.Scenarios.Login
{
    [Route(Path ="login")]
    public class LoginPage : ContentPage
    {
        public enum Row
        {
            Avatar,
            Controls,
            ToggleMode,
        }

        private Uri _avatarUri = new Uri("https://devblogs.microsoft.com/xamarin/wp-content/uploads/sites/44/2019/03/Screen-Shot-2017-01-03-at-3.35.53-PM.png"); 

        private Grid MainGrid;
        private StackLayout LoginControls;
        private StackLayout SignupControls;
        private CircleImage AvatarImage;
        private Label ToggleModeLabel;

        public LoginViewModel ViewModel = new LoginViewModel();

        public LoginPage()
        {
            BindingContext = ViewModel; 
            Visual = VisualMarker.Material; 

            Content = GetContent();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(ViewModel.Mode):
                    TransitionToMode(ViewModel.Mode);

                    break;

                default:
                    return;
            }
        }

        private async Task TransitionToMode(LoginViewModel.ScreenMode mode)
        {
            var outgoingControls = mode == LoginViewModel.ScreenMode.Login
                ? SignupControls
                : LoginControls;

            var incomingControls = mode == LoginViewModel.ScreenMode.Signup
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MainGrid.Opacity = 0;
            MainGrid.TranslationY = 50;

            MainGrid.FadeTo(1);
            MainGrid.TranslateTo(0,0);

            var i = 0;
            foreach (var v in MainGrid.Children)
            {
                v.Opacity = 0;
                v.TranslationY = 50;

                Task.Delay(TimeSpan.FromSeconds(i++ * .05))
                    .ContinueWith(_ =>
                    {
                        v.FadeTo(1);
                        v.TranslateTo(0, 0, easing: Easing.CubicOut);   
                    }); 
            }  
        }

        private View GetContent()
            => new Grid
            {
                RowDefinitions = new Dictionary<Row, GridLength>
                {
                    [Row.Avatar] = 140,
                    [Row.Controls] = GridLength.Auto,
                    [Row.ToggleMode] = GridLength.Auto,
                }
                .ToRowDefinitions(),

                Children =
                { 
                    new CircleImage { Source = _avatarUri }
                        .Center() .Size(140)
                        .Assign(out AvatarImage)
                        .Row(Row.Avatar),

                    new StackLayout
                    {
                        IsVisible = ViewModel.Mode == LoginViewModel.ScreenMode.Login,
                        Children =
                        {
                            new Entry { Placeholder = "Username" }
                                .Bind(nameof(ViewModel.Username)),

                            new Entry { Placeholder = "Password", IsPassword = true }
                                .Bind(nameof(ViewModel.Password)),

                            new Button { Text = "Log in", Command = ViewModel.LoginCommand }
                                .Bind(sourcePropertyName: nameof(IsBusy),
                                      targetProperty: Button.IsEnabledProperty,
                                      converter: BoolNotConverter.Instance),
                        }
                    }
                    .Assign(out LoginControls)
                    .Row(Row.Controls),

                    new Label { Text = "New User? Sign up" }
                        .Center() .FontSize(14)
                        .Margin(12)
                        .Row(Row.ToggleMode)
                        .Assign(out ToggleModeLabel)
                        .BindTapGesture(nameof(ViewModel.ToggleModeCommand)),

                    new ActivityIndicator { }
                        .Bind(nameof(ViewModel.IsBusy)),

                    MakeStack(ViewModel.Mode == LoginViewModel.ScreenMode.Signup,
                        MakeEntry("Username", nameof(ViewModel.Username)),
                        MakeEntry("Email Address", nameof(ViewModel.Username)),
                        MakeValidation(nameof(ViewModel.Username)),  
                        MakeEntry("Password", nameof(ViewModel.Password), true),
                        MakeEntry("Confirm Password", nameof(ViewModel.Password), true),
                        MakeButton("Sign up", ViewModel.LoginCommand))
                    .Assign(out SignupControls)
                    .Row(Row.Controls),
                }
            }
            .Center()
            .Margin(20) 
            .Assign(out MainGrid);

        public Label MakeValidation(string bindTo) =>
            new Label
            {
                TextColor = Color.Red
            }
            .Bind(bindTo)
            .Bind(sourcePropertyName: bindTo, targetProperty: Label.IsVisibleProperty,
                converter: new FuncConverter<string, bool>(x => !String.IsNullOrWhiteSpace(x))); 

        public Entry MakeEntry(string placeholder, string bindTo, bool isPassword = false)
            => new Entry { Placeholder = placeholder, IsPassword = isPassword }
                .Bind(bindTo);

        public Button MakeButton(string text, Command command)
            => new Button { Text = text, Command = command }
                .Bind(sourcePropertyName: nameof(IsBusy),
                        targetProperty: IsEnabledProperty,
                        converter: BoolNotConverter.Instance);

        public StackLayout MakeStack(bool isVisible, params View[] args)
        {
            var sl = new StackLayout { };
            foreach (var v in args)
                sl.Children.Add(v);

            return sl; 
        }

        public class LoginViewModel : BaseViewModel
        {
            public enum ScreenMode
            {
                Login,
                Signup
            }

            private string username;
            public string Username
            {
                get => username;
                set => SetProperty(ref username, value);
            }

            private string password;
            public string Password
            {
                get => password;
                set => SetProperty(ref password, value);
            }

            private string emailAddressValidation;
            public string EmailAddressValidation
            {
                get => emailAddressValidation;
                set => SetProperty(ref emailAddressValidation, value);
            }

            private ScreenMode mode;
            public ScreenMode Mode
            {
                get => mode;
                set => SetProperty(ref mode, value);
            }

            public Command LoginCommand { get; set; }
            public Command ToggleModeCommand { get; set; }

            public LoginViewModel()
            {
                LoginCommand = new Command(() => Login());
                ToggleModeCommand = new Command(ToggleMode);

                PropertyChanged += LoginViewModel_PropertyChanged;

                Username = "dave";
                Password = "hunter2";

                Mode = ScreenMode.Signup; 
            }

            public async Task Login()
            {
                IsBusy = true;
                Debug.WriteLine("logging in..");  
                await Task.Delay(TimeSpan.FromSeconds(3));

                IsBusy = false;
            }

            public void ToggleMode()
            {
                Mode = Mode == ScreenMode.Login
                    ? ScreenMode.Signup
                    : ScreenMode.Login; 
            }

            private void LoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                Debug.WriteLine(e.PropertyName);

                EmailAddressValidation = Username.Length > 5
                    ? ""
                    : "Email address is not valid.";
            }

            public string TitleForMode =>
                Mode == ScreenMode.Login
                    ? "Login"
                    : "Sign up";

            public string TextForToggleTitle =>
                Mode == ScreenMode.Login
                    ? "New User? Sign up"
                    : "Have an account? Log in";
        }
    }
}
