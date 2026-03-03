using System.Windows.Controls;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.ViewModel;

namespace WerWirdMioWPF.View
{
    /// <summary>
    /// Interaktionslogik für PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Page
    {

        PlayPageViewModel playPageViewModel;

        public PlayPage(GameService gameService)
        {
            InitializeComponent();

            playPageViewModel = new PlayPageViewModel(gameService);
            DataContext = playPageViewModel;
        }


    }
}
