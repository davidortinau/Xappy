using PropertyChanged;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Xappy.Content.Blog
{
    [AddINotifyPropertyChangedInterface]
    public class BlogDetailViewModel
    {
        public BlogItem BlogItem { get; }

        public string Source { get; }

        public BlogDetailViewModel(string id)
        {
            BlogItem = BlogStore.BlogItems.First(x => x.Id == id);
            Source = BlogItem.FullUri;
        } 
    }
}
