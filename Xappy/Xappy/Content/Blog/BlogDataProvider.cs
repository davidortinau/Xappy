using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml.Linq;
using System.Diagnostics;

namespace Xappy.Content.Blog
{
    public class BlogDataProvider
    {
        public async Task<List<BlogItem>> GetItems()
        {
            try
            {
                var sw = new Stopwatch();
                sw.Start();
                var client = new HttpClient();
                var response = await client.GetAsync("https://devblogs.microsoft.com/xamarin/feed/");

                var items = new List<BlogItem>();
                var rssStream = await response.Content.ReadAsStreamAsync();
                using (var xmlReader = XmlReader.Create(rssStream, new XmlReaderSettings() { Async = true }))
                {
                    var feed = SyndicationFeed.Load(xmlReader);
                    foreach (var item in feed.Items)
                    {
                        var contentElement = item.ElementExtensions.First(x => x.OuterNamespace == "http://purl.org/rss/1.0/modules/content/");
                        var content = contentElement.GetObject<XElement>().Value;
                        var creatorElement = item.ElementExtensions.First(x => x.OuterName == "creator");
                        var creator = creatorElement.GetObject<XElement>().Value;
                        var imageUriMatches = Regex.Match(content, "src=\"(?<image>.+(?:\\.jpg|\\.png))\"");
                        var imageUri = imageUriMatches.Groups["image"].Value;
                        var blogItem = new BlogItem
                        {
                            Id = Guid.NewGuid().ToString(),
                            Title = item.Title.Text,
                            Content = content,
                            ImageUri = imageUri.Replace("http://", "https://"), 
                            FullUri = item.Links.First().Uri.ToString(),
                            Author = creator,
                            LastEditDate = item.LastUpdatedTime.LocalDateTime,
                            PublishDate = item.PublishDate.LocalDateTime
                        };
                        items.Add(blogItem);
                    }
                }
                sw.Stop();
                BlogStore.BlogItems.Clear();
                BlogStore.BlogItems.AddRange(items);
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
