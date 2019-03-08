using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Threading.Tasks;

namespace Rekog
{
    class ListFaces
    {

        public static void Run(string collectionId, string faceId)
        {
            try
            {
                Console.WriteLine("Face matching faceId " + faceId);

                var result = SearchFaces(collectionId, faceId).Result;

                Console.WriteLine("Matche(s): ");
                foreach (FaceMatch face in result.FaceMatches)
                    Console.WriteLine("FaceId: " + face.Face.FaceId + " | ImageId: " + face.Face.ExternalImageId + ",  Similarity: " + face.Similarity);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR! - " + e.Message);
                Environment.Exit(-1);
            }
        }

        private static async Task<SearchFacesResponse> SearchFaces(string collectionId, string faceId)
        {
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.USWest2);
            float similarityThreshold = 0F; // set to 0 to see all probability scores
            int maxResults = 100;

            SearchFacesRequest request = new SearchFacesRequest()
            {
                CollectionId = collectionId,
                FaceId = faceId,
                FaceMatchThreshold = similarityThreshold,
                MaxFaces = maxResults
            };

            return await rekognitionClient.SearchFacesAsync(request);
        }
    }
}