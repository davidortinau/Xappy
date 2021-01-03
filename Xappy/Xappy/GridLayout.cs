using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Xappy
{
    public class GridLayout : Layout<View>
    {
        double childWidth;

        double childHeight;

        private int cellCount;

        public int CellCount
        {
            get { return cellCount; }
            set { cellCount = value; }
        }

        private StackOrientation orientation = StackOrientation.Vertical;

        public StackOrientation Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        private int cellSpacing;

        public int CellSpacing
        {
            get { return cellSpacing; }
            set { cellSpacing = value; }
        }


        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            Measure(width, height, 0);
            //var columns = GetColumnsCount(Children.Count, width, childWidth);
            //var rows = GetRowsCount(Children.Count, columns);

            int columns = 0;
            int rows = 0;
            double boundsWidth;
            double boundsHeight;
            Rectangle bounds;
            int count = 0;
            int primaryAxis;
            int secondaryAxis;

            if (orientation == StackOrientation.Horizontal)
            {
                rows = cellCount;
                columns = Children.Count / cellCount;
                boundsWidth = childWidth;
                boundsHeight = (height / rows);
                primaryAxis = rows;
                secondaryAxis = columns;
            }
            else
            {
                rows = Children.Count / cellCount;
                columns = cellCount;
                boundsWidth = (width / columns);
                boundsHeight = childHeight;
                primaryAxis = columns;
                secondaryAxis = rows;
            }

            Debug.WriteLine($"BoundsWidth: {boundsWidth}, BoundsHeight: {boundsHeight}");

            bounds = new Rectangle(0, 0, boundsWidth, boundsHeight);

            for (var i = 0; i < primaryAxis; i++)
            {
                bounds.Y = (i * boundsHeight) + ((i + 1) * cellSpacing);
                Debug.WriteLine($"Y: {bounds.Y}");
                for (var j = 0; j < secondaryAxis && count < Children.Count; j++)
                {
                    var item = Children[count];
                    bounds.X = (j * boundsWidth) + ((j+1) * cellSpacing);
                    item.Layout(bounds);
                    count++;
                    Debug.WriteLine($"X: {bounds.X}");
                }
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            foreach (var child in Children)
            {
                if (!child.IsVisible)
                    continue;

                var sizeRequest = child.Measure(double.PositiveInfinity, double.PositiveInfinity, 0);
                var minimum = sizeRequest.Minimum;
                var request = sizeRequest.Request;

                childHeight = Math.Max(minimum.Height, (request.Height - (cellSpacing * 2)));
                childWidth = Math.Max(minimum.Width, (request.Width - (cellSpacing * 2)));
            }

            int columns = 0;
            int rows = 0;

            if (orientation == StackOrientation.Horizontal)
            {
                rows = cellCount;
                columns = Children.Count / cellCount;
            }
            else
            {
                rows = Children.Count / cellCount;
                columns = cellCount;
            }
            var size = new Size((columns * childWidth)+((columns+1) * cellSpacing), (rows * childHeight)+((rows+1)*cellSpacing));
            return new SizeRequest(size, size);
        }

    }
}
