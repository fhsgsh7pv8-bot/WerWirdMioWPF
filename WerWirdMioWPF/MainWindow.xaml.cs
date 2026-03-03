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
        private MediaPlayer _backgroundPlayer = new MediaPlayer();
        private MediaPlayer _uiPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            _backgroundPlayer.Volume = 0.07;
            _uiPlayer.Volume = 0.2;

            this.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnGlobalButtonClicked));

            PlayStartSequence();

            GameService gameService = new GameService();
            gameService.navigationService = _NavigationFrame.NavigationService;
            _NavigationFrame.Navigate(new StartPage(gameService));
        }

        private void PlayStartSequence()
        {
            _backgroundPlayer.Open(new Uri("Assets/startsound.mp3", UriKind.Relative));

            _backgroundPlayer.MediaEnded += TransitionToMainMusic;

            _backgroundPlayer.Play();
        }

        private void TransitionToMainMusic(object sender, EventArgs e)
        {
            _backgroundPlayer.MediaEnded -= TransitionToMainMusic;

            _backgroundPlayer.Open(new Uri("Assets/mainmusic.mp3", UriKind.Relative));

            _backgroundPlayer.MediaEnded += (s, args) =>
            {
                _backgroundPlayer.Position = TimeSpan.Zero;
                _backgroundPlayer.Play();
            };

            _backgroundPlayer.Play();
        }

        private void OnGlobalButtonClicked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button || e.Source is Button)
            {
                try
                {
                    _uiPlayer.Open(new Uri("Assets/buttonsound.mp3", UriKind.Relative));

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