using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xappy.Scenarios
{
    public class IndexPageViewModel : INotifyPropertyChanged
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

        public ICommand SelectCommand { get; set; }

        public IndexPageViewModel()
        {
            SelectCommand = new Command(Selected);
        }

        private async void Selected()
        {
            if (SelectedItem == null)
                return;

            string targetPage = "map";
            switch (SelectedItem.ToLower())
            {
                case "map":
                    targetPage = "map";
                    break;
                case "login":
                    targetPage = "login";
                    break;
                case "to do list":
                    targetPage = "todo";
                    break;
                default:
                    break;

            }

            await Shell.Current.GoToAsync($"{targetPage}");
        }

        public string SelectedItem { get; set; }
    }

}
