using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.Login
{
    public class LoginViewModel : BaseViewModel
    {
        public enum Modes { Login, Signup }

        const int MinimumUsernameLength = 5;
        const int MinimumPasswordLength = 8;

        // thanks stack overflow 👀
        private Regex emailValidationRegex =
            new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled);

        public Uri AvatarUri => new Uri("https://devblogs.microsoft.com/xamarin/wp-content/uploads/sites/44/2019/03/Screen-Shot-2017-01-03-at-3.35.53-PM.png");

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string emailAddress;
        public string EmailAddress
        {
            get => emailAddress;
            set => SetProperty(ref emailAddress, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        private string usernameValidation;
        public string UsernameValidation
        {
            get => usernameValidation;
            set => SetProperty(ref usernameValidation, value);
        }

        private string emailAddressValidation;
        public string EmailAddressValidation
        {
            get => emailAddressValidation;
            set => SetProperty(ref emailAddressValidation, value);
        }

        private string passwordValidation;
        public string PasswordValidation
        {
            get => passwordValidation;
            set => SetProperty(ref passwordValidation, value);
        }

        private string confirmPasswordValidation;
        public string ConfirmPasswordValidation
        {
            get => confirmPasswordValidation;
            set => SetProperty(ref confirmPasswordValidation, value);
        }

        private Modes mode;
        public Modes Mode
        {
            get => mode;
            set => SetProperty(ref mode, value);
        }

        public Command LoginCommand { get; private set; }
        public Command SignupCommand { get; private set; }
        public Command ToggleModeCommand { get; private set; }

        public LoginViewModel()
        {
            PropertyChanged += LoginViewModel_PropertyChanged;

            LoginCommand = new Command(() => Login());
            SignupCommand = new Command(() => Signup(), SignupCanExecute);
            ToggleModeCommand = new Command(ToggleMode);
        }

        private async Task Login()
        {
            IsBusy = true;
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsBusy = false;
        }

        private Task Signup() => Login();

        private void ToggleMode()
        {
            Mode = Mode == Modes.Login
                ? Modes.Signup
                : Modes.Login;
        }

        // implements a basic validation routine in which the presence of a validation message implies invalid state
        private void PerformValidation()
        {
            UsernameValidation = String.IsNullOrWhiteSpace(Username) || Username?.Length >= MinimumUsernameLength
                ? ""
                : $"Username must be at least {MinimumUsernameLength} characters long";

            EmailAddressValidation = String.IsNullOrWhiteSpace(EmailAddress) || (IsValidEmailAddress(EmailAddress))
                ? ""
                : "Email address is not valid";

            PasswordValidation = String.IsNullOrWhiteSpace(Password) || Password?.Length >= MinimumPasswordLength
                ? ""
                : $"Password must be at least {MinimumPasswordLength} characters long";

            ConfirmPasswordValidation = String.IsNullOrWhiteSpace(ConfirmPassword) || (Password == ConfirmPassword)
                ? ""
                : $"Passwords do not match";

            SignupCommand.ChangeCanExecute();
        }

        // execution is allowed if there are no validation messages and no incomplete fields
        private bool SignupCanExecute()
        {
            var anyValidationErrors =
                new[] { UsernameValidation, EmailAddressValidation, PasswordValidation, ConfirmPasswordValidation }
                    .Any(x => !String.IsNullOrWhiteSpace(x));

            var anyMissingFields =
                new[] { Username, EmailAddress, Password, ConfirmPassword }
                    .Any(String.IsNullOrWhiteSpace);

            return !anyValidationErrors && !anyMissingFields;
        }

        public string TitleForMode =>
            Mode == Modes.Login
                ? "Login"
                : "Sign up";

        private bool IsValidEmailAddress(string input)
            => emailValidationRegex.IsMatch(input);

        private void LoginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Debug.WriteLine(e.PropertyName);
            PerformValidation();
        }
    }
}
