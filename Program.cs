using Amazon.Rekognition.Model;
using System;
using System.IO;

namespace Rekog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("Facial Recognition using AWS Rekognition");
            Console.WriteLine("");
            Console.WriteLine("Select an option below:");
            Console.WriteLine(" 1 - Detect faces");
            Console.WriteLine(" 2 - Compare two faces");
            Console.WriteLine(" 3 - Lookup identity based on photo");
            Console.WriteLine(" 4 - Lookup similar faces in collection");
            Console.WriteLine("");
            Console.Write("Option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter image filename: ");
                    var c1filename = Console.ReadLine();

                    var c1image = GetRekogImage(c1filename);
                    DetectFaces.Run(c1image);
                    break;
                case "2":
                    Console.Write("Enter image1 filename: ");
                    var c2filename1 = Console.ReadLine();
                    Console.Write("Enter image2 filename: ");
                    var c2filename2 = Console.ReadLine();

                    var c2image1 = GetRekogImage(c2filename1);
                    var c2image2 = GetRekogImage(c2filename2);
                    CompareFaces.Run(c2image1, c2image2);
                    break;
                case "3":
                    Console.Write("Enter collection id: ");
                    var c3collectionid = Console.ReadLine();
                    Console.Write("Enter image filename: ");
                    var c3filename = Console.ReadLine();

                    var c3image = GetRekogImage(c3filename);
                    IdentifyFace.Run(c3collectionid, c3image);
                    break;
                case "4":
                    Console.Write("Enter collection id: ");
                    var c4collectionid = Console.ReadLine();
                    Console.Write("Enter face id: ");
                    var c4faceid = Console.ReadLine();

                    ListFaces.Run(c4collectionid, c4faceid);
                    break;
                default:
                    Console.WriteLine("Invalid option. Exiting...");
                    break;
            }
            Console.WriteLine();
            return;
        }

        public static Image GetRekogImage(string filename)
        {
            Image rekogImage = new Image();
            try
            {
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + '\\' + filename, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    rekogImage.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load source image: " + filename);
                Environment.Exit(-1);
            }
            return rekogImage;
        }
    }
}
