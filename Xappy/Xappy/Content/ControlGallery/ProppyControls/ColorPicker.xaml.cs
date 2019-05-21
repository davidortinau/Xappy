using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Xamarin.Forms;

namespace Xappy.Content.ControlGallery.ProppyControls
{
    public partial class ColorPicker
    {
        public static readonly BindableProperty ElementInfoProperty = BindableProperty.Create(nameof(ElementInfo), typeof(PropertyInfo), typeof(ColorPicker), null,
            propertyChanged: OnElementInfoChanged);

        public static readonly BindableProperty ElementProperty = BindableProperty.Create(nameof(Element), typeof(View), typeof(ColorPicker), null,
            propertyChanged: OnElementChanged);

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorPicker), Color.Default,
            propertyChanged: OnColorChanged);

        Slider[] _sliders;

        public ColorPicker()
        {
            InitializeComponent();

            BindingContext = new ColorPickerViewModel();
        }

        public View Element
        {
            get => (View)GetValue(ElementProperty);
            set => SetValue(ElementProperty, value);
        }

        public PropertyInfo ElementInfo
        {
            get => (PropertyInfo)GetValue(ElementInfoProperty);
            set => SetValue(ElementInfoProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        void OnColorSliderChanged(object sender, ValueChangedEventArgs e)
        {

            if (!isEditingHex & R != null)
            {
                var color = Color.FromRgba(
                (int)R.Value,
                (int)G.Value,
                (int)B.Value,
                (int)A.Value);

                Color = color;
            }
        }

        static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ColorPicker picker)
            {
                picker.HexEntry.Text = picker.Color.ToHex();
                if (picker.Element != null)
                {
                    picker.ElementInfo.SetValue(picker.Element, picker.Color);
                }
            }
        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            isEditingHex = true;
            HexEntry.TextChanged += HexEntry_TextChanged;
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            isEditingHex = false;
            HexEntry.TextChanged -= HexEntry_TextChanged;
        }

        bool isEditingHex = false;

        void HexEntry_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var color = e.NewTextValue;
            if (color.Length == 6)
            {
                var c = Color.FromHex($"{color}");
                if (c != Color)
                {
                    Color = c;

                    isEditingHex = true;

                    R.Value = Math.Min((int)(c.R * 256), 255);
                    G.Value = Math.Min((int)(c.G * 256), 255);
                    B.Value = Math.Min((int)(c.B * 256), 255);
                    A.Value = c.A * 255;

                    isEditingHex = false;

                }
            }
        }

        void Handle_SelectionChanged(object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var c = e.CurrentSelection[0] as ColorViewModel;
            Color = Color.FromHex($"{c.HexColor}");

            isEditingHex = true;

            R.Value = Math.Min((int)(Color.R * 256), 255);
            G.Value = Math.Min((int)(Color.G * 256), 255);
            B.Value = Math.Min((int)(Color.B * 256), 255);
            A.Value = Color.A * 255;

            isEditingHex = false;

        }

        static void OnElementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //if (bindable is View picker)
            //{
            //    picker.HexEntry.Text = picker.Title;
            //}
        }

        static void OnElementInfoChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ColorPicker picker)
            {
                picker.HexEntry.Text = picker.Color.ToHex();
                picker.Color = (Color)picker.ElementInfo.GetValue(picker.Element);

                picker.isEditingHex = true;

                picker.R.Value = Math.Min((int)(picker.Color.R * 256), 255);
                picker.G.Value = Math.Min((int)(picker.Color.G * 256), 255);
                picker.B.Value = Math.Min((int)(picker.Color.B * 256), 255);
                picker.A.Value = picker.Color.A * 255;

                picker.isEditingHex = false;
            }
        }
    }
}
