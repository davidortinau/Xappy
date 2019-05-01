using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

using Xamarin.Forms;

using Xappy.Domain.Conversational;

namespace Xappy.Content.Scenarios
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
                        Chunks.Insert(0, new ConversationChunk(DateTime.Now, Participant.Me, MessageToSend));
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
                Chunks = new ObservableCollection<ConversationChunk>(conversation.Chunks);
                RaisePropertyChanged(nameof(Chunks));
                Participants = new ObservableCollection<Participant>(conversation.Participants);
                RaisePropertyChanged(nameof(Participants));
            }
            catch (Exception e)
            {
                // Handle this exception
            }
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
