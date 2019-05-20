using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xappy.Content;
using Xappy.Content.ControlGallery.ControlTemplates;

namespace Xappy.ControlGallery
{
    public class IndexPageViewModel : BaseViewModel
    {
        public ControlType SelectedControl { get; set; }

        public ICommand SelectCommand { get; set; }

        public List<ControlType> Layouts { get; set; }

        public List<ControlType> Pages { get; set; }

        public List<ControlType> Views { get; set; }

        public IndexPageViewModel()
        {
            Layouts = new List<ControlType>
            {
                new ControlType{ Title = nameof(StackLayout), ControlTemplate = nameof(StackLayoutControlTemplate) },
                new ControlType{ Title = nameof(Frame)},
                new ControlType{ Title = nameof(Grid)},
                new ControlType{ Title = nameof(FlexLayout)},
                new ControlType{ Title = nameof(AbsoluteLayout)},
                new ControlType{ Title = nameof(RelativeLayout)},
                new ControlType{ Title = nameof(ContentPresenter)},
                new ControlType{ Title = nameof(ContentView)},
                new ControlType{ Title = nameof(ScrollView)},
                new ControlType{ Title = nameof(TemplatedView)}
            };

            Pages = new List<ControlType>
            {
                new ControlType{ Title = nameof(ContentPage)},
                new ControlType{ Title = nameof(TabbedPage)},
                new ControlType{ Title = nameof(MasterDetailPage)},
                new ControlType{ Title = nameof(NavigationPage)},
                new ControlType{ Title = nameof(CarouselPage)}

            };

            Views = new List<ControlType>
            {
                new ControlType{ Title = nameof(ActivityIndicator)},
                new ControlType{ Title = nameof(Button)},
                new ControlType{ Title = nameof(Editor)},
                new ControlType{ Title = nameof(Entry)},
                new ControlType{ Title = nameof(ListView)},
                new ControlType{ Title = nameof(CollectionView)},
                new ControlType{ Title = nameof(DatePicker)},
                new ControlType{ Title = nameof(TimePicker)},
                new ControlType{ Title = nameof(Picker)},
                new ControlType{ Title = nameof(Stepper)},
                new ControlType{ Title = nameof(BoxView)},
                new ControlType{ Title = nameof(Switch)},
                new ControlType{ Title = nameof(ProgressBar)},
                new ControlType{ Title = nameof(Label)},
                new ControlType{ Title = nameof(Image)},
                new ControlType{ Title = nameof(ImageButton)},
                new ControlType{ Title = nameof(SearchBar)},
                new ControlType{ Title = nameof(WebView)}


            };


            SelectCommand = new Command(ControlSelected);
        }

        private async void ControlSelected()
        {
            // navigate to the control page
            if (SelectedControl == null)
                return;

            await Shell.Current.GoToAsync($"control?control={SelectedControl.Title}&template={SelectedControl.ControlTemplate}");

            SelectedControl = null;
        }
    }

    public class ControlType
    {
        public string Title { get; set; }
        public string ControlTemplate { get; set; }
    }
}