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
        public ICommand SkeletonCommand { get; set; }

        public ProductDetailsViewModel()
        {
            SkeletonCommand = new Command(async (x) =>
            {
                IsBusy = true;
                await Task.Delay(4000);
                IsBusy = false;
            });
        }

    }
}
