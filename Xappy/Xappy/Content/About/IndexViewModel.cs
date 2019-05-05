using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
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

        private ObservableCollection<VersionInfoGroup> _versions = new ObservableCollection<VersionInfoGroup>();
        public ObservableCollection<VersionInfoGroup> Versions
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

            var versions = new ObservableCollection<VersionInfo>(JsonConvert.DeserializeObject<List<VersionInfo>>(_json));
            foreach(var version in versions)
            {
                var versionInfo = new VersionInfoGroup() { Header = version.Version, Items = new List<VersionInfoItem>() };
                if (version.Features != null && version.Features.Any())
                {
                    foreach (var feature in version.Features)
                        versionInfo.Items.Add(new VersionInfoItem() { InfoType = VersionInfoType.Feature, Description = feature.Description });
                }
                if (version.BugFixes != null && version.BugFixes.Any())
                {
                    foreach (var bugFix in version.BugFixes)
                        versionInfo.Items.Add(new VersionInfoItem() { InfoType = VersionInfoType.BugFix, Description = bugFix.Description });
                }

                Versions.Add(versionInfo);
            }
        }
    }
}