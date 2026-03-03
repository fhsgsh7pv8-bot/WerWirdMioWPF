using System.Windows.Controls;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.ViewModel;

namespace WerWirdMioWPF.View
{
    /// <summary>
    /// Interaktionslogik für StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {

        StartPageViewModel startPageViewModel;

        public StartPage(GameService gameService)
        {
            InitializeComponent();


            startPageViewModel = new StartPageViewModel(gameService);
            DataContext = startPageViewModel;
        }


    }
}

