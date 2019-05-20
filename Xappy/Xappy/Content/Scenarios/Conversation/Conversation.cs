using System.Collections.Generic;
using System.Text;

namespace Xappy.Domain.Conversational
{
    public class Conversation
    {
        public Conversation(List<Participant> participants, List<ConversationChunk> chunks)
        {
            Participants = participants;
            Chunks = chunks;
        }

        public List<Participant> Participants { get; }

        public List<ConversationChunk> Chunks { get; }
    }
}
