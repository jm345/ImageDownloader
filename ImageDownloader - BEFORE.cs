using System;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data.Common;

namespace ImageDownloader
{
    class Program
    {
        const string DEFAULT_PATH = @"C:\Users\Jordan\Desktop\";

        static void Main(string[] args)
        {
            WebClient webClient = new WebClient();
            Console.Write("Please enter the name of the file that contains the URLs: ");
            string userFile = Console.ReadLine();
            Console.Write("Please create a new subdirectory (Default Path: " + DEFAULT_PATH + "): ");
            string userDirectory = Console.ReadLine();
            Directory.CreateDirectory(DEFAULT_PATH + userDirectory);
            File.OpenRead(DEFAULT_PATH + userFile);
            string[] urlStrings = File.ReadAllLines(@"C:\Users\Jordan\Desktop\m10T.txt");
            string[] productSkus = File.ReadAllLines(@"C:\Users\Jordan\Desktop\productSKU_Price.txt");
            int i = 1;
            int urlCount = urlStrings.Length;
            Directory.CreateDirectory(@"C:\Users\Jordan\Desktop\M10Trade_Images");
            
            foreach (string url in urlStrings)
            {
                if (url == string.Empty)
                {
                    Console.WriteLine("URL string is empty. Moving to next string...");
                    i++;
                    continue;
                }
                else
                {
                    Uri uri = new Uri(url);
                    Console.WriteLine("Downloading image {0} of {1}...", i, urlCount);
                    webClient.DownloadFile(uri, @"C:\Users\Jordan\Desktop\M10Trade_Images\SKU - " + productSkus[i - 1] + ".jpg");
                    i++;
                }
            }
            webClient.Dispose();
            webClient.Disposed += WebClient_Disposed;
        }

        private static void WebClient_Disposed(object sender, EventArgs e)
        {
            Console.WriteLine("Terminating Connection...");
            Console.WriteLine("Exiting Program...");
        }

        public static void CreateDirectory()
        {
            Console.Write("Please create a new subdirectory: (The default path is " + DEFAULT_PATH + "): ");
            string userFolder = Console.ReadLine();
            string newDirectory = DEFAULT_PATH + userFolder;
            bool directoryExists = Directory.Exists(newDirectory);
            if (!(directoryExists))
            {
                try
                {
                    Directory.CreateDirectory(newDirectory);
                    Console.WriteLine("Successfully created new directory.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please enter a valid directory.");
                }
            }
            else
            {
                Console.WriteLine("This directory already exists.");
            }      
        }
    }
}
