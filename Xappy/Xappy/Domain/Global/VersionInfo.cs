using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Xappy.Domain.Global
{
    public enum VersionInfoType
    {
        Header,
        Feature,
        BugFix
    }

    public class VersionInfoDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HeaderTemplate { get; set; }
        public DataTemplate FeatureTemplate { get; set; }
        public DataTemplate BugFixTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate respone = HeaderTemplate;

            if (item is VersionInfoItem versionInfoItem)
            {
                switch(versionInfoItem.InfoType)
                {
                    case VersionInfoType.Header:
                        respone = HeaderTemplate;
                        break;
                    case VersionInfoType.Feature:
                        respone = FeatureTemplate;
                        break;
                    case VersionInfoType.BugFix:
                        respone = BugFixTemplate;
                        break;
                }
            }

            return respone;
        }
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
        public bool Expanded { get; set; }
        public string Version { get; set; }
        public VersionInfoType InfoType { get; set; }
        public string Description { get; set; }
    }
}