using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

using Xamarin.Forms;

using Xappy.Domain.Conversational;

namespace Xappy.Content.Scenarios.Conversation
{
    public class ConversationPageViewModel : BaseViewModel
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
            set => SetProperty(ref _messageToSend, value);
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
                OnPropertyChanged(nameof(Chunks));
                Participants = new ObservableCollection<Participant>(conversation.Participants);
                OnPropertyChanged(nameof(Participants));

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
    }
}