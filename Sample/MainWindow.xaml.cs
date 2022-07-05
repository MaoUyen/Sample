using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Sample
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        static string FolderLocation = "YourLocation";
        static string PictureFile = "YourImageName";
        static string imageURI = FolderLocation + "\\" + PictureFile;

        public MainWindow()
        {
            this.InitializeComponent();
            if (File.Exists(imageURI))
            {
                Img.Source = null;
                GC.Collect();
                Img.Source = new BitmapImage(new Uri(imageURI));
            }

        }

        private void UI_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(imageURI))
            {
                Img.Source = null;
                GC.Collect();
                Img.Source = new BitmapImage(new Uri(imageURI));
            }

        }

        private async void TNI_ClickAsync(object sender, RoutedEventArgs e)
        {
            var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            DeviceInformation device = devices.FirstOrDefault(); // Finds one device, my webcam
            MediaCaptureInitializationSettings settings = new();
            settings.VideoDeviceId = device.Id;
            MediaCapture mediaCapture = new();
            await mediaCapture.InitializeAsync(settings);
            StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(FolderLocation);
            var result = await storageFolder.CreateFileAsync(PictureFile, CreationCollisionOption.ReplaceExisting);
            await mediaCapture.CapturePhotoToStorageFileAsync(ImageEncodingProperties.CreateBmp(), result);
        }
    }
}
