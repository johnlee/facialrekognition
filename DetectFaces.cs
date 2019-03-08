using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Threading.Tasks;

namespace Rekog
{
    class DetectFaces
    {
        public static void Run(Image image)
        {
            try
            {
                var result = IdentifyFaces(image).Result;

                if (result.FaceDetails.Count > 0)
                {
                    Console.WriteLine("There were " + result.FaceDetails.Count + " face(s) found in the image");
                }
                else
                {
                    Console.WriteLine("There were no faces found in the image");
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR! - " + e.Message);
                Environment.Exit(-1);
            }
        }

        private static async Task<DetectFacesResponse> IdentifyFaces(Image image)
        {
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(Amazon.RegionEndpoint.USWest2);

            DetectFacesRequest request = new DetectFacesRequest();
            request.Image = image;
            return await rekognitionClient.DetectFacesAsync(request);
        }
    }
}
