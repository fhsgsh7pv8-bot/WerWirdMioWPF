using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WerWirdMioWPF.Model;
using WerWirdMioWPF.Service;

namespace WerWirdMioWPF.ViewModel
{
    public class QuestionsPageViewModel : BaseViewModel
    {
        private List<GameStage> _stages;
        private int _currentStageIndex = 0;
        private Question _currentQuestion;


        private Boolean usedThisRoundJoker;

        // UI Bindings
        public string CurrentQuestionText => _currentQuestion?.question ?? "Lade Frage...";
        public string Answer1Text => $"A: {_currentQuestion?.answer1}";
        public string Answer2Text => $"B: {_currentQuestion?.answer2}";
        public string Answer3Text => $"C: {_currentQuestion?.answer3}";
        public string Answer4Text => $"D: {_currentQuestion?.answer4}";

        public string CurrentPrizeDisplay => $"Aktuelle Frage für: {_stages[_currentStageIndex].GameStageName}";

        public ICommand AnswerCommand { get; }

        public ICommand SelectJokerCommand { get; }

        public QuestionsPageViewModel(GameService gameService) : base(gameService)
        {
            AnswerCommand = new DelegateCommand(OnAnswerSelected);
            SelectJokerCommand = new DelegateCommand(OnSelectJoker);
            InitializeStages();
            LoadNextQuestion();

            usedThisRoundJoker = false;
        }


        //50/50 JOKER
        //Danielaffe joker (nur einer geht weg)

        private void OnSelectJoker(object parameter)
        {
            if(usedThisRoundJoker)
            {
                MessageBox.Show("Du hast einen Joker bereits in dieser Runde verwendet!");
                return;
            }


        }

        private void InitializeStages()
        {
            _stages = new List<GameStage>
            {
                new GameStage(1, "50 €", 50),
                new GameStage(2, "100 €", 100),
                new GameStage(3, "200 €", 200),
                new GameStage(4, "300 €", 300),
                new GameStage(5, "500 €", 500, true),

                new GameStage(6, "1.000 €", 1000),
                new GameStage(7, "2.000 €", 2000),
                new GameStage(8, "4.000 €", 4000),
                new GameStage(9, "8.000 €", 8000),
                new GameStage(10, "16.000 €", 16000),
                new GameStage(11, "32.000 €", 32000, true), 
                new GameStage(12, "64.000 €", 64000),
                new GameStage(13, "125.000 €", 125000),
                new GameStage(14, "500.000 €", 500000),
                new GameStage(15, "1.000.000 €", 1000000, true)
            };
        }

        private void LoadNextQuestion()
        {
            int currentDifficulty = _stages[_currentStageIndex].Difficulty;

            _currentQuestion = gameService.questionService.getRandomQuestionFromDifficulty(currentDifficulty);

            if (_currentQuestion == null)
            {
                MessageBox.Show("Fehler: Keine Frage für Stufe "+ currentDifficulty + " gefunden!");
                return;
            }


            RaisePropertyChanged(nameof(CurrentQuestionText));
            RaisePropertyChanged(nameof(Answer1Text));
            RaisePropertyChanged(nameof(Answer2Text));
            RaisePropertyChanged(nameof(Answer3Text));
            RaisePropertyChanged(nameof(Answer4Text));
            RaisePropertyChanged(nameof(CurrentPrizeDisplay));
        }

        private void OnAnswerSelected(object parameter)
        {
            if (int.TryParse(parameter.ToString(), out int selectedAnswerIndex))
            {
                if (selectedAnswerIndex == _currentQuestion.correctAnswer)
                {
                    if (_currentStageIndex == 14) // 1 Millionen Frage erreicht
                    {
                        EndGame(1000000, "Herzlichen Glückwunsch! Du bist MILLIONÄR!");
                    }
                    else
                    {
                        _currentStageIndex++;
                        LoadNextQuestion();
                    }
                }
                else
                {
                    // Falsche Antwort -> Zurückfallen auf die letzte Sicherheitsstufe
                    int wonAmount = GetSafeZoneAmount();
                    EndGame(wonAmount, $"Falsche Antwort! Die richtige Antwort war "+getRightAnswerText()+". Du hast " + wonAmount + " € erhalten!");
                }
            }
        }


        public String getRightAnswerText()
        {

            int correctAnswerIndex = _currentQuestion.correctAnswer;

            if (correctAnswerIndex == 1)
            {
                return _currentQuestion.answer1;
            }
            if(correctAnswerIndex == 2)
            {
                return _currentQuestion.answer2;
            }
            if(correctAnswerIndex == 3)
            {
                return _currentQuestion.answer3;
            }
            if(correctAnswerIndex == 4)
            {
                return _currentQuestion.answer4;
            }


            return "";
        }

        private int GetSafeZoneAmount()
        {
          
               
                 return _stages[_currentStageIndex].PrizeAmount == 50 ? 0 : _stages[_currentStageIndex].PrizeAmount;
             
        }

        private void EndGame(int score, string message)
        {
            MessageBox.Show(message, "Spiel beendet", MessageBoxButton.OK, MessageBoxImage.Information);


            gameService.highscoreService.AddOrUpdateScore(gameService.UserName, score);

            onPlayPage(gameService);
        }
    }
}