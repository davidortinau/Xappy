using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Xappy.Content.ControlGallery.ProppyControls
{
    public class ColorPickerViewModel : INotifyPropertyChanged
    {
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

        List<string> _colors;
        public List<string> Colors { get { return _colors; } }

        public List<ColorViewModel> ColorViewModels
        {
            get
            {
                return _colors.Select(x => new ColorViewModel { Name = x }).ToList<ColorViewModel>();
            }
        }

        public ColorPickerViewModel()
        {
            _colors = new List<string>();
            foreach (var field in typeof(Xamarin.Forms.Color).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (field != null && !String.IsNullOrEmpty(field.Name))
                    _colors.Add(field.Name);
            }
        }
    }
}
