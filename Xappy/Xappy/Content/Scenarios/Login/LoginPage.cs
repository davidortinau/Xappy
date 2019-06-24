using CSharpForMarkup;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace Xappy.Content.Scenarios.Login
{
    public partial class LoginPage : ContentPage
    {
        enum Row { Avatar, Controls, ToggleMode }

        void Build() => Content = new ScrollView { Content = 
            new Grid {
                RowDefinitions = Rows.Define (
                    (Row.Avatar    , 140),
                    (Row.Controls  , Auto),
                    (Row.ToggleMode, Auto)
                ),

                Children = {
                    new CircleImage { }
                        .Assign (out AvatarImage)
                        .Row (Row.Avatar) .Center () .Size (140)
                        .Bind (nameof(ViewModel.AvatarUri)),

                    LoginControls_Declarative
                        .Assign (out LoginControls)
                        .Row (Row.Controls),

                    SignupControls_DSL
                        .Assign (out SignupControls)
                        .Row (Row.Controls),

                    new Label { Text = ViewModel.TextForToggleTitle } .FontSize (14)
                        .Assign (out ToggleModeLabel)
                        .Row (Row.ToggleMode) .Center () .Margin (12)
                        .BindTapGesture (nameof(ViewModel.ToggleModeCommand)),

                    new ActivityIndicator { }
                        .Bind (nameof(ViewModel.IsBusy)),
                }
            } .Assign (out MainGrid)
              .Center () .Margin (20)
        };

        // returns the controls for the login form in a declarative, CSharpForMarkup manner
        // this style balances conciseness, consistency and reusability
        StackLayout LoginControls_Declarative => new StackLayout { Children = {
                new Entry { Placeholder = "Username" }
                    .Bind (nameof(ViewModel.Username)),

                new Entry { Placeholder = "Password", IsPassword = true }
                    .Bind (nameof(ViewModel.Password)),

                new Button { Text = "Log in", Command = ViewModel.LoginCommand }
                    .Bind (StackLayout.IsEnabledProperty, nameof(IsBusy), converter: BoolNotConverter.Instance),
        } } .Invoke (loginControls => loginControls.IsVisible = ViewModel.Mode == LoginViewModel.Modes.Login);

        // returns ths controls for the login form in using a dedicated DSL syntax for the page
        // this format is concise and readable, but potentially specific to the page or app
        StackLayout SignupControls_DSL => Stack (
            Entry ("Username", nameof(ViewModel.Username)),
            Validation (nameof(ViewModel.UsernameValidation)),

            Entry ("Email Address", nameof(ViewModel.EmailAddress)),
            Validation (nameof(ViewModel.EmailAddressValidation)),

            Entry ("Password", nameof(ViewModel.Password), true),
            Validation (nameof(ViewModel.PasswordValidation)),

            Entry ("Confirm Password", nameof(ViewModel.ConfirmPassword), true),
            Validation (nameof(ViewModel.ConfirmPasswordValidation)),

            Button ("Sign up", ViewModel.SignupCommand)
        ) .Invoke (signupControls => signupControls.IsVisible = ViewModel.Mode == LoginViewModel.Modes.Signup);
    }
}
