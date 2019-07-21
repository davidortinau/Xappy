using System;

namespace Xappy.Content.Blog
{
    public class BlogItem
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public string ImageUri { get; set; }

        public string FullUri { get; set; }

        public int Height { get; set; }

        public bool WholeScreen { get; set; }

        
    }
}
