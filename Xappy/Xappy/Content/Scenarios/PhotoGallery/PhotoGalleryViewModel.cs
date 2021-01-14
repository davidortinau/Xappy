using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xappy.Domain.Models;

namespace Xappy.Content.Scenarios.PhotoGallery
{
    public class PhotoGalleryViewModel : BaseViewModel
    {
        private ObservableCollection<Photo> _photos;

        public Photo SelectedItem { get;set;}

        public ObservableCollection<Photo> Photos { get => _photos; set => _photos = value; }

        public ObservableCollection<Photo> SelectedPhotos { get => _selectedPhotos; set => _selectedPhotos = value; }

        public Command ShareCommand { get;set;}

        public Command<Photo> SelectionChangedCommand { get;set;}

        public PhotoGalleryViewModel()
        {
            InitData();

            ShareCommand = new Command(OnShare);
            SelectionChangedCommand = new Command<Photo>(OnSelectionChanged);
        }

        private void OnSelectionChanged(object param)
        {
            Debug.WriteLine("selected");
        }

        private async void OnShare()
        {
            if(SelectedPhotos == null || SelectedPhotos.Count <= 0)
            {
                return;
            }

            //var files = new List<ShareFile>();
            //foreach (var p in SelectedPhotos)
            //{
            //    files.Add(new ShareFile(p.ImageSrc));
            //}

            //await Share.RequestAsync(new ShareMultipleFilesRequest
            //{
            //    Title = "Photos",
            //    Files = files
            //});

            SelectedPhotos.Clear();
        }

        private string[] _guitars = new string[] { "guitar1.jpg", "guitar2.jpg", "guitar3.jpg" };
        private ObservableCollection<Photo> _selectedPhotos;

        private void InitData()
        {
            _selectedPhotos = new ObservableCollection<Photo>();

            Random rand = new Random();
            _photos = new ObservableCollection<Photo>();
            for (int i = 0; i < 30; i++)
            {
                _photos.Add(
                    new Photo
                    {
                        ImageSrc = _guitars[rand.Next(_guitars.Length)],
                        Id = i
                    }
                );
            }

            OnPropertyChanged(nameof(Photos));
        }
    }


}
