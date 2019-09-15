using CSharpForMarkup;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Content;

namespace Xappy.Scenarios
{
    public class IndexPageViewModel : BaseViewModel
    {
        private ScenarioCard _selectedItem;
        private List<ScenarioCard> _scenarioCards;

        public ICommand SelectCommand { get; set; }

        public IndexPageViewModel()
        {
            ScenarioCards = BuildScenarioCards();
            SelectCommand = new Command(Selected);
        }

        private async void Selected()
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

        private List<ScenarioCard> BuildScenarioCards()
        {
            return new List<ScenarioCard>()
                {
                    new ScenarioCard()
                    {
                        BackgroundColor = App.LookupColor("dusty_orange"),
                        Description = "Rockin' product details layout",
                        ScenarioType = ScenarioType.ProductDetails,
                        Title = "Product Details",
                        Icon = FabIconFont.GiftboxOpen.ToXamlIconStr()
                    },
                    new ScenarioCard()
                    {
                        BackgroundColor = App.LookupColor("dark_lime_green"),
                        Description = "Standard login and registration page with professional animations and circle avatar",
                        ScenarioType = ScenarioType.Login,
                        Title = "Login",
                        Icon = FabIconFont.UserFollowed.ToXamlIconStr()
                    },
                    new ScenarioCard()
                    {
                        BackgroundColor = App.LookupColor("very_light_pink"),
                        Description = "Beautiful GitHub themed login and registration page with lottie animation",
                        ScenarioType = ScenarioType.OtherLogin,
                        Title = "Other Login",
                        Icon = FabIconFont.UserFollowed.ToXamlIconStr()
                    },
                    new ScenarioCard()
                    {
                        BackgroundColor = App.LookupColor("soft_blue"),
                        Description = "Map example, requires Google Play Services.",
                        ScenarioType = ScenarioType.Map,
                        Title = "Map",
                        Icon = FabIconFont.MapPin.ToXamlIconStr()
                    },
                    new ScenarioCard()
                    {
                        BackgroundColor = App.LookupColor("dark_peach"),
                        Description = "Super-slick Messaging UI",
                        ScenarioType = ScenarioType.Conversational,
                        Title = "Conversational",
                        Icon = FabIconFont.SkypeMessage.ToXamlIconStr()
                    }
                };
        }
    }

    public class ScenarioCard
    {
        public ScenarioType ScenarioType { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        public Color BackgroundColor { get; set; }
    }

    public enum ScenarioType
    {
        ProductDetails,
        Map,
        Conversational,
        Login,
        OtherLogin,
        ToDoList
    }
}