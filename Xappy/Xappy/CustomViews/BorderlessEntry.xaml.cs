using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xappy.CustomViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BorderlessEntry : Entry
	{
		public BorderlessEntry ()
		{
			InitializeComponent ();
		}
	}
}