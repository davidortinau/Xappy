using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Xappy.Domain.Global;

namespace Xappy.About.ViewModels
{
    public class IndexViewModel : INotifyPropertyChanged //: BaseViewModel
    {
        private string _json;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<VersionInfo> _versions;
        public ObservableCollection<VersionInfo> Versions
        {
            get => _versions;
            set
            {
                _versions = value;
                OnPropertyChanged();
            }
        }

        public IndexViewModel()
        {
            if (string.IsNullOrEmpty(_json))
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(IndexViewModel)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("Xappy.AppVersionInfo.json");
                using (var reader = new StreamReader(stream))
                    _json = reader.ReadToEnd();
            }

            Versions = new ObservableCollection<VersionInfo>(JsonConvert.DeserializeObject<List<VersionInfo>>(_json));
        }
    }
}