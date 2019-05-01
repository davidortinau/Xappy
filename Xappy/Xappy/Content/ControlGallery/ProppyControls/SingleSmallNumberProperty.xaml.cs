using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;

namespace Xappy.Content.ControlGallery.ProppyControls
{
    public partial class SingleSmallNumberProperty
    {
        public static readonly BindableProperty ElementInfoProperty = BindableProperty.Create(nameof(ElementInfo), typeof(PropertyInfo), typeof(ColorPicker), null,
            propertyChanged: OnElementInfoChanged);

        public static readonly BindableProperty ElementProperty = BindableProperty.Create(nameof(Element), typeof(View), typeof(ColorPicker), null,
            propertyChanged: OnElementChanged);

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

        static void OnElementInfoChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SingleSmallNumberProperty control)
            {
                var v = control.ElementInfo.GetValue(control.Element);
                control.TextEntryField.Text = v.ToString();
                control.TextEntryField.Placeholder = (string)control.ElementInfo.Name;
            }
        }

        static void OnElementChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }

        public SingleSmallNumberProperty()
        {
            InitializeComponent();
        }

        private void TextEntryField_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (ElementInfo.PropertyType == typeof(double) ||
                    ElementInfo.PropertyType == typeof(float))
                {
                    ElementInfo.SetValue(Element, Convert.ToDouble(e.NewTextValue));
                }
                else
                {
                    ElementInfo.SetValue(Element, Convert.ToInt32(e.NewTextValue));
                }
            }
        }
    }
}
