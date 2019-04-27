using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Xappy.Content.ControlGallery.ProppyControls
{
    public partial class ColorPicker : StackLayout
    {
        public static readonly BindableProperty UseDefaultProperty = BindableProperty.Create(nameof(UseDefault), typeof(bool), typeof(ColorPicker), false,
            propertyChanged: OnColorChanged);

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorPicker), Color.Default,
            propertyChanged: OnColorChanged);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ColorPicker), "Pick a color:",
            propertyChanged: OnTitleChanged);

        public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(ColorPicker), "Color",
            propertyChanged: OnNameChanged);

        public static readonly BindableProperty ElementProperty = BindableProperty.Create(nameof(Element), typeof(View), typeof(ColorPicker), null,
            propertyChanged: OnElementChanged);

        Slider[] _sliders;

        public ColorPicker()
        {
            InitializeComponent();

            BindingContext = new ColorPickerViewModel();


        }

        void Handle_BackClicked(object sender, System.EventArgs e)
        {
            Back?.Invoke(this, EventArgs.Empty);
        }

        public View Element
        {
            get => (View)GetValue(ElementProperty);
            set => SetValue(ElementProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public bool UseDefault
        {
            get => (bool)GetValue(UseDefaultProperty);
            set => SetValue(UseDefaultProperty, value);
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public event EventHandler<ColorPickedEventArgs> ColorPicked;

        public event EventHandler Back;

        void OnColorSliderChanged(object sender, ValueChangedEventArgs e)
        {

            if (!isEditingHex)
            {
                var color = Color.FromRgba(
                (int)R.Value,
                (int)G.Value,
                (int)B.Value,
                (int)A.Value);

                Color = color;
            }
        }

        private void OnUseDefaultToggled(object sender, ToggledEventArgs e)
        {
            UseDefault = !e.Value;

            foreach (var slider in _sliders)
                slider.IsEnabled = e.Value;
        }

        static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ColorPicker picker)
            {
                var color = picker.UseDefault ? Color.Default : picker.Color;
                picker.HexEntry.Text = color.ToHex();
                //picker._box.Color = color;
                picker.ColorPicked?.Invoke(picker, new ColorPickedEventArgs(color));
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

        }

        static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ColorPicker picker)
            {
                picker.HexEntry.Text = picker.Title;
            }
        }

        static void OnNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ColorPicker picker)
            {
                picker.NameLabel.Text = picker.Name;
            }
        }



        static void OnElementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //if (bindable is View picker)
            //{
            //    picker.HexEntry.Text = picker.Title;
            //}
        }
    }

    public class ColorPickedEventArgs : EventArgs
    {
        public ColorPickedEventArgs(Color color)
        {
            Color = color;
        }

        public Color Color { get; }
    }
}
