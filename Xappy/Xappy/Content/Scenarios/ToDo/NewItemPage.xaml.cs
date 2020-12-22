using System;

using Xamarin.Forms;

namespace Xappy.Content.Scenarios.ToDo
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item();

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.Current.Navigating += Current_Navigating;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Shell.Current.Navigating -= Current_Navigating;
        }

        private async void Current_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            var deferral = e.GetDeferral(); // hey shell, wait a moment

            if(!string.IsNullOrEmpty(Item.Text) || !string.IsNullOrEmpty(Item.Description))
            {
                var answer = await DisplayAlert(
                    "Are you sure?",
                    "Looks like you made changes. If you leave, ALL will be lost.",
                    "Leave",
                    "Cancel"
                    );

                if(answer == false)
                {
                    e.Cancel();// don't navigate away
                }
            }

            deferral.Complete();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Shell.Current.Navigating -= Current_Navigating;// don't need to check, we are saving
            MessagingCenter.Send(this, "AddItem", Item);
            await Shell.Current.GoToAsync("..?saved");
        }
    }
}