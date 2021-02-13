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
                    Icon = "layout_stacklayout.png"
                },
                new ControlType{ Title = nameof(Frame), Icon = "layout_frame.png"},
                new ControlType{ Title = nameof(Grid), Icon = "layout_grid.png"},
                new ControlType{ Title = nameof(FlexLayout), Icon = "layout_flexlayout.png"},
                new ControlType{ Title = nameof(AbsoluteLayout), Icon = "layout_absolutelayout.png"},
                new ControlType{ Title = nameof(RelativeLayout), Icon = "layout_relativelayout.png"},
                new ControlType{ Title = nameof(ScrollView), Icon = "layout_scrollview.png"},
                
            });

            var pages = new ControlGroup("Pages", new List<ControlType>
            {
                new ControlType{ Title = nameof(ContentPage), GroupIndex = 1},
                new ControlType{ Title = nameof(TabbedPage), GroupIndex = 1},
                new ControlType{ Title = nameof(FlyoutPage), GroupIndex = 1},
                new ControlType{ Title = nameof(NavigationPage), GroupIndex = 1},
                new ControlType{ Title = nameof(CarouselPage), GroupIndex = 1}

            });

            var views = new ControlGroup("Views", new List<ControlType>
            {
                new ControlType{ Title = nameof(ActivityIndicator), GroupIndex = 2, Icon = "view_activityindicator.png"},
                new ControlType{ Title = nameof(Button), GroupIndex = 2, Icon = "view_button.png"},
                new ControlType{ Title = nameof(Editor), GroupIndex = 2, Icon = "view_editor.png"},
                new ControlType{ Title = nameof(Entry), GroupIndex = 2, Icon = "view_entry.png"},
                new ControlType{ Title = nameof(ListView), GroupIndex = 2, Icon = "view_listview.png"},
                new ControlType{ Title = nameof(CollectionView), GroupIndex = 2, Icon = "view_listview.png"},
                new ControlType{ Title = nameof(DatePicker), GroupIndex = 2, Icon = "view_datepicker.png"},
                new ControlType{ Title = nameof(TimePicker), GroupIndex = 2, Icon = "view_timepicker.png"},
                new ControlType{ Title = nameof(Picker), GroupIndex = 2, Icon = "view_picker.png"},
                new ControlType{ Title = nameof(Stepper), GroupIndex = 2, Icon = "view_stepper.png"},
                new ControlType{ Title = nameof(BoxView), GroupIndex = 2, Icon = "view_boxview.png"},
                new ControlType{ Title = nameof(Switch), GroupIndex = 2, Icon = "view_switch.png"},
                new ControlType{ Title = nameof(ProgressBar), GroupIndex = 2, Icon = "view_progressbar.png"},
                new ControlType{ Title = nameof(Label), GroupIndex = 2, Icon = "view_label.png"},
                new ControlType{ Title = nameof(Image), GroupIndex = 2, Icon = "view_image.png"},
                new ControlType{ Title = nameof(ImageButton), GroupIndex = 2},
                new ControlType{ Title = nameof(SearchBar), GroupIndex = 2, Icon = "view_searchbar.png"},
                new ControlType{ Title = nameof(WebView), GroupIndex = 2, Icon = "view_webview.png"},
                new ControlType{ Title = nameof(Map), GroupIndex = 2, Icon = "view_map.png"},
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