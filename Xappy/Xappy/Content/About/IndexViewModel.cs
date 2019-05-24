using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Xappy.Content;
using Xappy.Domain.Global;

namespace Xappy.About.ViewModels
{
    public class IndexViewModel : BaseViewModel
    {
        private string _json;

        private ObservableCollection<VersionInfoItem> _versionsExpanded = new ObservableCollection<VersionInfoItem>();
        private ObservableCollection<VersionInfoItem> _versions = new ObservableCollection<VersionInfoItem>();
        public ObservableCollection<VersionInfoItem> Versions
        {
            get => _versions;
            set => SetProperty(ref _versions, value);
        }

        private VersionInfoItem _selectedVerion;
        public VersionInfoItem SelectedVerion
        {
            get => _selectedVerion;
            set
            {
                _selectedVerion = value;
                OnPropertyChanged();

                if (SelectedVerion != null)
                {
                    if (SelectedVerion.InfoType == VersionInfoType.Header)
                    {
                        if (SelectedVerion.Expanded)
                        {
                            _selectedVerion.Expanded = false;
                            foreach (var item in Versions.Where(item => item.Version == SelectedVerion.Version && item.InfoType != VersionInfoType.Header).ToList())
                                Versions.Remove(item);
                        }
                        else
                        {
                            SelectedVerion.Expanded = true;
                            var selectedIndex = Versions.IndexOf(SelectedVerion);
                            foreach (var item in _versionsExpanded.Where(item => item.Version == SelectedVerion.Version && item.InfoType != VersionInfoType.Header))
                                Versions.Insert(++selectedIndex, item);
                        }
                    }
                    SelectedVerion = null;
                }
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
                var versionInfoItemGroup = new VersionInfoItem() { Version = version.Version, Description = version.Description, InfoType = VersionInfoType.Header };
                _versionsExpanded.Add(versionInfoItemGroup);
                Versions.Add(versionInfoItemGroup);

                if(version.Features != null && version.Features.Any())
                    foreach (var feature in version.Features)
                        _versionsExpanded.Add(new VersionInfoItem() { Version = version.Version, Description = feature.Description, InfoType = VersionInfoType.Feature });

                if (version.BugFixes != null && version.BugFixes.Any())
                    foreach (var bugFix in version.BugFixes)
                        _versionsExpanded.Add(new VersionInfoItem() { Version = version.Version, Description = bugFix.Description, InfoType = VersionInfoType.BugFix });
            }
        }
    }
}