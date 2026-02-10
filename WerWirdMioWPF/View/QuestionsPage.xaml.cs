using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

    }
}
