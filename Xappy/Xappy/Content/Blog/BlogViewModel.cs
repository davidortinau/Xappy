
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xappy.Content.Blog
{
    public class BlogViewModel : BaseViewModel
    {
        private bool isRefreshing;
        public bool IsRefreshing{
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        public Command RefreshCommand { get; set; }

        public ObservableCollection<BlogItem> Items { get; } = new ObservableCollection<BlogItem>();

        public BlogViewModel()
        {
            SelectCommand = new Command(async () => await Selected());
            RefreshCommand = new Command(() => OnRefresh());
        
            Task.Run(LoadData);
        }

        private void OnRefresh()
        {
            Task.Run(LoadData);
        }

        public ICommand SelectCommand { get; set; }

        private async Task Selected()
        {
            if (SelectedItem == null)
                return;
            var item = SelectedItem;
            SelectedItem = null;
            await Shell.Current.GoToAsync($"blogDetail?id={item.Id}");
        }

        public BlogItem SelectedItem { get; set; }
        

        public async Task LoadData()
        {
            IsRefreshing = true;
            var dataProvider = new BlogDataProvider();
            var items = await dataProvider.GetItems();
            Device.BeginInvokeOnMainThread(() =>
            {
                var random = new Random();
                items = items.OrderByDescending(x => x.PublishDate).ToList();
                Items.Clear();
                items.ForEach(x =>
                {
                    x.Height = 200 + random.Next(0, 2) * 60;
                    Items.Add(x);
                });
            }
            );
            IsRefreshing = false;
        }
    }
}
