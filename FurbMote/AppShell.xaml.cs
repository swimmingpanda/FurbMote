using System;
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
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FurbMote {
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class AppShell : Page {

    public static Frame nFrame = new Frame();
    public static Grid hColor = new Grid();
    public static Controls.Header header = new Controls.Header();

    public AppShell() {
      this.InitializeComponent();

      nFrame.ContentTransitions = null;

      nFrame.Navigated += NFrame_Navigated;
      nFrame.Navigating += NFrame_Navigating;
    }

    private void NFrame_Navigating(object sender, NavigatingCancelEventArgs e) {
      //hGrid.Background = header.Background;
    }

    private void NFrame_Navigated(object sender, NavigationEventArgs e) {
    }

    /// <summary>
    /// Invoked when this page is about to be displayed in a Frame.
    /// </summary>
    /// <param name="e">Event data that describes how this page was reached.
    /// This parameter is typically used to configure the page.</param>
    protected override async void OnNavigatedTo(NavigationEventArgs e) {
      if (!await Common.SoundFilesPresent())
        await Common.ExtractSounds();

      nGrid.Children.Add(nFrame);
      hGrid.Children.Add(hColor);
      hColor.Children.Add(header);
      NavFrameHome();
    }

    public static void NavFrameNavigate(Type page, Windows.UI.Color color, string title) {
      nFrame.Navigate(page);
      header.Text = title;
      hColor.Background = new SolidColorBrush(color);
      Common.ColorStatusBar(color);
    }

    public static void NavFrameHome() {
      NavFrameNavigate(typeof(MainPage), Windows.UI.Color.FromArgb(0, 0x00, 0xAB, 0xA9), "Furbmote");
    }

  }
}
