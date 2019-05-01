using System;
using System.Threading.Tasks;

namespace Xappy.Domain.Conversational
{
    public class ConversationChunkEventArgs : EventArgs
    {
        public ConversationChunk Chunk { get; }

        public ConversationChunkEventArgs(ConversationChunk chunk)
        {
            Chunk = chunk;
        }
    }

    public enum TypingStatus
    {
        NonTyping = 0,
        Typing,
    }

    public class ParticipantTypingStatusEventArgs : EventArgs
    {
        public Participant Participant { get; }

        public TypingStatus TypingStatus { get; }

        public ParticipantTypingStatusEventArgs(Participant participant, TypingStatus typingStatus)
        {
            Participant = participant;
            TypingStatus = typingStatus;
        }
    }

    public interface IConversationService
    {
        event EventHandler<ConversationChunkEventArgs> ConversationChunkAdded;

        event EventHandler<ParticipantTypingStatusEventArgs> ParticipantTypingStatusUpdated;

        Task<Conversation> GetConversationAsync(int conversationId);
    }
}
