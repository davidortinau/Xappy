using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Content;
using Xappy.Content.ControlGallery;

namespace Xappy.ControlGallery
{
    [QueryProperty("ControlTemplate", "template")]
    [QueryProperty("ControlTitle", "control")]
    public class ControlPageViewModel : BaseViewModel
    {
        public ControlType SelectedControl { get; set; }

        public ICommand ViewXAMLCommand { get; set; }

        public ICommand UndoCommand { get; set; }

        public ICommand RedoCommand { get; set; }

        public ICommand ResetCommand { get; set; }

        public ICommand SelectCommand { get; set; }

        public ICommand SwitchedCommand { get; set; }

        public ObservableCollection<PropertyInfo> Properties { get => properties; set => SetProperty(ref properties, value); }

        public PropertyInfo Selected { get; set; }

        private string _controlTitle;
        public string ControlTitle
        {
            get => _controlTitle;
            set => SetProperty(ref _controlTitle, value);
        }

        private string _controlTemplate;
        public string ControlTemplate
        {
            get
            {
                return _controlTemplate;
            }
            set
            {
                SetProperty(ref _controlTemplate, value);
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
            SwitchedCommand = new Command<PropertyInfo>(OnSwitchToggled);
        }

        private void OnSwitchToggled(PropertyInfo propertyInfo)
        {
            var currentValue = (bool)propertyInfo.GetValue(_element);
            propertyInfo.SetValue(_element, !currentValue);
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

        public void SetElement(View view, HashSet<string> exceptions)
        {
            _element = view;

            var elementType = _element.GetType();

            var publicProperties = elementType
                .GetProperties(BindingFlags.Public
                    | BindingFlags.Instance
                    | BindingFlags.FlattenHierarchy)
                .Where(p => p.CanRead && p.CanWrite && !exceptions.Contains(p.Name));



            // BindableProperty used to clean property values
            //var bindableProperties = elementType
            //    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            //    .Where(p => p.FieldType.IsAssignableFrom(typeof(BindableProperty)))
            //    .Select(p => (BindableProperty)p.GetValue(_element));

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
            }

            Properties = new ObservableCollection<PropertyInfo>(props.OrderBy(x => x.Name));

        }

    }

}
