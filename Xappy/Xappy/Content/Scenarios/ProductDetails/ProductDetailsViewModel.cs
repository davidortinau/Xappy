using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.ProductDetails
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        private ObservableCollection<string> _images;
        private int _currentImage;
        private LayoutState _mainState;

        public LayoutState MainState
        {
            get => _mainState;
            set => SetProperty(ref _mainState, value);
        }

        public ProductDetailsViewModel()
        {
            LoadImages();
            //UpdateCurrentImage();
        }

        public ObservableCollection<string> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }

        public int CurrentImage
        {
            get { return _currentImage; }
            set
            {
                _currentImage = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand => new Command(async () => await OnRefreshAsync());

        private async Task OnRefreshAsync()
        {
            MainState = LayoutState.Loading;
            IsBusy = true;
            await Task.Delay(3000);
            IsBusy = false;
            MainState = LayoutState.None;
        }

        void LoadImages()
        {
            Images = new ObservableCollection<string>
            {
                Device.RuntimePlatform == Device.UWP ? "Assets/guitar1.jpg" : "guitar1.jpg",
                Device.RuntimePlatform == Device.UWP ? "Assets/guitar2.jpg" : "guitar2.jpg",
                Device.RuntimePlatform == Device.UWP ? "Assets/guitar3.jpg" : "guitar3.jpg"
            };
        }

        void UpdateCurrentImage()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                CurrentImage++;

                if (CurrentImage == Images.Count) CurrentImage = 0;

                return true;
            });
        }
    }
}