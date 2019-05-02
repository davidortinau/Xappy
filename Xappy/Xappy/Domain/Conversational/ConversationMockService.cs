using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xappy.Domain.Conversational
{
    public class ConversationMockService : IConversationService
    {
        public static readonly Participant WillFerrel = new Participant(
            "Will",
            "Ferrel",
            "http://www.2oceansvibe.com/wp-content/uploads/2017/10/willlferell.jpg",
            "By the beard of Zeus!");

        public static readonly Participant Moss = new Participant(
            "Maurice",
            "Moss",
            "http://i123.photobucket.com/albums/o320/lucy_edward/moss_pic2.jpg",
            "Did you see that ludicrous display last night?");

        public static readonly Participant KnightsOfNi = new Participant(
            "Knights",
            "Of Ni",
            "http://images.uncyc.org/commons/d/dd/The_leader_of_the_Knights_Who_Say_Ni.jpg",
            "NI!");

        public static readonly Participant SergeKara = new Participant(
            "Serge",
            "Karamazov",
            "https://pbs.twimg.com/profile_images/1022538875758108674/C2yjOb8o_400x400.jpg",
            "Il dit qu'il voit pas le rapport");

        public static List<Participant> Participants = new List<Participant>
            {
                WillFerrel,
                Moss,
                KnightsOfNi,
                SergeKara,
                WillFerrel,
                Moss,
                KnightsOfNi,
                SergeKara,
            };


        private const string WillFerrelQuote =
            "Hey.\nThey laughed at Louis Armstrong when he said he was gonna go to the moon.\nNow he’s up there, laughing at them.";

        public event EventHandler<ConversationChunkEventArgs> ConversationChunkAdded;

        public event EventHandler<ParticipantTypingStatusEventArgs> ParticipantTypingStatusUpdated;

        public ConversationMockService()
        {
            Task.Run(
                async () =>
                    {
                        while (true)
                        {
                            await Task.Delay(5000);

                            var participant = Participants[new Random().Next(4)];
                            ConversationChunkAdded?.Invoke(
                                this,
                                new ConversationChunkEventArgs(
                                    new ConversationChunk(DateTime.Now, participant, participant.SpamSentence)));
                        }
                    });
        }

        public Task<Conversation> GetConversationAsync(int conversationId)
        {
            return Task.Run(
                () =>
                    {
                        DateTime now = DateTime.Now.AddMinutes(-3);

                        return new Conversation(
                            Participants,
                            new List<ConversationChunk>
                                {
                                    new ConversationChunk(NextSentTime(ref now), WillFerrel, WillFerrelQuote),
                                    new ConversationChunk(NextSentTime(ref now), KnightsOfNi, KnightsOfNi.SpamSentence),
                                    new ConversationChunk(NextSentTime(ref now), Moss, Moss.SpamSentence),
                                    new ConversationChunk(NextSentTime(ref now), KnightsOfNi, KnightsOfNi.SpamSentence),
                                    new ConversationChunk(NextSentTime(ref now), SergeKara, SergeKara.SpamSentence),
                                });
                    });
        }

        private static DateTime NextSentTime(ref DateTime sentTime)
        {
            int intervalInSeconds = new Random().Next(5, 15);
            sentTime = sentTime.AddSeconds(intervalInSeconds);
            return sentTime;
        }
    }
}