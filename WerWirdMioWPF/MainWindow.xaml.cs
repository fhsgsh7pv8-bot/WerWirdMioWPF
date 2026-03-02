using System;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.View;

namespace WerWirdMioWPF
{
    public partial class MainWindow : Window
    {
        // Wir erstellen einen MediaPlayer als Klassenvariable
        private MediaPlayer _mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();

            // 1. Startsound abspielen
            PlayStartSound();

            GameService gameService = new GameService();
            gameService.navigationService = _NavigationFrame.NavigationService;
            _NavigationFrame.Navigate(new StartPage(gameService));
        }

        private void PlayStartSound()
        {
            // Pfad zur Datei (Stelle sicher, dass die Dateien im Ausgabeverzeichnis liegen)
            _mediaPlayer.Open(new Uri("Assets/startsound.mp3", UriKind.RelativeOrAbsolute));

            // Event-Handler: Wenn der Startsound fertig ist, starte die Main-Music
            _mediaPlayer.MediaEnded += StartMainMusicLoop;

            _mediaPlayer.Play();
        }

        private void StartMainMusicLoop(object sender, EventArgs e)
        {
            // Alten Event-Handler entfernen, damit er nicht jedes Mal neu triggert
            _mediaPlayer.MediaEnded -= StartMainMusicLoop;

            // 2. Main Music laden
            _mediaPlayer.Open(new Uri("Assets/mainmusic.mp3", UriKind.RelativeOrAbsolute));

            // Event-Handler für den Loop hinzufügen
            _mediaPlayer.MediaEnded += (s, args) =>
            {
                _mediaPlayer.Position = TimeSpan.Zero; // Zurück zum Anfang
                _mediaPlayer.Play();
            };

            _mediaPlayer.Play();
        }

        private void _NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Deine Logik hier
        }
    }
}