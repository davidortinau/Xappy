using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
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

        public List<ControlGroup> XamarinAll { get; set; }

        public IndexPageViewModel()
        {
            var layouts = new ControlGroup("Layouts", new List<ControlType>
            {
                new ControlType
                {
                    Title = nameof(StackLayout), ControlTemplate = nameof(StackLayoutControlTemplate),
                    Icon = "layout-StackLayout"
                },
                new ControlType{ Title = nameof(Frame), Icon = "layout-Frame"},
                new ControlType{ Title = nameof(Grid), Icon = "layout-Grid"},
                new ControlType{ Title = nameof(FlexLayout), Icon = "layout-FlexLayout"},
                new ControlType{ Title = nameof(AbsoluteLayout), Icon = "layout-AbsoluteLayout"},
                new ControlType{ Title = nameof(RelativeLayout), Icon = "layout-RelativeLayout"},
                // new ControlType{ Title = nameof(ContentPresenter)},
                // new ControlType{ Title = nameof(ContentView)},
                new ControlType{ Title = nameof(ScrollView), Icon = "layout-ScrollView"},
                // new ControlType{ Title = nameof(TemplatedView)}
            });;

            var pages = new ControlGroup("Pages", new List<ControlType>
            {
                new ControlType{ Title = nameof(ContentPage), GroupIndex = 1},
                new ControlType{ Title = nameof(TabbedPage), GroupIndex = 1},
                new ControlType{ Title = nameof(MasterDetailPage), GroupIndex = 1},
                new ControlType{ Title = nameof(NavigationPage), GroupIndex = 1},
                new ControlType{ Title = nameof(CarouselPage), GroupIndex = 1}

            });

            var views = new ControlGroup("Views", new List<ControlType>
            {
                new ControlType{ Title = nameof(ActivityIndicator), GroupIndex = 2, Icon = "view-ActivityIndicator"},
                new ControlType{ Title = nameof(Button), GroupIndex = 2, Icon = "view-Button"},
                new ControlType{ Title = nameof(Editor), GroupIndex = 2, Icon = "view-Editor"},
                new ControlType{ Title = nameof(Entry), GroupIndex = 2, Icon = "view-Entry"},
                new ControlType{ Title = nameof(ListView), GroupIndex = 2, Icon = "view-ListView"},
                new ControlType{ Title = nameof(CollectionView), GroupIndex = 2, Icon = "view-ListView"},
                new ControlType{ Title = nameof(DatePicker), GroupIndex = 2, Icon = "view-DatePicker"},
                new ControlType{ Title = nameof(TimePicker), GroupIndex = 2, Icon = "view-TimePicker"},
                new ControlType{ Title = nameof(Picker), GroupIndex = 2, Icon = "view-Picker"},
                new ControlType{ Title = nameof(Stepper), GroupIndex = 2, Icon = "view-Stepper"},
                new ControlType{ Title = nameof(BoxView), GroupIndex = 2, Icon = "view-BoxView"},
                new ControlType{ Title = nameof(Switch), GroupIndex = 2, Icon = "view-Switch"},
                new ControlType{ Title = nameof(ProgressBar), GroupIndex = 2, Icon = "view-ProgressBar"},
                new ControlType{ Title = nameof(Label), GroupIndex = 2, Icon = "view-Label"},
                new ControlType{ Title = nameof(Image), GroupIndex = 2, Icon = "view-Image"},
                new ControlType{ Title = nameof(ImageButton), GroupIndex = 2},
                new ControlType{ Title = nameof(SearchBar), GroupIndex = 2, Icon = "view-SearchBar"},
                new ControlType{ Title = nameof(WebView), GroupIndex = 2, Icon = "view-WebView"},
                new ControlType{ Title = nameof(Map), GroupIndex = 2, Icon = "view-Map"},
                new ControlType{ Title = nameof(RadioButton), GroupIndex = 2},
                new ControlType{ Title = nameof(CheckBox), GroupIndex = 2},
                new ControlType{ Title = nameof(CarouselView), GroupIndex = 2},
                new ControlType{ Title = nameof(IndicatorView), GroupIndex = 2},
                new ControlType{ Title = nameof(RefreshView), GroupIndex = 2},
                new ControlType{ Title = nameof(SwipeView), GroupIndex = 2}


            });

            XamarinAll = new List<ControlGroup>();
            XamarinAll.Add(layouts);
            XamarinAll.Add(views);
            XamarinAll.Add(pages);


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

    public class ControlGroup : List<ControlType>
    {
        public ControlGroup(string name, List<ControlType> members) : base(members)
        {
            Name = name;
        }

        public string Name { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }

    public class ControlType
    {
        public string Title { get; set; }
        public string ControlTemplate { get; set; }
        public int GroupIndex { get; set; } = 0;
        
        public string Icon { get; set; }
    }


}