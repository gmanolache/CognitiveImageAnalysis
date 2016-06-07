using System.Collections.Generic;
using System.Threading;
using Microsoft.ProjectOxford.Vision;

namespace CognitiveImageViewer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // SETUP
            var visionServiceClient = new VisionServiceClient("dfd79b82151f4330b6125baaa3689b0e");
            IEnumerable<RedditImage> items;
            using (var importer = new RedditSqliteImporter("reddit.sqlite"))
                items = importer.Import();

            using (var exporter = new RedditSqliteExporter("reddit.output.sqlite"))
            {
                // Process
                foreach (var redditImage in items)
                {
                    if (exporter.IsProcessed(redditImage.link)) continue;

                    var visualFeatures = new[]
                    {
                        VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description,
                        VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags
                    };
                    var analysisResult = visionServiceClient.AnalyzeImageAsync(redditImage.link, visualFeatures).Result;
                    if (analysisResult == null) continue;
                    var outItem = ItemMerger.Merge(redditImage, analysisResult);

                    // DUMP
                    exporter.AddRedditProcessedImage(outItem);
                    Thread.Sleep(3*1000);
                }
            }
        }
    }
}