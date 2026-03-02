using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WerWirdMioWPF.View
{
    /// <summary>
    /// Interaktionslogik für EndPage.xaml
    /// </summary>
    public partial class EndPage : Page
    {
        public EndPage()
        {

            InitializeComponent();

            string videoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "tikiland-video.mp4");

            if (System.IO.File.Exists(videoPath))
            {
                BackgroundVideo.Source = new Uri(videoPath);
                BackgroundVideo.Play();
            }
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.FromMilliseconds(1);
            BackgroundVideo.Play();
        }
    }
}
