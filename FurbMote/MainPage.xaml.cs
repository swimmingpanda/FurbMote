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
      if (Settings.ShowAdvanced == false) {
        GridLength len = new GridLength(0);
        AdvRow.Height = len;
        AdvBtn.Visibility = Visibility.Collapsed;
      }
      else {
        GridLength len = new GridLength(1, GridUnitType.Star);
        AdvRow.Height = len;
        AdvBtn.Visibility = Visibility.Visible;
      }
    }

    private void PlayBtn_Click(object sender, RoutedEventArgs e) {
      //Common.PlayMedia(ApplicationData.Current.LocalFolder.Path + "\\" + SoundNameBox.Text + ".wav", LayoutRoot);
    }

    private async void OpenArchiveBtn_Click(object sender, RoutedEventArgs e) {
      StorageFolder rootFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
      StorageFolder assetFolder = await rootFolder.GetFolderAsync("Assets");
      StorageFile file = await assetFolder.GetFileAsync("Sound.zip");

      StorageFolder local = ApplicationData.Current.LocalFolder;

      var progress = new Progress<float>();
      progress.ProgressChanged += Progress_ProgressChanged;
      await Common.ExtractSounds();
    }

    private void Progress_ProgressChanged(object sender, float e) {
      //PBar.Value = e;
    }

    private void AdvBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
      AppShell.NavFrameNavigate(typeof(Views.AdvancedPage), Windows.UI.Color.FromArgb(0xFF, 0x9A, 0xCD, 0x32), "Advanced");
      //this.Frame.Navigate(typeof(Views.AdvancedPage));
    }

    private void SettingsBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
      var btn = sender as Controls.BigColorButton;
      AppShell.NavFrameNavigate(typeof(Views.Settings.SettingsPage), Windows.UI.Color.FromArgb(0xFF, 0xFA, 0x68, 0x00), "Settings");
      //this.Frame.Navigate(typeof(Views.Settings.SettingsPage));
    }

    private void CommonBtn_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
      AppShell.NavFrameNavigate(typeof(Views.CommandsPage), Windows.UI.Color.FromArgb(0xFF, 0xD8, 0x00, 0x73), "Common Commands");
    }
  }
}