using System;
using System.Collections.Generic;
using System.Text;

namespace Xappy.Content.Blog
{
    public class BlogViewModel
    {
        public List<BlogItem> Items { get; } = new List<BlogItem>();

        public BlogViewModel()
        {
            for (int i = 0; i < 20; i++)
            {
                var modifier = (i % 5) + 1;
                bool wholeScreen = i % 5 == 4;
                Items.Add(new BlogItem
                {
                    Name = i + "Test Test Test Test" + (wholeScreen ? " whole " : " half "),
                    Height = modifier * 20 + 40,
                    WholeScreen = wholeScreen
                });
            }
        }
    }

    public class BlogItem
    {
        public string Name { get; set; }

        public int Height { get; set; }

        public bool WholeScreen { get; set; }
    }
}
