using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FurbMote.Views.Settings {

  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class SettingsPage : Page {

    public SettingsPage() {
      this.InitializeComponent();

      Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
    }

    private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e) {
      AppShell.NavFrameHome();
      e.Handled = true;
    }

    /// <summary>
    /// Invoked when this page is about to be displayed in a Frame.
    /// </summary>
    /// <param name="e">Event data that describes how this page was reached.
    /// This parameter is typically used to configure the page.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e) {
      CheckFilesBtn.IsChecked = FurbMote.Settings.CheckFiles;
      ShowAdvBtn.IsChecked = FurbMote.Settings.ShowAdvanced;
    }

    private void CheckFiles_Checked(object sender, RoutedEventArgs e) {
      var box = sender as CheckBox;
      FurbMote.Settings.CheckFiles = box.IsChecked.Value;
    }

    private void ShowAdvanced_Checked(object sender, RoutedEventArgs e) {
      var box = sender as CheckBox;
      FurbMote.Settings.ShowAdvanced = box.IsChecked.Value;
    }
  }
}