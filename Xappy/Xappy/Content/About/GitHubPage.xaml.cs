
using Xamarin.Forms;
using Xappy.About.ViewModels;

namespace Xappy.About
{
    public partial class GitHubPage : ContentPage
    {
        public GitHubPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((GitHubViewModel)BindingContext).OnAppearing();
        }
    }
}