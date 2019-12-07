using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Content;

namespace Xappy.Scenarios
{
    public class IndexPageViewModel : BaseViewModel
    {
        public ICommand SelectCommand { get; set; }

        public IndexPageViewModel()
        {
            SelectCommand = new Command<string>(Selected);
        }

        private async void Selected(string scenario)
        {
            string targetPage = "map";
            switch (scenario.ToLower())
            {
                case "product details":
                    targetPage = "productdetails";
                    break;
                case "map":
                    targetPage = "map";
                    break;
                case "login":
                    targetPage = "login";
                    break;
                case "other login":
                    targetPage = "otherlogin";
                    break;
                case "to do list":
                    targetPage = "todo";
                    break;
                case "conversational":
                    targetPage = "conversation";
                    break;
                default:
                    break;

            }

            await Shell.Current.GoToAsync($"{targetPage}");
        }

    }

}
