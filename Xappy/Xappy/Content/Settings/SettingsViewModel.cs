using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Domain.Global;
using Xappy.Services;
using Xappy.Styles;

namespace Xappy.Content.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public ICommand ChangeThemeCommand { get; set; }

        public ICommand ChangeNavigationCommand { get; set; }

        public string SelectedTheme { get; set; }

        public SettingsViewModel()
        {
            ChangeThemeCommand = new Command((x) =>
            {
                if (SelectedTheme.ToLower() == "dark")
                {
                    Application.Current.Resources = new DarkTheme();
                }
                else
                {
                    Application.Current.Resources = new LightTheme();
                }

                App.AppTheme = SelectedTheme.ToLower();
            });

            ChangeNavigationCommand = new Command<string>((nav) =>
            {
                var am = DependencyService.Resolve<AppModel>();
                if(nav == "flyout")
                {
                    am.NavigationStyle = NavigationStyle.Flyout;
                }
                else
                {
                    am.NavigationStyle = NavigationStyle.Tabs;
                }

                OnPropertyChanged(nameof(UseFlyout));
                OnPropertyChanged(nameof(UseTabs));

                Shell.Current.Navigation.PopModalAsync();
            });
        }

        private bool isVisualMaterial;

        public bool IsVisualMaterial
        {
            get => isVisualMaterial;
            set
            {
                SetProperty(ref isVisualMaterial, value);
                isVisualDefault = !IsVisualMaterial;
                OnPropertyChanged(nameof(IsVisualDefault));
            }
        }
        public bool IsVisualDefault
        {
            get => isVisualDefault;
            set
            {
                SetProperty(ref isVisualDefault, value);
                isVisualMaterial = !isVisualDefault;
                OnPropertyChanged(nameof(IsVisualMaterial));
            }
        }

        public bool UseDarkMode
        {
            get => DependencyService.Get<AppModel>().UseDarkMode;
            set
            {
                DependencyService.Get<AppModel>().UseDarkMode = value;
                OnPropertyChanged(nameof(UseDarkMode));

                if(UseDarkMode && App.AppTheme != "dark")
                {
                    App.Current.Resources = new DarkTheme();
                    App.AppTheme = "dark";
                }else if(!UseDarkMode && App.AppTheme == "dark")
                {
                    App.Current.Resources = new LightTheme();
                    App.AppTheme = "light";
                }
            }
        }

        public bool UseDeviceThemeSettings
        {
            get => DependencyService.Get<AppModel>().UseDeviceThemeSettings;
            set
            {
                DependencyService.Get<AppModel>().UseDeviceThemeSettings = value;
                OnPropertyChanged(nameof(UseDeviceThemeSettings));
            }
        }

        public int UseFlyout
        {
            get { return (AppModel.NavigationStyle == Domain.Global.NavigationStyle.Flyout) ? 1 : 0; }
        }

        public int UseTabs
        {
            get { return (AppModel.NavigationStyle == Domain.Global.NavigationStyle.Tabs) ? 1 : 0; }
        }

        private bool isVisualDefault;
    }
}
