using System.Reflection;
using Xamarin.Forms;

namespace Xappy.Content.ControlGallery.ProppyControls
{
    public partial class TextEntry
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
            if (bindable is TextEntry control)
            {
               control.TextEntryField.Text = (string)control.ElementInfo.GetValue(control.Element);
               control.TextEntryField.Placeholder = (string) control.ElementInfo.Name;
            }
        }
        
        static void OnElementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        public TextEntry()
        {
            InitializeComponent();
        }

        private void TextEntryField_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ElementInfo.SetValue(Element, e.NewTextValue);
        }
    }
}
