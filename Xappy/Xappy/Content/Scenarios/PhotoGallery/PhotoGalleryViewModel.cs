using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xappy.Domain.Models;

namespace Xappy.Content.Scenarios.PhotoGallery
{
    public class PhotoGalleryViewModel : BaseViewModel
    {
        private ObservableCollection<Photo> _photos;

        private ObservableCollection<object> _selectedPhotos;

        private string[] _guitars = new string[] { "guitar1.jpg", "guitar2.jpg", "guitar3.jpg" };
        
        private SelectionMode _selectionMode = SelectionMode.None;


        public Photo SelectedItem { get; set; }

        public SelectionMode SelectionMode { get => _selectionMode; set => SetProperty(ref _selectionMode, value); }

        public ObservableCollection<Photo> Photos { get => _photos; set => _photos = value; }

        public ObservableCollection<object> SelectedPhotos { get => _selectedPhotos; set => _selectedPhotos = value; }

        public Command ShareCommand { get; set; }

        public Command<Photo> LongPressCommand { get; private set;}

        public Command ClearCommand { get; private set; }

        public Command<Photo> PressedCommand { get; private set; }

        public PhotoGalleryViewModel()
        {
            InitData();

            ShareCommand = new Command(OnShare);
            LongPressCommand = new Command<Photo>(OnLongPress);
            ClearCommand = new Command(OnClear);
            PressedCommand = new Command<Photo>(OnPressed);
        }

        private void OnPressed(Photo obj)
        {
            if (_selectionMode != SelectionMode.None)
            { 
                Debug.WriteLine($"Added {obj.ImageSrc}");
                if(_selectedPhotos.Contains(obj))
                    SelectedPhotos.Remove(obj);
                else
                    SelectedPhotos.Add(obj);
            }
            else
            {
                Shell.Current.GoToAsync($"photo?src={obj.ImageSrc}");
            }
        }

        private void OnClear()
        {
            SelectionMode = SelectionMode.None;
        }

        private void OnLongPress(Photo obj)
        {
            Debug.WriteLine("LongPressed");
            if(_selectionMode == SelectionMode.None)
            {
                SelectionMode = SelectionMode.Multiple;
                SelectedPhotos.Add(obj);
            }
        }

        private async void OnShare()
        {
            var files = new List<ShareFile>();
            foreach (var obj in SelectedPhotos)
            {
                if (obj is Photo photo)
                {
                    var path = Path.Combine(FileSystem.CacheDirectory, photo.ImageSrc);
                    if (File.Exists(path)) { // it's a cache dir, so delete it
                        File.Delete(path);
                    }
                    var newFile = File.Create(path);
                    using (var stream = await FileSystem.OpenAppPackageFileAsync(photo.ImageSrc))
                    {
                        await stream.CopyToAsync(newFile);    
                    }

                    files.Add(new ShareFile(path));
                    System.Diagnostics.Debug.WriteLine($">>>>>>photo {photo.Id} {photo.ImageSrc} selected in OnShare. Exists {File.Exists(path)} at {path}");
                }
            }

            await Share.RequestAsync(new ShareMultipleFilesRequest
            {
                Title = "Photos",
                Files = files
            });

            SelectedPhotos.Clear();
        }

        

        private void InitData()
        {
            _selectedPhotos = new ObservableCollection<object>();

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
