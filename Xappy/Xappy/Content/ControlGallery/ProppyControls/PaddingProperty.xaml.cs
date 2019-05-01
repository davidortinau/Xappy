using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xappy.Content.ControlGallery.ProppyControls
{
    public partial class PaddingProperty
    {
        public static readonly BindableProperty ElementInfoProperty = BindableProperty.Create(nameof(ElementInfo), typeof(PropertyInfo), typeof(ColorPicker), null,
            propertyChanged: OnElementInfoChanged);

        public static readonly BindableProperty ElementProperty = BindableProperty.Create(nameof(Element), typeof(View), typeof(ColorPicker), null,
            propertyChanged: OnElementChanged);
        
        public static readonly BindableProperty ElementPaddingProperty = BindableProperty.Create(nameof(ElementPadding), typeof(Thickness), typeof(ColorPicker), new Thickness(),
            propertyChanged: OnElementPaddingChanged);

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
        
        public Thickness ElementPadding
        {
            get => (Thickness)GetValue(ElementPaddingProperty);
            set => SetValue(ElementPaddingProperty, value);
        }

        static void OnElementInfoChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is PaddingProperty control)
            {
                if (control.Element != null)
                {
                    control.ElementPadding = (Thickness) control.ElementInfo.GetValue(control.Element);
                    
                    control.LeftStepper.Value = control.ElementPadding.Left;
                    control.TopStepper.Value = control.ElementPadding.Top;
                    control.RightStepper.Value = control.ElementPadding.Right;
                    control.BottomStepper.Value = control.ElementPadding.Bottom;
                }

            }
        }

        static void OnElementChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
        
        static void OnElementPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is PaddingProperty control)
            {
                control.LeftPadding.Text = control.ElementPadding.Left.ToString();
                control.TopPadding.Text = control.ElementPadding.Top.ToString();
                control.RightPadding.Text = control.ElementPadding.Right.ToString();
                control.BottomPadding.Text = control.ElementPadding.Bottom.ToString();
                
                control.ElementInfo.SetValue(control.Element, control.ElementPadding);
            }

        }

        public PaddingProperty()
        {
            InitializeComponent();
        }

        private void LeftStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ElementPadding = new Thickness
            {
                Left = e.NewValue,
                Top =  TopStepper.Value,
                Right = RightStepper.Value,
                Bottom = BottomStepper.Value
            };
        }

        private void TopStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ElementPadding = new Thickness
            {
                Left = LeftStepper.Value,
                Top =  e.NewValue,
                Right = RightStepper.Value,
                Bottom = BottomStepper.Value
            };
        }

        private void RightStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ElementPadding = new Thickness
            {
                Left = LeftStepper.Value,
                Top =  TopStepper.Value,
                Right = e.NewValue,
                Bottom = BottomStepper.Value
            };
        }

        private void BottomStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ElementPadding = new Thickness
            {
                Left = LeftStepper.Value,
                Top =  TopStepper.Value,
                Right = RightStepper.Value,
                Bottom = e.NewValue
            };
        }
    }
}
