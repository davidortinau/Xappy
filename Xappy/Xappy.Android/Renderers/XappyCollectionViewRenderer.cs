using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xappy.CustomViews;

[assembly: ExportRenderer(typeof(CustomCollectionView), typeof(Xappy.Droid.Renderers.XappyCollectionViewRenderer))]
namespace Xappy.Droid.Renderers
{
    public class XappyCollectionViewRenderer : CollectionViewRenderer
    {
        public XappyCollectionViewRenderer(Context context) : base(context)
        {
        }

        protected override void SetUpNewElement(ItemsView newElement)
        {
            base.SetUpNewElement(newElement);
            SetClipToPadding(false);
            SetPadding(10, 10, 10, 10);
        }

        protected override LayoutManager SelectLayoutManager(IItemsLayout layoutSpecification)
        {
            switch (layoutSpecification)
            {
                case GridItemsLayout gridItemsLayout: 
                    var gridOrientation = gridItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal ?
                        StaggeredGridLayoutManager.Horizontal :
                        StaggeredGridLayoutManager.Vertical;
                    return new StaggeredGridLayoutManager(gridItemsLayout.Span, gridOrientation); 
                case ListItemsLayout listItemsLayout:
                    var orientation = listItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal
                        ? LinearLayoutManager.Horizontal
                        : LinearLayoutManager.Vertical;

                    return new LinearLayoutManager(Context, orientation, false);
            }

            // Fall back to plain old vertical list
            // TODO hartez 2018/08/30 19:34:36 Log a warning when we have to fall back because of an unknown layout	
            return new LinearLayoutManager(Context);
        } 
    }
}