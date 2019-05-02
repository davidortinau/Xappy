using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;

using Xappy.Domain.Conversational;

namespace Xappy.Content.Scenarios.Conversation
{
    public class ConversationPageViewModel : INotifyPropertyChanged
    {
        private readonly IConversationService _conversationService;

        private string _messageToSend;

        public ConversationPageViewModel(IConversationService conversationService)
        {
            _conversationService = conversationService;
            SendMessageCommand = new Command(
                () =>
                    {
                        Chunks.Insert(1, new ConversationChunk(DateTime.Now, Participant.Me, MessageToSend));
                        MessageToSend = null;
                    });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Participant> Participants { get; set; }

        public ObservableCollection<ConversationChunk> Chunks { get; set; }

        public ICommand SendMessageCommand { get; }

        public string MessageToSend
        {
            get => _messageToSend;
            set => SetAndRaise(ref _messageToSend, value);
        }

        public async void InitAsync()
        {
            try
            {

                var conversation = await _conversationService.GetConversationAsync(56);

                // Reverse chunks cause our CollectionView is under the mighty double 180 spell
                var newChunks = new ObservableCollection<ConversationChunk> { new ConversationChunkPaddingBottom() };
                for (int i = conversation.Chunks.Count - 1; i >= 0; i--)
                {
                    newChunks.Add(conversation.Chunks[i]);
                }

                newChunks.Add(new ConversationChunkPaddingTop());

                Chunks = newChunks;
                RaisePropertyChanged(nameof(Chunks));
                Participants = new ObservableCollection<Participant>(conversation.Participants);
                RaisePropertyChanged(nameof(Participants));

                _conversationService.ConversationChunkAdded += ConversationServiceConversationChunkAdded;
            }
            catch (Exception e)
            {
                // Handle this exception
            }
        }

        private void ConversationServiceConversationChunkAdded(object sender, ConversationChunkEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => Chunks.Insert(1, e.Chunk));
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetAndRaise<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value))
            {
                return false;
            }

            property = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
