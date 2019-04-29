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
    public partial class MarginProperty : ScrollView
    {
        public MarginProperty()
        {
            InitializeComponent();
        }
        
        public static readonly BindableProperty ElementInfoProperty = BindableProperty.Create(nameof(ElementInfo), typeof(PropertyInfo), typeof(ColorPicker), null,
            propertyChanged: OnElementInfoChanged);

        public static readonly BindableProperty ElementProperty = BindableProperty.Create(nameof(Element), typeof(View), typeof(ColorPicker), null,
            propertyChanged: OnElementChanged);
        
        public static readonly BindableProperty ElementMarginProperty = BindableProperty.Create(nameof(ElementMargin), typeof(Thickness), typeof(ColorPicker), new Thickness(),
            propertyChanged: OnElementMarginChanged);

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
        
        public Thickness ElementMargin
        {
            get => (Thickness)GetValue(ElementMarginProperty);
            set => SetValue(ElementMarginProperty, value);
        }

        static void OnElementInfoChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MarginProperty control)
            {
                if (control.Element != null)
                {
                    control.ElementMargin = (Thickness) control.ElementInfo.GetValue(control.Element);
                    
                    control.LeftStepper.Value = control.ElementMargin.Left;
                    control.TopStepper.Value = control.ElementMargin.Top;
                    control.RightStepper.Value = control.ElementMargin.Right;
                    control.BottomStepper.Value = control.ElementMargin.Bottom;
                }

            }
        }

        static void OnElementChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
        
        static void OnElementMarginChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MarginProperty control)
            {
                control.LeftMargin.Text = control.ElementMargin.Left.ToString();
                control.TopMargin.Text = control.ElementMargin.Top.ToString();
                control.RightMargin.Text = control.ElementMargin.Right.ToString();
                control.BottomMargin.Text = control.ElementMargin.Bottom.ToString();
                
                control.ElementInfo.SetValue(control.Element, control.ElementMargin);
            }

        }

        private void LeftStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ElementMargin = new Thickness
            {
                Left = e.NewValue,
                Top =  TopStepper.Value,
                Right = RightStepper.Value,
                Bottom = BottomStepper.Value
            };
        }

        private void TopStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ElementMargin = new Thickness
            {
                Left = LeftStepper.Value,
                Top =  e.NewValue,
                Right = RightStepper.Value,
                Bottom = BottomStepper.Value
            };
        }

        private void RightStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ElementMargin = new Thickness
            {
                Left = LeftStepper.Value,
                Top =  TopStepper.Value,
                Right = e.NewValue,
                Bottom = BottomStepper.Value
            };
        }

        private void BottomStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ElementMargin = new Thickness
            {
                Left = LeftStepper.Value,
                Top =  TopStepper.Value,
                Right = RightStepper.Value,
                Bottom = e.NewValue
            };
        }
    }
}
