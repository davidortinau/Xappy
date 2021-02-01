using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xappy.Content.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutFooterTemplate : Grid
    {
        public Command DiscordCommand { get;set;}
        public Command GithubCommand { get; set; }

        public FlyoutFooterTemplate()
        {
            BindingContext = this;

            InitializeComponent();

            DiscordCommand = new Command(OnDiscord);
            GithubCommand = new Command(OnGithub);
        }

        async private void OnGithub()
        {
            var result = await Launcher.TryOpenAsync("https://github.com/davidortinau/Xappy");
        }

        async private void OnDiscord()
        {
            var result = await Launcher.TryOpenAsync("https://discord.gg/pN5jMhr");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("settings");
        }

        async private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var result = await Launcher.TryOpenAsync("https://discord.gg/pN5jMhr");
        }

        async private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var result = await Launcher.TryOpenAsync("https://github.com/davidortinau/Xappy");
        }

    }
}