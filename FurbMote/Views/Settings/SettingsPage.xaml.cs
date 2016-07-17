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
using Windows.Storage;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FurbMote.Views.Settings {
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class SettingsPage : Page {

    ObservableCollection<Common.Commands> records;

    public SettingsPage() {
      this.InitializeComponent();

      Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
    }

    private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e) {
      Frame.Navigate(typeof(MainPage));
      e.Handled = true;
    }

    /// <summary>
    /// Invoked when this page is about to be displayed in a Frame.
    /// </summary>
    /// <param name="e">Event data that describes how this page was reached.
    /// This parameter is typically used to configure the page.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e) {
      Windows.UI.ViewManagement.StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
      statusBar.BackgroundColor = Windows.UI.Color.FromArgb(0, 0xFA, 0x68, 0x00);
      statusBar.BackgroundOpacity = 1;

      ReadCsv();
    }

    async void ReadCsv() {
      StorageFolder rootFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
      StorageFolder assetFolder = await rootFolder.GetFolderAsync("Assets");
      StorageFile file = await assetFolder.GetFileAsync("CommandList.csv");
      Stream stream = await file.OpenStreamForReadAsync();
      StreamReader reader = new StreamReader(stream);
      var csv = new CsvHelper.CsvReader(reader);
      records = new ObservableCollection<Common.Commands>(csv.GetRecords<Common.Commands>());
      suggestBox.ItemsSource = records;
    }

    private void suggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args) {
      suggestBox.Text += " Chosen";
    }

    private void suggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args) {
      if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) {
      }
    }

  }
}
