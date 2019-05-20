using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xappy.Content.ControlGallery;
using Xappy.Content.ControlGallery.ControlTemplates;
using Xappy.Content.ControlGallery.ProppyControls;

namespace Xappy.ControlGallery
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class ControlPage : ContentPage
    {
        PropertyInfo ActiveProperty;
        View PresentedPropertyControl;

        private View _element; // this is the target control

        public View Element
        {
            get
            {
                return _element;
            }
        }

        StackLayout _propertyLayout; // this is the property grid content

        HashSet<string> _exceptProperties = new HashSet<string>
        {
            AutomationIdProperty.PropertyName,
            ClassIdProperty.PropertyName,
            "StyleId",
        };

        ControlPageViewModel _vm;

        public ControlPage()
        {
            InitializeComponent();

            BindingContext = _vm = new ControlPageViewModel();
        }

        private View ControlTemplate;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null && !string.IsNullOrEmpty(_vm.ControlTemplate))
            {
                ControlTemplate = (View)Activator.CreateInstance(Type.GetType($"Xappy.Content.ControlGallery.ControlTemplates.{_vm.ControlTemplate}"));
            }
            else
            {
                ControlTemplate = new ButtonControlTemplate();
            }

            ControlCanvas.Children.Clear();
            ControlCanvas.Children.Add(ControlTemplate);

            _element = (ControlTemplate as IControlTemplate).TargetControl;
            //_propertyLayout = PropertyContainer;

            (BindingContext as ControlPageViewModel).SetElement(_element, _exceptProperties);
        }

        async void Handle_SelectionChanged(object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            if (!e.CurrentSelection.Any())
                return;

            var property = e.CurrentSelection[0] as PropertyInfo;
            var propertyControl = EditorForProperty(property);

            if (propertyControl != null)
                await PresentControlForProperty(property, propertyControl);
        }

        private View EditorForProperty(PropertyInfo property)
        {
            if (property.PropertyType == typeof(Color))
            {
                return new ColorPicker
                {
                    Element = _element,
                    ElementInfo = property,

                };
            }
            else if (property.PropertyType == typeof(string))
            {
                return new TextEntry
                {
                    Element = _element,
                    ElementInfo = property
                };
            }
            else if (property.PropertyType == typeof(Thickness))
            {
                if (property.Name.ToLower() == "padding")
                {
                    return new PaddingProperty
                    {
                        Element = _element,
                        ElementInfo = property
                    };
                }
                else if (property.Name.ToLower() == "margin")
                {
                    return new MarginProperty
                    {
                        Element = _element,
                        ElementInfo = property
                    };
                }
            }
            else if (
               property.PropertyType == typeof(double) ||
               property.PropertyType == typeof(float) ||
               property.PropertyType == typeof(int)
               )
            {
                return new SingleSmallNumberProperty
                {
                    Element = _element,
                    ElementInfo = property
                };
            }

            // no implementation for this property
            Console.WriteLine($"{property.Name}, {property.PropertyType.Name}");

            return null;
        }

        class NamedAction
        {
            public string Name { get; set; }

            public Action<View> Action { get; set; }
        }

        (Func<View> ctor, NamedAction[] methods) GetPicker()
        {
            return (ctor: () =>
            {
                var picker = new Picker();
                picker.Items.Add("item 1");
                picker.Items.Add("item 2");
                return picker;
            }, methods: new[] {
                    new NamedAction {
                        Name = "Add item",
                        Action = (p) => (p as Picker).Items.Add("item")
                    },
                    new NamedAction {
                        Name = "Remove item last item",
                        Action = (p) => {
                            var picker = (Picker)p;
                            if (picker.Items.Count > 0)
                                picker.Items.RemoveAt(picker.Items.Count - 1);
                        }
                    },
                    new NamedAction {
                        Name = "Clear",
                        Action = (p) => (p as Picker).Items.Clear()
                    }
                }
            );
        }

        private async void PropertyToolbar_OnBack(object sender, EventArgs e)
        {
            if (PresentedPropertyControl != null)
                await DismissPresentedPropertyControl();
        }

        private async void PropertyToolbar_OnViewSource(object sender, EventArgs e)
        {
            var source = XamlUtil.GetXamlForType(this.ControlTemplate.GetType());
            await Shell.Current.Navigation.PushAsync(new ViewSourcePage
            {
                Source = source
            });
        }

        private async Task PresentControlForProperty(PropertyInfo property, View propertyControl)
        {
            SetInteractionEnabled(false);

            ActiveProperty = property;
            PresentedPropertyControl = propertyControl;

            PropertyToolbar.SetProperty(property.Name);

            Grid.SetRow(propertyControl, 1);
            PresentedPropertyControl.TranslationX = this.Width;
            PropertyContainer.Children.Add(PresentedPropertyControl);

            await Task.WhenAll(
                PropertiesList.FadeTo(0, easing: Easing.CubicOut),
                PresentedPropertyControl.TranslateTo(0, 0, easing: Easing.CubicOut));

            SetInteractionEnabled(true);
        }

        private async Task DismissPresentedPropertyControl()
        {
            SetInteractionEnabled(false);

            await Task.WhenAll(
                PropertiesList.FadeTo(1, easing: Easing.CubicIn),
                PresentedPropertyControl.TranslateTo(this.Width, 0, easing: Easing.CubicIn));

            PropertyContainer.Children.Remove(PresentedPropertyControl);
            ClearSelection();

            SetInteractionEnabled(true);
        }

        private void ClearSelection()
        {
            ActiveProperty = null;
            PresentedPropertyControl = null;
            PropertiesList.SelectedItem = null;
        }

        private void SetInteractionEnabled(bool enabled)
        {
            PropertyToolbar.IsEnabled = enabled;
            PropertiesList.IsEnabled = enabled;
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {

        }
    }

    public class XappyPropertySelector : DataTemplateSelector
    {
        public DataTemplate BooleanTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            //Console.WriteLine($"{((PropertyInfo)item).Name}, {((PropertyInfo)item).PropertyType.Name}");

            return ((PropertyInfo)item).PropertyType == typeof(Boolean)
                ? BooleanTemplate
                : DefaultTemplate;
        }
    }

    public static class GridExtension
    {
        public static void AddChild(this Grid grid, View view, int column, int row, int columnspan = 1, int rowspan = 1)
        {
            if (row < 0)
            {
                throw new ArgumentOutOfRangeException("row");
            }
            if (column < 0)
            {
                throw new ArgumentOutOfRangeException("column");
            }
            if (rowspan <= 0)
            {
                throw new ArgumentOutOfRangeException("rowspan");
            }
            if (columnspan <= 0)
            {
                throw new ArgumentOutOfRangeException("columnspan");
            }
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            Grid.SetRow(view, row);
            Grid.SetRowSpan(view, rowspan);
            Grid.SetColumn(view, column);
            Grid.SetColumnSpan(view, columnspan);
            grid.Children.Add(view);
        }
    }
}
