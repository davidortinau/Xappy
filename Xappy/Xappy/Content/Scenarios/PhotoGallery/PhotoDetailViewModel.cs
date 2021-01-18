using System;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.PhotoGallery
{
    [QueryProperty("ImageSrc", "src")]
    public class PhotoDetailViewModel : BaseViewModel
    {
        private string _imageSrc;

        public string ImageSrc { get => _imageSrc;
            set => SetProperty(ref _imageSrc, value); }

        public PhotoDetailViewModel()
        {
        }
    }
}
