using System.Windows;
using System.Windows.Controls;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.ViewModel;

namespace WerWirdMioWPF.View
{
    /// <summary>
    /// Interaktionslogik für QuestionsPage.xaml
    /// </summary>
    public partial class QuestionsPage : Page
    {
        QuestionsPageViewModel questionsPageViewModel;

        public QuestionsPage(GameService gameService)
        {
            InitializeComponent();

            questionsPageViewModel = new QuestionsPageViewModel(gameService);
            DataContext = questionsPageViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
