using System;
using System.Net;
using System.IO;

namespace ImageDownloader
{
    class Program
    {
        const string DEFAULT_PATH = @"C:\Users\Jordan\Desktop\";
        static string newPath = string.Empty;

        static void Main(string[] args)
        {
			InitialDisplay();
            UserNewDirectory();
            SelectFiles();  
        }

        private static void WebClient_Disposed(object sender, EventArgs e)
        {
            Console.WriteLine("\nTerminating Connection...");
            Console.WriteLine("Exiting Program...");
        }
		
		public static void InitialDisplay()
		{	
			string title = "Image Downloader v1.0"
			Console.WriteLine("=" * len(title));
			Console.WriteLine(title);
			Console.WriteLine("=" * len(title));
			Console.WriteLine("\nPlease note: Files and options entered by the user are not case-sensitive, directories are case-sensitive.");
			Console.WriteLine();
		}
			
        public static void SelectFiles()
        {
            Console.Write("Please enter the .txt file which contains the image URLs: (Default Path: {0}) ",DEFAULT_PATH);
            string imageURLFileName = Console.ReadLine();
            Console.Write("Please enter the .txt file which contains the codes and prices: (Default Path: {0}) ", DEFAULT_PATH);
            string skuPriceFileName = Console.ReadLine();
            try
            {
                string[] imageUrls = File.ReadAllLines(DEFAULT_PATH + "\\" + imageURLFileName + ".txt");
                string[] skusPrices = File.ReadAllLines(DEFAULT_PATH + "\\" + skuPriceFileName + ".txt");
                GetData(imageUrls, skusPrices);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Please select the valid file(s).\n");
                SelectFiles();
            } 
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Please ensure the directory is valid.\n");
                SelectFiles();
            }
        }

        public static void GetData(string[] imageUrls,string[] skusPrices)
        {
            WebClient webClient = new WebClient();
            webClient.Disposed += WebClient_Disposed;

            if (newPath == string.Empty)
            {
                newPath = (Directory.CreateDirectory(DEFAULT_PATH + "Images")).ToString();
                Console.WriteLine(newPath);
            }

            Console.Write("Press any key to begin downloading the data.\n");
            Console.ReadKey();

            int i = 0;
            foreach (string url in imageUrls)
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
                    Console.WriteLine("Downloading image {0} of {1}...", i + 1, imageUrls.Length);
                    webClient.DownloadFile(uri, newPath + "SKU - " + skusPrices[i] + ".jpg");
                    i++;
                }
            }
            webClient.Dispose();
        }

        public static void UserNewDirectory()
        {
            string userOption = string.Empty;
            Console.Write("Would you like to create a new directory? (Y/N) ");
            userOption = Console.ReadLine();
            switch (userOption.ToUpper())
            {
                case ("Y"):
                    CreateNewDirectory();
                    break;
                case ("N"):
                    Console.WriteLine();
                    SelectFiles();
					break;
                default:
                    Console.WriteLine("Please enter a valid option.\n");
                    UserNewDirectory();
                    break;
            }
        }

        public static void CreateNewDirectory()
        {
            Console.Write("Please create a new subdirectory: (The default path is " + DEFAULT_PATH + "): ");
            string newDirectory = Console.ReadLine();
            string newDirectoryPath = DEFAULT_PATH + newDirectory;
            bool directoryExists = Directory.Exists(newDirectoryPath);
            if (directoryExists == false)
            {
                try
                {
                    Directory.CreateDirectory(newDirectoryPath);
                    newPath = newDirectoryPath + "\\";
                    Console.WriteLine("Successfully created new directory.\n");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Please enter a valid directory.\n");
                    CreateNewDirectory();
                }
            }
            else
            {
                Console.WriteLine("This directory already exists.\n");
                CreateNewDirectory();
            }
        }
    }
}
