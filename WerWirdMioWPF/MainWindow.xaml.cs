using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.View;

namespace WerWirdMioWPF
{
    public partial class MainWindow : Window
    {
        // Player für die Hintergrundmusik (Start -> Loop)
        private MediaPlayer _backgroundPlayer = new MediaPlayer();

        // Separater Player für den Button-Sound (Effekt-Kanal)
        private MediaPlayer _uiPlayer = new MediaPlayer();

        private GameService _gameService;

        public MainWindow()
        {
            InitializeComponent();

            // Service initialisieren (Wichtig, sonst gibt es eine NullReferenceException)
            _gameService = new GameService();

            // Lautstärke zentral einstellen
            _backgroundPlayer.Volume = 0.07;
            _uiPlayer.Volume = 0.2;

            // 1. Globaler Event-Handler für ALLE Buttons im Fenster
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnGlobalButtonClicked));

            // 2. Musik-Sequenz starten (Startsound -> Main Music)
            PlayStartSequence();

            // 3. Erste Seite laden
            _NavigationFrame.Navigate(new StartPage(_gameService));
        }

        private void PlayStartSequence()
        {
            // Startsound laden
            _backgroundPlayer.Open(new Uri("Assets/startsound.mp3", UriKind.Relative));

            // Event: Wenn Startsound zu Ende, wechsle zur Main-Music
            _backgroundPlayer.MediaEnded += TransitionToMainMusic;

            _backgroundPlayer.Play();
        }

        private void TransitionToMainMusic(object sender, EventArgs e)
        {
            // Alten Handler entfernen
            _backgroundPlayer.MediaEnded -= TransitionToMainMusic;

            // Endlos-Musik laden
            _backgroundPlayer.Open(new Uri("Assets/mainmusic.mp3", UriKind.Relative));

            // Loop-Logik: Immer wieder von vorne abspielen
            _backgroundPlayer.MediaEnded += (s, args) =>
            {
                _backgroundPlayer.Position = TimeSpan.Zero;
                _backgroundPlayer.Play();
            };

            _backgroundPlayer.Play();
        }

        private void OnGlobalButtonClicked(object sender, RoutedEventArgs e)
        {
            // Sound für Klicks abspielen
            _uiPlayer.Open(new Uri("Assets/click.mp3", UriKind.Relative));
            _uiPlayer.Stop(); // Zurücksetzen, falls schnell hintereinander geklickt wird
            _uiPlayer.Play();
        }

        private void _NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Platz für zusätzliche Navigations-Logik
        }
    }
}