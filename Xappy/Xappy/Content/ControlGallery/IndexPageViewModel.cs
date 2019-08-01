using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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
            var layouts = new ControlGroup("Layouts", Color.Red, new List<ControlType>
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
            });;

            var pages = new ControlGroup("Pages", Color.Blue, new List<ControlType>
            {
                new ControlType{ Title = nameof(ContentPage)},
                new ControlType{ Title = nameof(TabbedPage)},
                new ControlType{ Title = nameof(MasterDetailPage)},
                new ControlType{ Title = nameof(NavigationPage)},
                new ControlType{ Title = nameof(CarouselPage)}

            });

            var views = new ControlGroup("Views", Color.Orange, new List<ControlType>
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


            });

            XamarinAll = new List<ControlGroup>();
            XamarinAll.Add(layouts);
            XamarinAll.Add(pages);
            XamarinAll.Add(views);


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
        public ControlGroup(string name, Color color, List<ControlType> members) : base(members)
        {
            Name = name;
            GroupColor = color;
        }

        public string Name { get; set; }

        public Color GroupColor { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [Preserve(AllMembers = true)]
    public class Team : List<Member>
    {
        public Team(string name, List<Member> members) : base(members)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [Preserve(AllMembers = true)]
    public class Member
    {
        public Member(string name) => Name = name;

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [Preserve(AllMembers = true)]
    public class SuperTeams : List<Team>
    {
        public SuperTeams()
        {
            Add(new Team("Avengers",
                new List<Member>
                {
                    new Member("Thor"),
                    new Member("Captain America"),
                    new Member("Iron Man"),
                    new Member("The Hulk"),
                    new Member("Ant-Man"),
                    new Member("Wasp"),
                    new Member("Hawkeye"),
                    new Member("Black Panther"),
                    new Member("Black Widow"),
                    new Member("Doctor Druid"),
                    new Member("She-Hulk"),
                    new Member("Mockingbird"),
                }
            ));

            Add(new Team("Fantastic Four",
                new List<Member>
                {
                    new Member("The Thing"),
                    new Member("The Human Torch"),
                    new Member("The Invisible Woman"),
                    new Member("Mr. Fantastic"),
                }
            ));

            Add(new Team("Defenders",
                new List<Member>
                {
                    new Member("Doctor Strange"),
                    new Member("Namor"),
                    new Member("Hulk"),
                    new Member("Silver Surfer"),
                    new Member("Hellcat"),
                    new Member("Nighthawk"),
                    new Member("Yellowjacket"),
                }
            ));

            Add(new Team("Heroes for Hire",
                new List<Member>
                {
                    new Member("Luke Cage"),
                    new Member("Iron Fist"),
                    new Member("Misty Knight"),
                    new Member("Colleen Wing"),
                    new Member("Shang-Chi"),
                }
            ));

            Add(new Team("West Coast Avengers",
                new List<Member>
                {
                    new Member("Hawkeye"),
                    new Member("Mockingbird"),
                    new Member("War Machine"),
                    new Member("Wonder Man"),
                    new Member("Tigra"),
                }
            ));

            Add(new Team("Great Lakes Avengers",
                new List<Member>
                {
                    new Member("Squirrel Girl"),
                    new Member("Dinah Soar"),
                    new Member("Mr. Immortal"),
                    new Member("Flatman"),
                    new Member("Doorman"),
                }
            ));
        }
    }

    [Preserve(AllMembers = true)]
    public class ObservableTeam : ObservableCollection<Member>
    {
        public ObservableTeam(string name, List<Member> members) : base(members)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [Preserve(AllMembers = true)]
    public class ObservableSuperTeams : ObservableCollection<ObservableTeam>
    {
        public ObservableSuperTeams()
        {
            Add(new ObservableTeam("Avengers",
                new List<Member>
                {
                    new Member("Thor"),
                    new Member("Captain America"),
                    new Member("Iron Man"),
                    new Member("The Hulk"),
                    new Member("Ant-Man"),
                    new Member("Wasp"),
                    new Member("Hawkeye"),
                    new Member("Black Panther"),
                    new Member("Black Widow"),
                    new Member("Doctor Druid"),
                    new Member("She-Hulk"),
                    new Member("Mockingbird"),
                }
            ));

            Add(new ObservableTeam("Fantastic Four",
                new List<Member>
                {
                    new Member("The Thing"),
                    new Member("The Human Torch"),
                    new Member("The Invisible Woman"),
                    new Member("Mr. Fantastic"),
                }
            ));

            Add(new ObservableTeam("Defenders",
                new List<Member>
                {
                    new Member("Doctor Strange"),
                    new Member("Namor"),
                    new Member("Hulk"),
                    new Member("Silver Surfer"),
                    new Member("Hellcat"),
                    new Member("Nighthawk"),
                    new Member("Yellowjacket"),
                }
            ));

            Add(new ObservableTeam("Heroes for Hire",
                new List<Member>
                {
                    new Member("Luke Cage"),
                    new Member("Iron Fist"),
                    new Member("Misty Knight"),
                    new Member("Colleen Wing"),
                    new Member("Shang-Chi"),
                }
            ));

            Add(new ObservableTeam("West Coast Avengers",
                new List<Member>
                {
                    new Member("Hawkeye"),
                    new Member("Mockingbird"),
                    new Member("War Machine"),
                    new Member("Wonder Man"),
                    new Member("Tigra"),
                }
            ));

            Add(new ObservableTeam("Great Lakes Avengers",
                new List<Member>
                {
                    new Member("Squirrel Girl"),
                    new Member("Dinah Soar"),
                    new Member("Mr. Immortal"),
                    new Member("Flatman"),
                    new Member("Doorman"),
                }
            ));
        }
    }

    public class ControlType
    {
        public string Title { get; set; }
        public string ControlTemplate { get; set; }
    }


}