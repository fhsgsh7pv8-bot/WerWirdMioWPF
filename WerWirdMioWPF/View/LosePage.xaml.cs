using System.Windows.Controls;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.ViewModel;

namespace WerWirdMioWPF.View
{
    /// <summary>
    /// Interaktionslogik für LosePage.xaml
    /// </summary>
    public partial class LosePage : Page
    {
        LosePageViewModel losePageViewModel;

        public LosePage(GameService gameService, String param)
        {
            InitializeComponent();

            losePageViewModel = new LosePageViewModel(gameService);
            DataContext = losePageViewModel;

            losePageViewModel.setEndMessage(param);
        }


    }
}
