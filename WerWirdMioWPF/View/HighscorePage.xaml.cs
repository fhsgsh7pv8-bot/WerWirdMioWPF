using System.Windows.Controls;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.ViewModel;

namespace WerWirdMioWPF.View
{
    /// <summary>
    /// Interaktionslogik für Highscores.xaml
    /// </summary>
    public partial class HighscorePage : Page
    {

        HighscorePageViewModel highscorePageViewModel;
        public HighscorePage(GameService gameService)
        {
            InitializeComponent();

            highscorePageViewModel = new HighscorePageViewModel(gameService);
            DataContext = highscorePageViewModel;
        }
    }
}
