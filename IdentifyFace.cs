using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Threading.Tasks;

namespace Rekog
{
    class IdentifyFace
    {
        public static void Run(string collectionId, Image image)
        {
            try
            {
                Console.WriteLine("Searching for facial match" );

                var result = LookupImage(collectionId, image).Result;

                if (result.FaceMatches.Count > 0)
                {
                    foreach (FaceMatch face in result.FaceMatches)
                        Console.WriteLine("FaceId: " + face.Face.FaceId + " | ImageId: " + face.Face.ExternalImageId + " | Similarity: " + face.Similarity);
                }
                else
                {
                    Console.WriteLine("No matches were found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR! - " + e.Message);
                Environment.Exit(-1);
            }
        }

        private static async Task<SearchFacesByImageResponse> LookupImage(string collectionId, Image image)
        {
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.USWest2);
            float similarityThreshold = 0F; // set to 0 to see all probability scores
            int maxResults = 100;

            SearchFacesByImageRequest request = new SearchFacesByImageRequest()
            {
                CollectionId = collectionId,
                Image = image,
                FaceMatchThreshold = similarityThreshold,
                MaxFaces = maxResults
            };

            return await rekognitionClient.SearchFacesByImageAsync(request);
        }
    }
}
