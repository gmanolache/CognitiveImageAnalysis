using SQLite;

namespace CognitiveImageViewer
{
    public class RedditImageOutput
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [Indexed]
        public string Link { get; set; }

        public int Score { get; set; }
        public string Subid { get; set; }
        public double AdultScore { get; set; }
        public double RacyScore { get; set; }
        public string Categories { get; set; }
        public string DescriptionTags { get; set; }
        public string Tags { get; set; }
    }
}