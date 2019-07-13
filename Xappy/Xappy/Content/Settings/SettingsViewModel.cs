using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Styles;

namespace Xappy.Content.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public ICommand ChangeThemeCommand { get; set; }

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
                    Application.Current.Resources = new WhiteTheme();
                }
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

        private bool isVisualDefault;
    }
}
