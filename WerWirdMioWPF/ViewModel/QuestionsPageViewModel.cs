using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using WerWirdMioWPF.Model;
using WerWirdMioWPF.Service;

namespace WerWirdMioWPF.ViewModel
{
    public class QuestionsPageViewModel : BaseViewModel
    {
        private Question _currentQuestion;

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                RaisePropertyChanged("CurrentQuestion");
            }
        }

        public QuestionsPageViewModel(GameService gameService) : base(gameService)
        {
            // Lade direkt beim Start eine Frage der Schwierigkeit 1
            LoadQuestion(1);
        }

        private void LoadQuestion(int difficulty)
        {
            // Hole Frage aus dem Service
            var q = this.gameService.questionService.getRandomQuestionFromDifficulty(difficulty);
            CurrentQuestion = new Question()
            {
                question = "Fehler: Keine Fragen gefunden. (Existiert questions.json?)",
                answer1 = "-",
                answer2 = "-",
                answer3 = "-",
                answer4 = "-"
            };
            /*
        if (q != null)
        {
            CurrentQuestion = q;
        }
        else
        {
            // Fallback, falls keine Fragen geladen wurden (z.B. questions.json fehlt)
            CurrentQuestion = new Question()
            {
                question = "Fehler: Keine Fragen gefunden. (Existiert questions.json?)",
                answer1 = "-",
                answer2 = "-",
                answer3 = "-",
                answer4 = "-"
            };
        }
            */
        }
    }
}