using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using Xappy.Domain.Conversational;

namespace Xappy.Content.Scenarios.Conversation
{
    public partial class ConversationPage : ContentPage
    {
        private const int EntryFramePeekWidth = 60;
        private const int ParticipantsFramePeekWidth = 100;

        private readonly ConversationPageViewModel _viewModel;

        private double _entryFrameTranslationX;

        private double _participantsFrameTranslationX;

        public ConversationPage()
        {
            InitializeComponent();

            _viewModel = new ConversationPageViewModel(new ConversationMockService());
            BindingContext = _viewModel;

            _viewModel.PropertyChanged += ConversationViewModelPropertyChanged;
            _viewModel.InitAsync();
        }

        private void ConversationViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ConversationPageViewModel.Chunks))
            {
                if (_viewModel.Chunks != null)
                {
                    _viewModel.Chunks.CollectionChanged += ChunksCollectionChanged;
                }
            }
        }

        private void ChunksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ConversationChunk item in e.NewItems)
                {
                    if (item.Author == Participant.Me)
                    {
                        MessageCollectionView.ScrollTo(0);
                    }
                }
            }
        }

        private void BackButtonOnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void EntryFrameOnSwipedLeft(object sender, SwipedEventArgs e)
        {
            if (EntryFrame.TranslationX > -100)
            {
                ComputeEntryFrameTranslationIfNeeded();
                ComputeParticipantsFrameTranslationIfNeeded();

                var entryFrameAnimationTask = EntryFrame.TranslateTo(_entryFrameTranslationX, EntryFrame.TranslationY);
                var participantsAnimationTask = ParticipantsFrame.TranslateTo(_participantsFrameTranslationX, ParticipantsFrame.TranslationY);
                await Task.WhenAll(entryFrameAnimationTask, participantsAnimationTask);
            }
        }

        private async void EntryFrameOnSwipedRight(object sender, SwipedEventArgs e)
        {
            if (EntryFrame.TranslationX < 0)
            {
                var entryFrameAnimationTask = EntryFrame.TranslateTo(0, EntryFrame.TranslationY);
                var participantsAnimationTask = ParticipantsFrame.TranslateTo(0, ParticipantsFrame.TranslationY);
                await Task.WhenAll(entryFrameAnimationTask, participantsAnimationTask);
            }
        }

        private void ComputeEntryFrameTranslationIfNeeded()
        {
            if (_entryFrameTranslationX != 0 || EntryFrame.Width <= 0)
            {
                return;
            }

            double displayedFrameWidth = EntryFrame.Width + EntryFrame.Margin.Left;
            _entryFrameTranslationX = -(displayedFrameWidth - EntryFramePeekWidth);
        }

        private void ComputeParticipantsFrameTranslationIfNeeded()
        {
            if (_participantsFrameTranslationX != 0 || ParticipantsFrame.Width <= 0)
            {
                return;
            }

            double displayedFrameWidth = ParticipantsFrame.Width + ParticipantsFrame.Margin.Right;
            _participantsFrameTranslationX = displayedFrameWidth - ParticipantsFramePeekWidth;
        }

        private async void MessageCollectionViewOnTapped(object sender, EventArgs e)
        {
            if (EntryFrame.TranslationX > -100)
            {
                ComputeEntryFrameTranslationIfNeeded();
                ComputeParticipantsFrameTranslationIfNeeded();

                var entryFrameAnimationTask = EntryFrame.TranslateTo(_entryFrameTranslationX, EntryFrame.TranslationY);
                var participantsAnimationTask = ParticipantsFrame.TranslateTo(_participantsFrameTranslationX, ParticipantsFrame.TranslationY);
                await Task.WhenAll(entryFrameAnimationTask, participantsAnimationTask);
                return;
            }
            
            if (EntryFrame.TranslationX < 0)
            {
                var entryFrameAnimationTask = EntryFrame.TranslateTo(0, EntryFrame.TranslationY);
                var participantsAnimationTask = ParticipantsFrame.TranslateTo(0, ParticipantsFrame.TranslationY);
                await Task.WhenAll(entryFrameAnimationTask, participantsAnimationTask);
            }
        }
    }
}
