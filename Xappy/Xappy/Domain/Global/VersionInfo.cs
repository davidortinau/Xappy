using System.Collections.Generic;

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

    public class VersionInfoGroup : List<VersionInfo>
    {
        public string Header { get; set; }
        public List<VersionInfoItem> Items { get; set; }
    }

    public class VersionInfoItem
    {
        public VersionInfoType InfoType { get; set; }
        public string Description { get; set; }
    }
}