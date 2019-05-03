using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Xappy.Domain.Global
{
    public enum XappyTheme
    {
        Default,
        Clancey
    }

    public class AppTheme : INotifyPropertyChanged
    {
        public bool UseFlyout { get; set; }

        public bool UseTabs
        {
            get
            {
                return !UseFlyout;
            }
        }

        public XappyTheme CurrentTheme { get; set; } = XappyTheme.Default;

        public AppTheme()
        {
        }

        public void InitTheme()
        {
            Application.Current.Resources["pageBackgroundColor"] = Color.White; //Application.Current.Resources["black"];
        }

        public void SetTheme(XappyTheme theme)
        {

            switch (theme)
            {
                case XappyTheme.Clancey:
                    Application.Current.Resources["pageBackgroundColor"] = LookupColor("almost_black");
                    Application.Current.Resources["flyoutGradientStart"] = LookupColor("dark_peach");
                    Application.Current.Resources["flyoutGradientEnd"] = LookupColor("light_peach");
                    break;
                default:
                    Application.Current.Resources["pageBackgroundColor"] = LookupColor("white");
                    Application.Current.Resources["flyoutGradientStart"] = LookupColor("cerulean_three");
                    Application.Current.Resources["flyoutGradientEnd"] = LookupColor("teal");
                    break;
            }

        }

        public Color LookupColor(string key)
        {
            Application.Current.Resources.TryGetValue(key, out var newColor);
            return (Color)newColor;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetAndRaisePropertyChanged<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }
    }
}
