using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Tesseract;

namespace OCR
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Open file dialog to select an image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                // Specify the path to the Tesseract data directory
                string tessData = @"C:\Program Files (x86)\Tesseract-OCR\tessdata";
                string tessDataara = @"C:\Program Files (x86)\Tesseract-OCR\aratessdata";
                // Get the selected image path
                string imagePath = openFileDialog.FileName;

                // Display the selected image in the Image Rectangle
                DisplayImage(imagePath);

                // Perform OCR on the selected image
                string arabicText = PerformOCR(imagePath, tessDataara, "ara");
                string englishText = PerformOCR(imagePath, tessData, "eng");
                // Display OCR results in the TextBlock
                OcrResultsText.Text = englishText;
                OcrResultsTextara.Text = arabicText;
                // Update status
                StatusText.Text = "Image successfully uploaded and OCR performed.";
            }
        }

        private void DisplayImage(string imagePath)
        {
            try
            {
                // Create a BitmapImage and set it as the source of the Image control
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                bitmap.EndInit();

                // Set the source of the Image Rectangle
                ImageRectangle.Fill = new System.Windows.Media.ImageBrush(bitmap);
            }
            catch (Exception ex)
            {
                // Handle any exceptions related to loading the image
                StatusText.Text = "Error loading the image: " + ex.Message;
            }
        }

        static string PerformOCR(string imagePath, string tessData, string language)
        {
            try
            {
                // Initialize the Tesseract engine
                using (var engine = new TesseractEngine(tessData, language, EngineMode.Default))
                {
                    // Load the image
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        // Set the image for OCR
                        using (var page = engine.Process(img))
                        {
                            // Get the recognized text
                            return page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Error performing OCR: {ex.Message}");
                return string.Empty; // Or throw the exception again if needed
            }
        }

    }
}
