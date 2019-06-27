using System;
namespace Xappy.Content.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
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
