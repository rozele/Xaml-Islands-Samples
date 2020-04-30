﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Samples.ManagedUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DialogsAndFlyoutsPage : Page
    {
        public DialogsAndFlyoutsPage()
        {
            this.InitializeComponent();
        }

        private async void OnShowContentDialog(object sender, RoutedEventArgs e)
        {
            contentDialog.XamlRoot = (sender as Button).XamlRoot;
            await contentDialog.ShowAsync();
        }

        private void OnShowShareText(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(shareText.Text))
            {
                UWPApplication.App.ShowShareUIForWindow(shareText.Text);
            }
        }
    }
}