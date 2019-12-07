using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xappy.Scenarios;

namespace Xappy.Scenarios
{
    public partial class IndexPage : ContentPage
    {
        public IndexPage()
        {
            InitializeComponent();

            BindingContext = new IndexPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }

    public class ScenarioDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HalfTemplate { get; set; }

        public DataTemplate WholeTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var c = (FlexLayout)container;
            if ((c.Children.Count % 3) == 0)
            {
                return WholeTemplate;

            }
            else
            {
                return HalfTemplate;
            }
        }
    }
}
