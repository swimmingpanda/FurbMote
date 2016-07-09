using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FurbMote
{
    public class Common
    {
        public static void PlayMedia(string source, Grid element)
        {
            Uri uriSource = new Uri(source);
            MediaElement mediaElement = new MediaElement();
            element.Children.Add(mediaElement);
            mediaElement.Stop();
            mediaElement.Source = uriSource;
            mediaElement.Play();
        }
    }
}
