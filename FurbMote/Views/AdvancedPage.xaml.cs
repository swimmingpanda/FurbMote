using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FurbMote.Views {
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class AdvancedPage : Page {

    ObservableCollection<Common.Commands> records;

    public AdvancedPage() {
      this.InitializeComponent();
      Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
    }

    private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e) {
      AppShell.NavFrameNavigate(typeof(MainPage), Windows.UI.Color.FromArgb(0, 0x00, 0xAB, 0xA9), "Furbmote");// Frame.Navigate(typeof(MainPage));
      e.Handled = true;
    }

    /// <summary>
    /// Invoked when this page is about to be displayed in a Frame.
    /// </summary>
    /// <param name="e">Event data that describes how this page was reached.
    /// This parameter is typically used to configure the page.</param>
    protected override async void OnNavigatedTo(NavigationEventArgs e) {
      records = await Common.ReadCsv();
      suggestBox.ItemsSource = records;
      commandListView.ItemsSource = records;
    }

    private void suggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args) {
      if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) {
        ObservableCollection<Common.Commands> tList = new ObservableCollection<Common.Commands>();
        foreach (Common.Commands item in records) {
          if (item.Entry.ToLowerInvariant().Contains(sender.Text.ToLowerInvariant()))
            tList.Add(item);
        }
        commandListView.ItemsSource = tList;
      }
    }

    private void commandListView_ItemClick(object sender, ItemClickEventArgs e) {
      Common.Commands item = (Common.Commands)e.ClickedItem;
      Common.PlayFurbyCommand(item.Name, layoutRoot);
    }
  }
}
