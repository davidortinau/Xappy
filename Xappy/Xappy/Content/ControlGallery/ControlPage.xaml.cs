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

        public ControlPage()
        {
            InitializeComponent();

            BindingContext = new ControlPageViewModel();
        }

        private View ControlTemplate;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // do the looked to get the right template
            ControlTemplate = new ButtonControlTemplate();

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

        Dictionary<string, (double min, double max)> _minMaxProperties = new Dictionary<string, (double min, double max)>
        {
            { ScaleProperty.PropertyName, (0d, 1d) },
            { ScaleXProperty.PropertyName, (0d, 1d) },
            { ScaleYProperty.PropertyName, (0d, 1d) },
            { OpacityProperty.PropertyName, (0d, 1d) },
            { RotationProperty.PropertyName, (0d, 360d) },
            { RotationXProperty.PropertyName, (0d, 360d) },
            { RotationYProperty.PropertyName, (0d, 360d) },
            { View.MarginProperty.PropertyName, (-100, 100) },
            { PaddingProperty.PropertyName, (-100, 100) },
        };

        Grid CreateValuePicker(PropertyInfo property, BindableProperty bindableProperty)
        {
            var min = 0d;
            var max = 100d;
            if (_minMaxProperties.ContainsKey(property.Name))
            {
                min = _minMaxProperties[property.Name].min;
                max = _minMaxProperties[property.Name].max;
            }

            var isInt = property.PropertyType == typeof(int);
            var value = isInt ? (int)property.GetValue(_element) : (double)property.GetValue(_element);
            var slider = new Slider(min, max, value);

            var actions = new Grid
            {
                Padding = 0,
                ColumnSpacing = 6,
                RowSpacing = 6,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 40 }
                }
            };

            actions.AddChild(new Label { Text = property.Name, FontAttributes = FontAttributes.Bold }, 0, 0, 2);

            if (bindableProperty != null)
            {
                actions.AddChild(new Button
                {
                    Text = "X",
                    TextColor = Color.White,
                    BackgroundColor = Color.DarkRed,
                    WidthRequest = 28,
                    HeightRequest = 28,
                    Margin = 0,
                    Padding = 0,
                    Command = new Command(() => _element.ClearValue(bindableProperty))
                }, 1, 0);
            }

            var valueLabel = new Label
            {
                Text = slider.Value.ToString(isInt ? "0" : "0.#"),
                HorizontalOptions = LayoutOptions.End
            };

            slider.ValueChanged += (_, e) =>
            {
                if (isInt)
                    property.SetValue(_element, (int)e.NewValue);
                else
                    property.SetValue(_element, e.NewValue);
                valueLabel.Text = e.NewValue.ToString(isInt ? "0" : "0.#");
            };

            actions.AddChild(slider, 0, 1);
            actions.AddChild(valueLabel, 1, 1);

            return actions;
        }

        Grid CreateThicknessPicker(PropertyInfo property)
        {
            var grid = new Grid
            {
                Padding = 0,
                RowSpacing = 3,
                ColumnSpacing = 3,
                ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = 50 },
                            new ColumnDefinition { Width = GridLength.Star },
                            new ColumnDefinition { Width = 30 }
                        },
            };
            grid.AddChild(new Label { Text = property.Name, FontAttributes = FontAttributes.Bold }, 0, 0, 2);

            var val = (Thickness)property.GetValue(_element);
            var sliders = new Slider[4];
            var valueLabels = new Label[4];
            for (int i = 0; i < 4; i++)
            {
                sliders[i] = new Slider
                {
                    VerticalOptions = LayoutOptions.Center,
                    Minimum = 0,
                    Maximum = 100
                };
                var row = i + 1;
                switch (i)
                {
                    case 0:
                        sliders[i].Value = val.Left;
                        grid.AddChild(new Label { Text = nameof(val.Left) }, 0, row);
                        break;
                    case 1:
                        sliders[i].Value = val.Top;
                        grid.AddChild(new Label { Text = nameof(val.Top) }, 0, row);
                        break;
                    case 2:
                        sliders[i].Value = val.Right;
                        grid.AddChild(new Label { Text = nameof(val.Right) }, 0, row);
                        break;
                    case 3:
                        sliders[i].Value = val.Bottom;
                        grid.AddChild(new Label { Text = nameof(val.Bottom) }, 0, row);
                        break;
                }

                valueLabels[i] = new Label { Text = sliders[i].Value.ToString("0") };
                grid.AddChild(sliders[i], 1, row);
                grid.AddChild(valueLabels[i], 2, row);
                sliders[i].ValueChanged += ThicknessChanged;
            }

            void ThicknessChanged(object sender, ValueChangedEventArgs e)
            {
                property.SetValue(_element, new Thickness(sliders[0].Value, sliders[1].Value, sliders[2].Value, sliders[3].Value));
                for (int i = 0; i < valueLabels.Length; i++)
                    valueLabels[i].Text = sliders[i].Value.ToString("0");
            }

            return grid;
        }

        Grid CreateBooleanPicker(PropertyInfo property)
        {
            var grid = new Grid
            {
                Padding = 0,
                ColumnSpacing = 6,
                RowSpacing = 6,
                ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Star },
                            new ColumnDefinition { Width = 50 }
                        }
            };
            grid.AddChild(new Label { Text = property.Name, FontAttributes = FontAttributes.Bold }, 0, 0);
            var boolSwitch = new Switch
            {
                IsToggled = (bool)property.GetValue(_element),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            boolSwitch.Toggled += (_, e) => property.SetValue(_element, e.Value);
            grid.AddChild(boolSwitch, 1, 0);
            _element.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == property.Name)
                {
                    var newVal = (bool)property.GetValue(_element);
                    if (newVal != boolSwitch.IsToggled)
                        boolSwitch.IsToggled = newVal;
                }
            };

            return grid;
        }

        Grid CreateStringPicker(PropertyInfo property)
        {
            var grid = new Grid
            {
                Padding = 0,
                ColumnSpacing = 6,
                RowSpacing = 6
            };
            grid.AddChild(new Label { Text = property.Name, FontAttributes = FontAttributes.Bold }, 0, 0);
            var entry = new Entry
            {
                Text = (string)property.GetValue(_element),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            entry.TextChanged += (_, e) => property.SetValue(_element, e.NewTextValue);
            grid.AddChild(entry, 0, 1);
            _element.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == property.Name)
                {
                    var newVal = (string)property.GetValue(_element);
                    if (newVal != entry.Text)
                        entry.Text = newVal;
                }
            };

            return grid;
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
