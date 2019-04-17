using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

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

        public ICommand SelectCommand { get; set; }

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


        public ControlPageViewModel()
        {


            //SelectCommand = new Command(ControlSelected);
        }

        //private async void ControlSelected()
        //{
        //    // navigate to the control page
        //    await Shell.Current.GoToAsync()
        //}
    }

    //public class ControlType
    //{
    //    public string Title = "";
    //}
}
