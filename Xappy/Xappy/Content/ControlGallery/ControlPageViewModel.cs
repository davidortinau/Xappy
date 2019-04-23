using System;
using System.Collections.Generic;
using System.ComponentModel;
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


            ViewXAMLCommand = new Command(ViewXAML);
            UndoCommand = new Command(Undo);
            RedoCommand = new Command(Redo);
            ResetCommand = new Command(Reset);
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

    }

    //public class ControlType
    //{
    //    public string Title = "";
    //}
}
