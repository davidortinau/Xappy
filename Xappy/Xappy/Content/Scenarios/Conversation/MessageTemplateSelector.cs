using Xamarin.Forms;

using Xappy.Domain.Conversational;

namespace Xappy.Content.Scenarios.Conversation
{
    public class MessageTemplateSelector: DataTemplateSelector
    {
        public DataTemplate MessageFromMe { get; set; }

        public DataTemplate MessageFromOthers { get; set; }

        public DataTemplate TopPaddingMessage { get; set; }

        public DataTemplate BottomPaddingMessage { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ConversationChunk conversationChunk)
            {
                if (conversationChunk.Author == Participant.Me)
                {
                    return MessageFromMe;
                }

                if (conversationChunk is ConversationChunkPaddingTop)
                {
                    return TopPaddingMessage;
                }

                if (conversationChunk is ConversationChunkPaddingBottom)
                {
                    return BottomPaddingMessage;
                }

                return MessageFromOthers;
            }

            return null;
        }
    }
}
