using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xappy.Content;

namespace Xappy.Domain.Global
{
    public class AppModel : INotifyPropertyChanged
    {
        public AppModel()
        {
        }

        public NavigationStyle NavigationStyle
        {
            get => navigationStyle;
            set
            {
                navigationStyle = value;
                IsTabBarVisible = (navigationStyle == NavigationStyle.Tabs);
                Shell.Current.FlyoutBehavior = IsTabBarVisible ? FlyoutBehavior.Disabled : FlyoutBehavior.Flyout;
            }
        }
        protected bool isTabBarVisible;
        private NavigationStyle navigationStyle = NavigationStyle.Flyout;

        public bool IsTabBarVisible
        {
            get
            {
                return isTabBarVisible;
            }

            set
            {
                SetProperty(ref isTabBarVisible, value);
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }

    public enum NavigationStyle
    {
        Flyout,
        Tabs
    }
}
