using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerWirdMioWPF.Model;
using WerWirdMioWPF.Service;

namespace WerWirdMioWPF.ViewModel
{
    public class HighscorePageViewModel : BaseViewModel
    {
        // ObservableCollection aktualisiert die UI automatisch, wenn sich etwas ändert
        public ObservableCollection<UserScore> TopScores { get; set; }


        public readonly DelegateCommand _backcommand;
        public DelegateCommand BackCommand { get { return _backcommand; } }



     
        public HighscorePageViewModel(GameService gameService) : base(gameService)
        {
            // Die sortierte Liste aus dem Service in die UI-Collection laden
            TopScores = new ObservableCollection<UserScore>(gameService.highscoreService.Leaderboard);
            _backcommand = new DelegateCommand(onBackCommand);
        }


        public void onBackCommand(object parameters)
        {
            // Zurück zur Startseite navigieren
            onStartPage(gameService);
        }
    }
}
