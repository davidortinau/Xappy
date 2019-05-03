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
    public partial class BlogView
    {
        public BlogView()
        {
            InitializeComponent();
        }

        //protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        //{ 
        //    if (BindingContext is BlogItem blog && Parent is  CollectionView collection)
        //    { 
        //        var size = new Size(collection.Width / 2, blog.Height); 
        //        if(blog.WholeScreen)
        //        {
        //            size.Width = collection.Width;
        //        }
        //        return new SizeRequest(size, size);
        //    }
        //    return base.OnMeasure(widthConstraint, heightConstraint);
        //}
    }
}