using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Threading.Tasks;

namespace Rekog
{
    class CompareFaces
    {
        public static void Run(Image image1, Image image2)
        {
            try
            {
                var result = Compare(image1, image2).Result;

                foreach (CompareFacesMatch match in result.FaceMatches)
                {
                    ComparedFace face = match.Face;
                    BoundingBox position = face.BoundingBox;
                    Console.WriteLine();
                    Console.WriteLine("Image1 Face Position: " + position.Left + " " + position.Top);
                    Console.WriteLine("Image1 matches Image2 Score: " + match.Similarity);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR! - " + e.Message);
                Environment.Exit(-1);
            }
        }

        private static async Task<CompareFacesResponse> Compare(Image image1, Image image2)
        {
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.USWest2);
            float similarityThreshold = 0F; // set to 0 to see all probability scores

            CompareFacesRequest compareFacesRequest = new CompareFacesRequest()
            {
                SourceImage = image1,
                TargetImage = image2,
                SimilarityThreshold = similarityThreshold
            };

            return await rekognitionClient.CompareFacesAsync(compareFacesRequest);
        }
    }
}
