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

            _mediaPlayer.Volume = 0.07;
            InitializeComponent();

   

            // 1. Startsound abspielen
            PlayStartSound();

            // 2. Globaler Event-Handler für ALLE Buttons im Spiel
            // Registriert den Sound-Effekt zentral für das gesamte Fenster
            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnGlobalButtonClicked));

            // 3. Musik-Sequenz starten (Startsound -> Main Music)
            PlayStartSequence();

            // 4. Erste Seite laden
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
            // Handler entfernen, damit er nicht erneut feuert
            _backgroundPlayer.MediaEnded -= TransitionToMainMusic;

            // Endlos-Musik laden
            _backgroundPlayer.Open(new Uri("Assets/mainmusic.mp3", UriKind.Relative));

            // Loop-Logik: Immer wieder von vorne abspielen
            _backgroundPlayer.MediaEnded += (s, args) =>
            {
                _mediaPlayer.Position = TimeSpan.Zero; // Zurück zum Anfang

                _mediaPlayer.Play();
            };


            _mediaPlayer.Play();
        }

        private void _NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Platz für zusätzliche Navigations-Logik
        }
    }
}