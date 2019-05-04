using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xappy.Content.Blog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("BlogId", "id")]
    public partial class BlogDetailPage : ContentPage
    {
        public BlogDetailPage()
        {
            InitializeComponent();
        }

        public string BlogId
        {
            set
            {
                BindingContext = new BlogDetailViewModel(value);
            }
        }
    }
}