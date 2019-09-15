using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Content;

namespace Xappy.Scenarios
{
    public class IndexPageViewModelDesignTime : BaseViewModel
    {
        private ScenarioCard _selectedItem;
        private List<ScenarioCard> _scenarioCards;

        public ICommand SelectCommand { get; set; }

        public IndexPageViewModelDesignTime()
        {
            ScenarioCards = new List<ScenarioCard>()
            {
                new ScenarioCard()
                {
                     BackgroundColor = Color.FromHex("F25022"),
                     Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",
                     ScenarioType = ScenarioType.ProductDetails,
                     Title = "Scenario Card #1",
                     Icon = ""
                },
                new ScenarioCard()
                {
                     BackgroundColor = Color.FromHex("7FBA00"),
                     Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. ",
                     ScenarioType = ScenarioType.OtherLogin,
                     Title = "Scenario Card #2",
                     Icon = ""
                },
                new ScenarioCard()
                {
                     BackgroundColor = Color.FromHex("00A4EF"),
                     Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                     ScenarioType = ScenarioType.Login,
                     Title = "Scenario Card #3",
                     Icon = ""
                },
                new ScenarioCard()
                {
                     BackgroundColor = Color.FromHex("FFB900"),
                     Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                     ScenarioType = ScenarioType.Map,
                     Title = "Scenario Card #4",
                     Icon = ""
                },
                new ScenarioCard()
                {
                     BackgroundColor = Color.FromHex("737373"),
                     Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                     ScenarioType = ScenarioType.Conversational,
                     Title = "Scenario Card #5",
                     Icon = ""
                }
            };

            SelectedItem = ScenarioCards[0];
        }

        /*private async void Selected()
        {
            if (SelectedItem == null)
                return;

            string targetPage = "map";
            switch (SelectedItem.ScenarioType)
            {
                case ScenarioType.ProductDetails:
                    targetPage = "productdetails";
                    break;

                case ScenarioType.Map:
                    targetPage = "map";
                    break;

                case ScenarioType.Login:
                    targetPage = "login";
                    break;

                case ScenarioType.OtherLogin:
                    targetPage = "otherlogin";
                    break;

                case ScenarioType.ToDoList:
                    targetPage = "todo";
                    break;

                case ScenarioType.Conversational:
                    targetPage = "conversation";
                    break;

                default:
                    break;
            }

            SelectedItem = null;
            await Shell.Current.GoToAsync($"{targetPage}");
        }
        */

        public List<ScenarioCard> ScenarioCards
        {
            get => _scenarioCards;
            set => SetProperty(ref _scenarioCards, value);
        }

        public ScenarioCard SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
    }

    public static class ViewModelLocator
    {
        static IndexPageViewModelDesignTime indexPageDesignTime;

        public static IndexPageViewModelDesignTime IndexPageViewModelDesignTime =>
           indexPageDesignTime ?? (indexPageDesignTime = new IndexPageViewModelDesignTime());
    }
}