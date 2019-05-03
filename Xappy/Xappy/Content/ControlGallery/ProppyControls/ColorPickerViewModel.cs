using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xappy.Content.ControlGallery.ProppyControls
{
    public class ColorPickerViewModel : BaseViewModel
    {
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