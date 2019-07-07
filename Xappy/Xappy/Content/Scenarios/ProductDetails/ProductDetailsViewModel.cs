using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xappy.Content.Scenarios.ProductDetails
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        public ICommand SkeletonCommand = new Command(async (x) =>
            {
            IsSkeletonLoading = true;
            await Task.Delay(2000);
            IsSkeletonLoading = false;
        });

        public static bool IsSkeletonLoading { get; private set; }
    }
}
