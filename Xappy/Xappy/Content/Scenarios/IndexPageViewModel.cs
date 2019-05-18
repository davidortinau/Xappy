using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Content;

namespace Xappy.Scenarios
{
    public class IndexPageViewModel : BaseViewModel
    {
        private string selectedItem;

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
                case "conversation":
                    targetPage = "conversation";
                    break;
                default:
                    break;

            }

            SelectedItem = null;
            await Shell.Current.GoToAsync($"{targetPage}");
        }

        public string SelectedItem {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }
    }

}
