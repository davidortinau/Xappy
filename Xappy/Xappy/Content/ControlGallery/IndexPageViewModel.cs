using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xappy.Content;
using Xappy.Content.ControlGallery.ControlTemplates;

namespace Xappy.ControlGallery
{
    public class IndexPageViewModel : BaseViewModel
    {
        public Command NavToDetailCommand { get;set;}

        public ControlType SelectedControl { get; set; }

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
                    Icon = "layoutStackLayout.png"
                },
                new ControlType{ Title = nameof(Frame), Icon = "layout-Frame.png"},
                new ControlType{ Title = nameof(Grid), Icon = "layout-Grid.png"},
                new ControlType{ Title = nameof(FlexLayout), Icon = "layout-FlexLayout.png"},
                new ControlType{ Title = nameof(AbsoluteLayout), Icon = "layout-AbsoluteLayout.png"},
                new ControlType{ Title = nameof(RelativeLayout), Icon = "layout-RelativeLayout.png"},
                new ControlType{ Title = nameof(ScrollView), Icon = "layout-ScrollView.png"},
                
            });

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
                new ControlType{ Title = nameof(ActivityIndicator), GroupIndex = 2, Icon = "view-ActivityIndicator.png"},
                new ControlType{ Title = nameof(Button), GroupIndex = 2, Icon = "view-Button.png"},
                new ControlType{ Title = nameof(Editor), GroupIndex = 2, Icon = "view-Editor.png"},
                new ControlType{ Title = nameof(Entry), GroupIndex = 2, Icon = "view-Entry.png"},
                new ControlType{ Title = nameof(ListView), GroupIndex = 2, Icon = "view-ListView.png"},
                new ControlType{ Title = nameof(CollectionView), GroupIndex = 2, Icon = "view-ListView.png"},
                new ControlType{ Title = nameof(DatePicker), GroupIndex = 2, Icon = "view-DatePicker.png"},
                new ControlType{ Title = nameof(TimePicker), GroupIndex = 2, Icon = "view-TimePicker.png"},
                new ControlType{ Title = nameof(Picker), GroupIndex = 2, Icon = "view-Picker.png"},
                new ControlType{ Title = nameof(Stepper), GroupIndex = 2, Icon = "view-Stepper.png"},
                new ControlType{ Title = nameof(BoxView), GroupIndex = 2, Icon = "view-BoxView.png"},
                new ControlType{ Title = nameof(Switch), GroupIndex = 2, Icon = "view-Switch.png"},
                new ControlType{ Title = nameof(ProgressBar), GroupIndex = 2, Icon = "view-ProgressBar.png"},
                new ControlType{ Title = nameof(Label), GroupIndex = 2, Icon = "view-Label.png"},
                new ControlType{ Title = nameof(Image), GroupIndex = 2, Icon = "view-Image.png"},
                new ControlType{ Title = nameof(ImageButton), GroupIndex = 2},
                new ControlType{ Title = nameof(SearchBar), GroupIndex = 2, Icon = "view-SearchBar.png"},
                new ControlType{ Title = nameof(WebView), GroupIndex = 2, Icon = "view-WebView.png"},
                new ControlType{ Title = nameof(Map), GroupIndex = 2, Icon = "view-Map.png"},
                new ControlType{ Title = nameof(RadioButton), GroupIndex = 2},
                new ControlType{ Title = nameof(CheckBox), GroupIndex = 2},
                new ControlType{ Title = nameof(CarouselView), GroupIndex = 2},
                new ControlType{ Title = nameof(IndicatorView), GroupIndex = 2},
                new ControlType{ Title = nameof(RefreshView), GroupIndex = 2},
                new ControlType{ Title = nameof(SwipeView), GroupIndex = 2}


            });

            Layouts = layouts;
            Views = views;
            Pages = pages;

            XamarinAll = new List<ControlGroup>();
            XamarinAll.Add(layouts);
            XamarinAll.Add(views);
            XamarinAll.Add(pages);

            NavToDetailCommand = new Command<ControlType>(OnNav);
            
        }

        private async void OnNav(ControlType control)
        {
            if(control == null)
                return;

            await Shell.Current.GoToAsync($"control?control={control.Title}&template={control.ControlTemplate}");
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