using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;
using ZXing.Rendering;

namespace QrCodeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BarcodeReader QRDecoder = new();
        BarcodeWriter QREncoder = new();
        BitmapImage bitmapImage;

        public MainWindow()
        {
            InitializeComponent();
            QREncoder.Format = BarcodeFormat.QR_CODE;
            QREncoder.Options.Height = 1000;
            QREncoder.Options.Width = 1000;
            QREncoder.Renderer = new BitmapRenderer();
        }

        private void BitMapToBitMapPngImage(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
        }

        private void Show(object sender, RoutedEventArgs e)
        {
            var bitMap = QREncoder.Write(MessageInput.Text);

            BitMapToBitMapPngImage(bitMap);
            QrImage.Source = bitmapImage;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            var bitMap = QREncoder.Write(MessageInput.Text);

            using (var fileStream = new FileStream("../QRCodes/image_"+DateTime.Now.ToString().Replace(':','.')+".png", FileMode.Create))
            {
                bitMap.Save(fileStream, ImageFormat.Png);
            }
        }

        private void Read(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new();
            fileDialog.Filter = "Файлы рисунков (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            var succes = fileDialog.ShowDialog();

            if (succes == true)
            {
                QRDecoder.AutoRotate = true;
                var result = QRDecoder.Decode(new System.Drawing.Bitmap(fileDialog.FileName));

                if (result != null)
                {
                    MessageBox.Show(result.Text);
                }
                else
                {
                    MessageBox.Show("Неудалось распознать");
                }
            }

        }
    }
}
