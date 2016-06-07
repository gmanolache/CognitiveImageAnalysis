using Microsoft.ProjectOxford.Vision.Contract;

namespace CognitiveImageViewer
{
    public static class ItemMerger
    {
        public static RedditImageOutput Merge(RedditImage redditImage, AnalysisResult analysisResult)
        {
            var outItem = new RedditImageOutput
            {
                Link = redditImage.link,
                Score = redditImage.score,
                Subid = redditImage.subid,
                AdultScore = analysisResult.Adult.AdultScore,
                RacyScore = analysisResult.Adult.RacyScore,
                DescriptionTags = string.Join(",", analysisResult.Description.Tags)
            };
            if (analysisResult.Categories != null)
                foreach (var category in analysisResult.Categories)
                {
                    outItem.Categories += category.Name + ",";
                }
            else
                outItem.Categories = "";

            foreach (var tag in analysisResult.Tags)
            {
                outItem.Tags += tag.Name + ",";
            }
            return outItem;
        }
    }
}