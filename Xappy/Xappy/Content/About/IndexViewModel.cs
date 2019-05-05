using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Xamarin.Forms;
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

        private ObservableCollection<Grouping<VersionInfo, VersionInfoItem>> _versionsExpanded = new ObservableCollection<Grouping<VersionInfo, VersionInfoItem>>();
        private ObservableCollection<Grouping<VersionInfo, VersionInfoItem>> _versions = new ObservableCollection<Grouping<VersionInfo, VersionInfoItem>>();
        public ObservableCollection<Grouping<VersionInfo, VersionInfoItem>> Versions
        {
            get => _versions;
            set
            {
                _versions = value;
                OnPropertyChanged();
            }
        }

        private Command<Grouping<VersionInfo, VersionInfoItem>> _headerSelectedCommand;
        public Command<Grouping<VersionInfo, VersionInfoItem>> HeaderSelectedCommand => _headerSelectedCommand ?? (_headerSelectedCommand = new Command<Grouping<VersionInfo, VersionInfoItem>>(obj => HeaderSelected(obj)));

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
                var versionInfoItems = new List<VersionInfoItem>();
                if (version.Features != null && version.Features.Any())
                {
                    foreach (var feature in version.Features)
                        versionInfoItems.Add(new VersionInfoItem() { InfoType = VersionInfoType.Feature, Description = feature.Description });
                }
                if (version.BugFixes != null && version.BugFixes.Any())
                {
                    foreach (var bugFix in version.BugFixes)
                        versionInfoItems.Add(new VersionInfoItem() { InfoType = VersionInfoType.BugFix, Description = bugFix.Description });
                }

                _versionsExpanded.Add(new Grouping<VersionInfo, VersionInfoItem>(version, versionInfoItems));
            }

            foreach (var group in _versionsExpanded)
                Versions.Add(new Grouping<VersionInfo, VersionInfoItem>(group.Key, new List<VersionInfoItem>()));
        }

        private void HeaderSelected(Grouping<VersionInfo, VersionInfoItem> selectedGroup)
        {
            if (selectedGroup.Any())
                selectedGroup.Clear();
            else
            {
                foreach (var item in _versionsExpanded.FirstOrDefault(i => i.Key == selectedGroup.Key))
                    selectedGroup.Add(item);
            }
        }
    }
}