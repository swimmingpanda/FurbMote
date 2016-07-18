using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace FurbMote {

  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page {

    public MainPage() {
      this.InitializeComponent();

      this.NavigationCacheMode = NavigationCacheMode.Required;
    }

    /// <summary>
    /// Invoked when this page is about to be displayed in a Frame.
    /// </summary>
    /// <param name="e">Event data that describes how this page was reached.
    /// This parameter is typically used to configure the page.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e) {
      // TODO: Prepare page for display here.

      // TODO: If your application contains multiple pages, ensure that you are
      // handling the hardware Back button by registering for the
      // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
      // If you are using the NavigationHelper provided by some templates,
      // this event is handled for you.

      Windows.UI.ViewManagement.StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
      statusBar.BackgroundColor = Windows.UI.Color.FromArgb(0, 0x00, 0xAB, 0xA9);
      statusBar.BackgroundOpacity = 1;
    }

    private void PlayBtn_Click(object sender, RoutedEventArgs e) {
      //Common.PlayMedia(ApplicationData.Current.LocalFolder.Path + "\\" + SoundNameBox.Text + ".wav", LayoutRoot);
    }

    private async void OpenArchiveBtn_Click(object sender, RoutedEventArgs e) {
      StorageFolder rootFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
      StorageFolder assetFolder = await rootFolder.GetFolderAsync("Assets");
      StorageFile file = await assetFolder.GetFileAsync("Sound.zip");

      //Stream stream = await file.OpenStreamForReadAsync();

      StorageFolder local = ApplicationData.Current.LocalFolder;

      var progress = new Progress<float>();
      progress.ProgressChanged += Progress_ProgressChanged;
      await Common.ExtractDoubleArchive(file, local, progress, true);
    }

    private void Progress_ProgressChanged(object sender, float e) {
      //PBar.Value = e;
    }

    private void AdvBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
      this.Frame.Navigate(typeof(Views.AdvancedPage));
    }

    private void SettingsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
      var btn = sender as Controls.BigColorButton;
      this.Frame.Navigate(typeof(Views.Settings.SettingsPage));
    }
  }
}