using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;

namespace Samples.ManagedUWP
{

    public sealed partial class MediaPage : Page
    {
        private InkPresenter inkPresenter;
        public MediaPage()
        {
            this.InitializeComponent();
            inkPresenter = inkCanvas.InkPresenter;
            inkPresenter.InputDeviceTypes =
                CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen | CoreInputDeviceTypes.Touch;
            this.Loaded += MediaPage_Loaded;
            this.Unloaded += MediaPage_Unloaded;
        }

        private void MediaPage_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.canvas.RemoveFromVisualTree();
            this.canvas = null;
        }

        private void MediaPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var testPanel = new Native_SwapChainPanel_Comp.CustomRenderer();
            swapChainPanelGrid.Children.Add(testPanel);
        }

        private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender,Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawText("Hello, World!", 100, 100, Windows.UI.Colors.Black);
        }
    }
}
