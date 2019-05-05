using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Xappy.Domain.Global
{
    public enum VersionInfoType
    {
        Feature,
        BugFix
    }

    public class Feature
    {
        public string Description { get; set; }
    }

    public class BugFix
    {
        public string Description { get; set; }
    }

    public class VersionInfo
    {
        public string Version { get; set; }
        public string Description { get; set; }

        public List<Feature> Features { get; set; }
        public List<BugFix> BugFixes { get; set; }
    }

    public class VersionInfoItem
    {
        public VersionInfoType InfoType { get; set; }
        public string Description { get; set; }
    }

    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                Items.Add(item);
        }
    }
}