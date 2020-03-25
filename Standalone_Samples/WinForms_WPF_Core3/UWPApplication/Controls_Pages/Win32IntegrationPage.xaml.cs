using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPApplication;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Samples.ManagedUWP
{
    public sealed partial class Win32IntegrationPage : Page
    {
        public Win32IntegrationPage()
        {
            this.InitializeComponent();

            UWPApplication.App.OrientationChanged += OnWin32OrientationChanged;
            displayOrientationWin32.Text = Helpers.Win32.DisplayInformation.CurrentOrientation.ToString();

            Windows.Graphics.Display.DisplayInformation.GetForCurrentView().OrientationChanged += OnOrientationChanged;
            displayOrientationUWP.Text = Windows.Graphics.Display.DisplayInformation.GetForCurrentView().CurrentOrientation.ToString();
        }

        private void OnOrientationChanged(Windows.Graphics.Display.DisplayInformation sender, object args)
        {
            if (sender.CurrentOrientation == Windows.Graphics.Display.DisplayOrientations.Landscape)
            {
                displayOrientationUWP.Text = "Landscape";
            }
            else if (sender.CurrentOrientation == Windows.Graphics.Display.DisplayOrientations.Portrait)
            {
                displayOrientationUWP.Text = "Portrait";
            }
        }

        private void OnWin32OrientationChanged(OrientationChangedEventArgs e)
        {
            if (e.IsLandscape)
            {
                displayOrientationWin32.Text = "Landscape";
            }
            else
            {
                displayOrientationWin32.Text = "Portrait";
            }
        }

        private async void cameraBtn_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            
            //In Win32, this is always null.
            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }
            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Premultiplied);

            SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

            imageControl.Source = bitmapSource;
        }
    }
}
