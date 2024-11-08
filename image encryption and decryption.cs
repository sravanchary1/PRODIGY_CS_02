using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ConsoleApp2
{

    class ImageEncryption
    {
        // Encrypt an image using XOR operation on each pixel
        public static Bitmap EncryptImage(Bitmap image, int key)
        {
            Bitmap encryptedImage = new Bitmap(image);

            // Loop through each pixel and apply XOR with the key
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    // Get the pixel at position (x, y)
                    Color pixelColor = image.GetPixel(x, y);

                    // Apply XOR operation to each color component (R, G, B)
                    int red = pixelColor.R ^ key;
                    int green = pixelColor.G ^ key;
                    int blue = pixelColor.B ^ key;

                    // Ensure color values are within valid range [0, 255]
                    red = Math.Max(0, Math.Min(255, red));
                    green = Math.Max(0, Math.Min(255, green));
                    blue = Math.Max(0, Math.Min(255, blue));

                    // Set the new pixel color
                    encryptedImage.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            }

            return encryptedImage;
        }

        // Decrypt an image using XOR operation on each pixel (same key used as in encryption)
        public static Bitmap DecryptImage(Bitmap image, int key)
        {
            return EncryptImage(image, key); // XOR is a reversible operation (same key)
        }

        // Simple function to load an image from a file
        public static Bitmap LoadImage(string filePath)
        {
            return new Bitmap(filePath);
        }

        // Save the image to a file
        public static void SaveImage(Bitmap image, string filePath)
        {
            image.Save(filePath);
        }

        static void Main()
        {
            Console.WriteLine("Image Encryption Tool");
            Console.Write("Enter image path for encryption (e.g., C:\\path\\to\\image.jpg): ");
            string inputFilePath = Console.ReadLine();

            // Check if the file exists
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("File not found! Please check the path and try again.");
                return;
            }

            try
            {
                // Load the image
                Bitmap image = LoadImage(inputFilePath);

                // Ask the user for an encryption key (integer)
                Console.Write("Enter encryption key (integer): ");
                int key;
                while (!int.TryParse(Console.ReadLine(), out key))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }

                // Encrypt the image
                Bitmap encryptedImage = EncryptImage(image, key);
                string encryptedFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), "encrypted_image.png");
                SaveImage(encryptedImage, encryptedFilePath);
                Console.WriteLine($"Encrypted image saved at: {encryptedFilePath}");

                // Decrypt the image (using the same key)
                Bitmap decryptedImage = DecryptImage(encryptedImage, key);
                string decryptedFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), "decrypted_image.png");
                SaveImage(decryptedImage, decryptedFilePath);
                Console.WriteLine($"Decrypted image saved at: {decryptedFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    
     }
}




