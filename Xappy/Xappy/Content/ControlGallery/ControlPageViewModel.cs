using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Content.ControlGallery;

namespace Xappy.ControlGallery
{
    [QueryProperty("ControlTitle", "control")]
    public class ControlPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetAndRaisePropertyChanged<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }

        public ControlType SelectedControl { get; set; }

        public ICommand ViewXAMLCommand { get; set; }

        public ICommand UndoCommand { get; set; }

        public ICommand RedoCommand { get; set; }

        public ICommand ResetCommand { get; set; }

        public ICommand SelectCommand { get; set; }

        public ObservableCollection<PropertyInfo> Properties { get => properties; set => SetAndRaisePropertyChanged(ref properties, value); }

        public PropertyInfo Selected { get; set; }

        private string _controlTitle;
        public string ControlTitle
        {
            get
            {
                return _controlTitle;
            }
            set
            {
                SetAndRaisePropertyChanged(ref _controlTitle, value);
            }
        }

        private View _element;
        private ObservableCollection<PropertyInfo> properties;

        public ControlPageViewModel()
        {


            ViewXAMLCommand = new Command(ViewXAML);
            UndoCommand = new Command(Undo);
            RedoCommand = new Command(Redo);
            ResetCommand = new Command(Reset);
            SelectCommand = new Command(OnPropertySelect);
        }

        private void OnPropertySelect()
        {
            // need to push a new property panel into that view
        }

        private async void ViewXAML()
        {
            var source = XamlUtil.GetXamlForType(typeof(ControlPage));
            await Shell.Current.Navigation.PushAsync(new ViewSourcePage
            {
                Source = source
            });
        }

        private void Undo()
        {

        }

        private void Redo()
        {

        }

        private void Reset()
        {

        }

        //HashSet<string> _exceptProperties = new HashSet<string>
        //{
        //    AutomationIdProperty.PropertyName,
        //    ClassIdProperty.PropertyName,
        //    "StyleId",
        //};


        public void SetElement(View view)
        {
            _element = view;

            var elementType = _element.GetType();

            var publicProperties = elementType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);// && !_exceptProperties.Contains(p.Name)

            // BindableProperty used to clean property values
            var bindableProperties = elementType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => p.FieldType.IsAssignableFrom(typeof(BindableProperty)))
                .Select(p => (BindableProperty)p.GetValue(_element));

            var props = new ObservableCollection<PropertyInfo>();
            foreach (var property in publicProperties)
            {
                if (
                    property.PropertyType == typeof(Color)
                    || property.PropertyType == typeof(string)
                    || property.PropertyType == typeof(double)
                    || property.PropertyType == typeof(float)
                    || property.PropertyType == typeof(int)
                    || property.PropertyType == typeof(bool)
                    || property.PropertyType == typeof(Thickness)
                    )
                {
                    props.Add(property);
                }
                //if (property.PropertyType == typeof(Color))
                //{
                //    var colorPicker = new ColorPicker
                //    {
                //        Title = property.Name,
                //        Color = (Color)property.GetValue(_element)
                //    };
                //    colorPicker.ColorPicked += (_, e) => property.SetValue(_element, e.Color);
                //    _propertyLayout.Children.Add(colorPicker);
                //}
                //else if (property.PropertyType == typeof(string))
                //{
                //    _propertyLayout.Children.Add(CreateStringPicker(property));
                //}
                //else if (property.PropertyType == typeof(double) ||
                //    property.PropertyType == typeof(float) ||
                //    property.PropertyType == typeof(int))
                //{
                //    _propertyLayout.Children.Add(
                //        CreateValuePicker(property, bindableProperties.FirstOrDefault(p => p.PropertyName == property.Name)));
                //}
                //else if (property.PropertyType == typeof(bool))
                //{
                //    _propertyLayout.Children.Add(CreateBooleanPicker(property));
                //}
                //else if (property.PropertyType == typeof(Thickness))
                //{
                //    _propertyLayout.Children.Add(CreateThicknessPicker(property));
                //}
                //else
                //{
                //    //_propertyLayout.Children.Add(new Label { Text = $"//TODO: {property.Name} ({property.PropertyType})", TextColor = Color.Gray });
                //}
            }

            Properties = props;

        }

    }

}
