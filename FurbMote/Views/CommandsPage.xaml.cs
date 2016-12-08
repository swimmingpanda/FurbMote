using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FurbMote.Views {
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class CommandsPage : Page {

    ObservableCollection<Common.Commands> records;
    ObservableCollection<Common.Commands> firstList = new ObservableCollection<Common.Commands>();
    ObservableCollection<Common.Commands> secondList = new ObservableCollection<Common.Commands>();

    public CommandsPage() {
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
    protected override async void OnNavigatedTo(NavigationEventArgs e) {
      records = await Common.ReadCsv();
      foreach (Common.Commands item in records) {
        int id = item.Id;
        switch (id) {
          //first list
          case (862):
            firstList.Add(item);
            break;

          case (863):
            firstList.Add(item);
            break;

          case (867):
            firstList.Add(item);
            break;

          case (865):
            firstList.Add(item);
            break;

          case (869):
            firstList.Add(item);
            break;

          //second list
          case (874):
            secondList.Add(item);
            break;

          case (877):
            secondList.Add(item);
            break;

          case (864):
            secondList.Add(item);
            break;

          case (866):
            secondList.Add(item);
            break;

          case (868):
            secondList.Add(item);
            break;
        }
      }

      firstCommandListView.ItemsSource = firstList;
      secondCommandListView.ItemsSource = secondList;
    }

    private void CommandListView_ItemClick(object sender, ItemClickEventArgs e) {
      Common.Commands item = (Common.Commands)e.ClickedItem;
      Common.PlayFurbyCommand(item.Name, layoutRoot);
    }
  }
}
