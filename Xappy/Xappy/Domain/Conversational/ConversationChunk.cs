using System;

namespace Xappy.Domain.Conversational
{
    public class ConversationChunk
    {
        public ConversationChunk(DateTime sentTime, Participant author, string text)
        {
            Text = text;
            SentTime = sentTime;
            Author = author;
        }

        public string Text { get; }

        public DateTime SentTime { get; }

        public Participant Author { get; }
    }
}