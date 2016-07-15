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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FurbMote.Controls {
  public sealed partial class BigColorButton : UserControl {
    public BigColorButton() {
      this.InitializeComponent();
    }

    public static readonly DependencyProperty BackColorProperty = DependencyProperty.Register("BackColor", typeof(SolidColorBrush), typeof(BigColorButton), null);
    public static readonly DependencyProperty BackColorHoverProperty = DependencyProperty.Register("BackColorHover", typeof(SolidColorBrush), typeof(BigColorButton), null);
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(BigColorButton), null);
    public static readonly DependencyProperty ColorChangeProperty = DependencyProperty.Register("UseThemeForHoverColor", typeof(bool), typeof(BigColorButton), null);

    public SolidColorBrush BackColor { set { grid.Background = value; BgColor = value; } }
    public SolidColorBrush BackColorHover { set { BgColorHover = value; } }
    public string Title { set { text.Text = value; } }
    public bool UseThemeForHoverColor { set { BgChange = value; } }

    SolidColorBrush BgColor;
    SolidColorBrush BgColorHover;
    bool BgChange;

    private void grid_PointerEntered(object sender, PointerRoutedEventArgs e) {
      if (BgChange) {
        if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
          grid.Background = new SolidColorBrush(Common.ShiftColor(BgColor.Color, 25, false));

        else if (Application.Current.RequestedTheme == ApplicationTheme.Light)
          grid.Background = new SolidColorBrush(Common.ShiftColor(BgColor.Color, 25, true));
      }
    }

    private void grid_PointerExited(object sender, PointerRoutedEventArgs e) {
      grid.Background = BgColor;
    }
  }
}
