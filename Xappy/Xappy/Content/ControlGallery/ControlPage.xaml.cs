using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xappy.Content.ControlGallery.ProppyControls;

namespace Xappy.ControlGallery
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class ControlPage : ContentPage
    {

        View _element; // this is the target control

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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _element = TargetControl;
            _propertyLayout = PropertyContainer;

            OnElementUpdated(_element);
        }

        void OnElementUpdated(View oldElement)
        {
            _propertyLayout.Children.Clear();

            var elementType = _element.GetType();

            var publicProperties = elementType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && !_exceptProperties.Contains(p.Name));

            // BindableProperty used to clean property values
            var bindableProperties = elementType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => p.FieldType.IsAssignableFrom(typeof(BindableProperty)))
                .Select(p => (BindableProperty)p.GetValue(_element));

            foreach (var property in publicProperties)
            {
                if (property.PropertyType == typeof(Color))
                {
                    var colorPicker = new ColorPicker
                    {
                        Title = property.Name,
                        Color = (Color)property.GetValue(_element)
                    };
                    //var colorPicker = new ColorPicker
                    //{
                    //    Title = property.Name,
                    //    Color = (Color)property.GetValue(_element)
                    //};
                    colorPicker.ColorPicked += (_, e) => property.SetValue(_element, e.Color);
                    _propertyLayout.Children.Add(colorPicker);
                }
                else if (property.PropertyType == typeof(string))
                {
                    _propertyLayout.Children.Add(CreateStringPicker(property));
                }
                else if (property.PropertyType == typeof(double) ||
                    property.PropertyType == typeof(float) ||
                    property.PropertyType == typeof(int))
                {
                    _propertyLayout.Children.Add(
                        CreateValuePicker(property, bindableProperties.FirstOrDefault(p => p.PropertyName == property.Name)));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    _propertyLayout.Children.Add(CreateBooleanPicker(property));
                }
                else if (property.PropertyType == typeof(Thickness))
                {
                    _propertyLayout.Children.Add(CreateThicknessPicker(property));
                }
                else
                {
                    //_propertyLayout.Children.Add(new Label { Text = $"//TODO: {property.Name} ({property.PropertyType})", TextColor = Color.Gray });
                }
            }

            //var customMethods = _testedTypes[elementType.Name].methods;
            //if (customMethods != null)
            //{
            //    _propertyLayout.Children.Add(new Label
            //    {
            //        Text = "Custom methods",
            //        FontSize = 20,
            //        Margin = 6
            //    });

            //    foreach (var method in customMethods)
            //    {
            //        _propertyLayout.Children.Add(new Button
            //        {
            //            Text = method.Name,
            //            FontAttributes = FontAttributes.Bold,
            //            Padding = 6,
            //            Command = new Command(() => method.Action(_element))
            //        });
            //    }
            //}

            //_pageContent.Children.Add(_element);
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


    }

    //public class ColorPicker : ContentView
    //{
    //    public static readonly BindableProperty UseDefaultProperty = BindableProperty.Create(nameof(UseDefault), typeof(bool), typeof(ColorPicker), false,
    //        propertyChanged: OnColorChanged);

    //    public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorPicker), Color.Default,
    //        propertyChanged: OnColorChanged);

    //    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ColorPicker), "Pick a color:",
    //        propertyChanged: OnTitleChanged);

    //    static readonly string[] _components = { "R", "G", "B", "A" };

    //    Label _titleLabel;
    //    Slider[] _sliders;
    //    BoxView _box;
    //    Label _hexLabel;
    //    Switch _useDefault;

    //    public ColorPicker()
    //    {
    //        var grid = new Grid
    //        {
    //            Padding = 0,
    //            RowSpacing = 3,
    //            ColumnSpacing = 3,
    //            ColumnDefinitions =
    //            {
    //                new ColumnDefinition { Width = 20 },
    //                new ColumnDefinition { Width = GridLength.Star },
    //                new ColumnDefinition { Width = 60 },
    //            },
    //        };

    //        _titleLabel = new Label { Text = (string)TitleProperty.DefaultValue };
    //        grid.AddChild(_titleLabel, 0, 0, 2);

    //        _useDefault = new Switch
    //        {
    //            IsToggled = true,
    //            HorizontalOptions = LayoutOptions.Center,
    //            VerticalOptions = LayoutOptions.Center
    //        };
    //        _useDefault.Toggled += OnUseDefaultToggled;
    //        grid.AddChild(_useDefault, 2, 0);

    //        _sliders = new Slider[_components.Length];
    //        for (var i = 0; i < _components.Length; i++)
    //        {
    //            _sliders[i] = new Slider
    //            {
    //                VerticalOptions = LayoutOptions.Center,
    //                Minimum = 0,
    //                Maximum = 255,
    //                Value = 255
    //            };
    //            _sliders[i].ValueChanged += OnColorSliderChanged;
    //            var label = new Label
    //            {
    //                Text = _components[i],
    //                HorizontalOptions = LayoutOptions.Center,
    //                VerticalOptions = LayoutOptions.Center
    //            };
    //            grid.AddChild(label, 0, i + 1);
    //            grid.AddChild(_sliders[i], 1, i + 1);
    //        }

    //        _box = new BoxView
    //        {
    //            Color = Color,
    //            HorizontalOptions = LayoutOptions.Fill,
    //            VerticalOptions = LayoutOptions.Fill,
    //        };
    //        grid.AddChild(_box, 2, 1, 1, 3);

    //        _hexLabel = new Label
    //        {
    //            Text = ColorToHex(Color),
    //            HorizontalOptions = LayoutOptions.Center,
    //            VerticalOptions = LayoutOptions.Center,
    //            FontSize = 10,
    //        };
    //        grid.AddChild(_hexLabel, 2, 4);

    //        Content = grid;
    //    }

    //    public string Title
    //    {
    //        get => (string)GetValue(TitleProperty);
    //        set => SetValue(TitleProperty, value);
    //    }

    //    public bool UseDefault
    //    {
    //        get => (bool)GetValue(UseDefaultProperty);
    //        set => SetValue(UseDefaultProperty, value);
    //    }

    //    public Color Color
    //    {
    //        get => (Color)GetValue(ColorProperty);
    //        set => SetValue(ColorProperty, value);
    //    }

    //    public event EventHandler<ColorPickedEventArgs> ColorPicked;

    //    void OnColorSliderChanged(object sender, ValueChangedEventArgs e)
    //    {
    //        var color = Color.FromRgba(
    //            (int)_sliders[0].Value,
    //            (int)_sliders[1].Value,
    //            (int)_sliders[2].Value,
    //            (int)_sliders[3].Value);
    //        Color = color;
    //    }

    //    private void OnUseDefaultToggled(object sender, ToggledEventArgs e)
    //    {
    //        UseDefault = !e.Value;

    //        foreach (var slider in _sliders)
    //            slider.IsEnabled = e.Value;
    //    }

    //    static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    //    {
    //        if (bindable is ColorPicker picker)
    //        {
    //            var color = picker.UseDefault ? Color.Default : picker.Color;
    //            picker._hexLabel.Text = color.IsDefault ? "<default>" : ColorToHex(color);
    //            picker._box.Color = color;
    //            picker.ColorPicked?.Invoke(picker, new ColorPickedEventArgs(color));
    //        }
    //    }

    //    static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    //    {
    //        if (bindable is ColorPicker picker)
    //        {
    //            picker._titleLabel.Text = picker.Title;
    //        }
    //    }

    //    static string ColorToHex(Color color)
    //    {
    //        var a = (int)(color.A * 255);
    //        var r = (int)(color.R * 255);
    //        var g = (int)(color.G * 255);
    //        var b = (int)(color.B * 255);

    //        var value = a << 24 | r << 16 | g << 8 | b;

    //        return "#" + value.ToString("X");
    //    }
    //}

    //public class ColorPickedEventArgs : EventArgs
    //{
    //    public ColorPickedEventArgs(Color color)
    //    {
    //        Color = color;
    //    }

    //    public Color Color { get; }
    //}

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
