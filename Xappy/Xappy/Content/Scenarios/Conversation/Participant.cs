namespace Xappy.Domain.Conversational
{
    public class Participant
    {
        public static readonly Participant Me = new Participant(
            "Xam",
            "Xappy",
            "https://pbs.twimg.com/profile_images/1118743728003452928/oMJdZl-C_400x400.png",
            "Il dit qu'il voit pas le rapport");

        public Participant(string firstName, string lastName, string avatarUrl, string spamSentence)
        {
            FirstName = firstName;
            LastName = lastName;
            AvatarUrl = avatarUrl;
            SpamSentence = spamSentence;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string AvatarUrl { get; }

        public string SpamSentence { get; }
    }
}
