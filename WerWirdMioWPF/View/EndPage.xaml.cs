using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WerWirdMioWPF.View
{
    public partial class EndPage : Page
    {
        // Separater Player für den End-Sound
        private MediaPlayer _endSoundPlayer = new MediaPlayer();

        public EndPage()
        {
            InitializeComponent();

            // 1. Video-Logik
            string videoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "tikiland-video.mp4");

            if (System.IO.File.Exists(videoPath))
            {
                BackgroundVideo.Source = new Uri(videoPath);
                BackgroundVideo.Play();
            }

            // 2. Sound-Logik: Sobald die Seite geladen ist, wird der Sound abgespielt
            this.Loaded += EndPage_Loaded;
        }

        private void EndPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pfad zur missionpassed.mp3 
                string soundPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "missionpassed.mp3");

                if (System.IO.File.Exists(soundPath))
                {
                    _endSoundPlayer.Open(new Uri(soundPath));
                    _endSoundPlayer.Volume = 0.5; // Lautstärke auf 50%
                    _endSoundPlayer.Play();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Fehler beim Abspielen des Sounds: " + ex.Message);
            }
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.FromMilliseconds(1);
            BackgroundVideo.Play();
        }
    }
}