using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Xappy.Content.ControlGallery
{
    public static class XamlUtil
    {
        static readonly Dictionary<Type, string> XamlResources = new Dictionary<Type, string>();


        public static string GetXamlForType(Type type)
        {

            var assembly = type.GetTypeInfo().Assembly;

            string resourceId;
            string xaml = string.Empty;
            if (XamlResources.TryGetValue(type, out resourceId))
            {
                var result = ReadResourceAsXaml(type, assembly, resourceId);
                if (result != null)
                    return result;
            }

            var likelyResourceName = type.Name + ".xaml";
            var resourceNames = assembly.GetManifestResourceNames();
            string resourceName = null;

            // first pass, pray to find it because the user named it correctly

            foreach (var resource in resourceNames)
            {
                if (ResourceMatchesFilename(assembly, resource, likelyResourceName))
                {
                    resourceName = resource;
                    xaml = ReadResourceAsXaml(type, assembly, resource);
                    if (xaml != null)
                        goto end;
                }
            }

            // okay maybe they at least named it .xaml

            foreach (var resource in resourceNames)
            {
                if (!resource.EndsWith(".xaml", StringComparison.OrdinalIgnoreCase))
                    continue;

                resourceName = resource;
                xaml = ReadResourceAsXaml(type, assembly, resource);
                if (xaml != null)
                    goto end;
            }

            foreach (var resource in resourceNames)
            {
                if (resource.EndsWith(".xaml", StringComparison.OrdinalIgnoreCase))
                    continue;

                resourceName = resource;
                xaml = ReadResourceAsXaml(type, assembly, resource, true);
                if (xaml != null)
                    goto end;
            }

        end:
            if (string.IsNullOrEmpty(xaml))
                return null;

            XamlResources[type] = resourceName;
            return xaml;
            //var alternateXaml = ResourceLoader.ResourceProvider?.Invoke(resourceName);
            //return alternateXaml ?? xaml;
        }

        static bool ResourceMatchesFilename(Assembly assembly, string resource, string filename)
        {
            try
            {
                var info = assembly.GetManifestResourceInfo(resource);

                if (!string.IsNullOrEmpty(info.FileName) &&
                    string.Compare(info.FileName, filename, StringComparison.OrdinalIgnoreCase) == 0)
                    return true;
            }
            catch (PlatformNotSupportedException)
            {
                // Because Win10 + .NET Native
            }

            if (resource.EndsWith("." + filename, StringComparison.OrdinalIgnoreCase) ||
                string.Compare(resource, filename, StringComparison.OrdinalIgnoreCase) == 0)
                return true;

            return false;
        }

        static string ReadResourceAsXaml(Type type, Assembly assembly, string likelyTargetName, bool validate = false)
        {
            using (var stream = assembly.GetManifestResourceStream(likelyTargetName))
            using (var reader = new StreamReader(stream))
            {
                if (validate)
                {
                    // terrible validation of XML. Unfortunately it will probably work most of the time since comments
                    // also start with a <. We can't bring in any real deps.

                    var firstNonWhitespace = (char)reader.Read();
                    while (char.IsWhiteSpace(firstNonWhitespace))
                        firstNonWhitespace = (char)reader.Read();

                    if (firstNonWhitespace != '<')
                        return null;

                    stream.Seek(0, SeekOrigin.Begin);
                }

                var xaml = reader.ReadToEnd();

                var pattern = String.Format("x:Class *= *\"{0}\"", type.FullName);
                var regex = new Regex(pattern, RegexOptions.ECMAScript);
                if (regex.IsMatch(xaml) || xaml.Contains(String.Format("x:Class=\"{0}\"", type.FullName)))
                    return xaml;
            }
            return null;
        }
    }
}
