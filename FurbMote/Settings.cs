using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurbMote {
  public class Settings {
    private static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

    public static bool? ShowAdvanced {
      get { return (bool?)localSettings.Values["ShowAdvanced"]; }
      set { localSettings.Values["ShowAdvanced"] = value; }
    }

    public static bool? CheckFiles {	
      get { return (bool?)localSettings.Values["CheckFiles"]; }	
      set { localSettings.Values["CheckFiles"] = value; }
    }
  }
}
