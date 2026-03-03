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

        public MainWindow()
        {
            InitializeComponent();

            // Lautstärke zentral einstellen
            _backgroundPlayer.Volume = 0.07;
            _uiPlayer.Volume = 0.2;

            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnGlobalButtonClicked));

            PlayStartSequence();

            // 3. Erste Seite laden
            GameService gameService = new GameService();
            gameService.navigationService = _NavigationFrame.NavigationService;
            _NavigationFrame.Navigate(new StartPage(gameService));
        }

        private void PlayStartSequence()
        {
            // Startsound laden
            _backgroundPlayer.Open(new Uri("Assets/startsound.mp3", UriKind.Relative));

           // wechsle zur Main-Music
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
            // Wir prüfen, ob die Quelle des Klicks wirklich ein Button (oder ein Teil eines Buttons) ist
            if (e.OriginalSource is Button || e.Source is Button)
            {
                try
                {
                    // Pfad zur Sounddatei
                    _uiPlayer.Open(new Uri("Assets/buttonsound.mp3", UriKind.Relative));

                    // Wichtig: Stop und Position auf Null, damit der Sound bei schnellen Klicks sofort neu triggert
                    _uiPlayer.Stop();
                    _uiPlayer.Position = TimeSpan.Zero;
                    _uiPlayer.Play();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Sound konnte nicht abgespielt werden: " + ex.Message);
                }
            }
        }

        private void _NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            
        }
    }
}