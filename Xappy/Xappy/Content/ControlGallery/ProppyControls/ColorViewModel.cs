using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Xappy.Content.ControlGallery.ProppyControls
{


    public class ColorViewModel : BaseViewModel
    {
        private ColorTypeConverter _converter;

        public ColorViewModel()
        {

            _converter = new ColorTypeConverter();
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                var v = string.Format("http://placehold.it/50x50/{0}", getColorHex(_name));
                //System.Diagnostics.Debug.WriteLine(v);
                return v;
            }
        }

        public string HexColor
        {
            get
            {
                var color = (Color)_converter.ConvertFromInvariantString(_name);
                return $"#{color.ToHex()}";
            }
        }

        string getColorHex(string colorName)
        {
            return HexColor.Replace("#", "");
        }
    }
}
