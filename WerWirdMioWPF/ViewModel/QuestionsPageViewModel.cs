using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using WerWirdMioWPF.Model;
using WerWirdMioWPF.Service;

namespace WerWirdMioWPF.ViewModel
{
    public class QuestionsPageViewModel : BaseViewModel
    {
        private List<GameStage> _stages;
        private int _currentStageIndex = 0;


        private MediaPlayer _mediaPlayer = new MediaPlayer();

        private Question _currentQuestion;


        private Boolean usedThisRoundJoker;

        // UI Bindings
        public string CurrentQuestionText => _currentQuestion?.question ?? "Lade Frage...";
        public string Answer1Text => formatTextForDisplay("A: " , _currentQuestion?.answer1);
        public string Answer2Text => formatTextForDisplay("B: " , _currentQuestion?.answer2);
        public string Answer3Text => formatTextForDisplay("C: " , _currentQuestion?.answer3);
        public string Answer4Text => formatTextForDisplay("D: ",  _currentQuestion?.answer4);

        public string CurrentPrizeDisplay => $"Aktuelle Frage für: {_stages[_currentStageIndex].GameStageName}";

        public ICommand AnswerCommand { get; }

        public ICommand SelectJokerCommand { get; }



        List<string> replacedAnswers = new List<string>();
        List<string> usedJokers = new List<string>();



        public String formatTextForDisplay(String prefix, String text)
        {
            if(replacedAnswers.Contains(text))
            {
                return text;
            }

            return prefix + text;
        }


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


            if (usedThisRoundJoker)
            {
                MessageBox.Show("Du kannst nur einen Joker pro Frage benutzen!");
                return;
            }

            String type = parameter.ToString();


            if(usedJokers.Contains(type))
            {
                MessageBox.Show("Du hast diesen Joker bereits verwendet!");
                return;
            }

            usedJokers.Add(type);

            if (type.Equals("Danielaffe-Joker"))
            {
                usedThisRoundJoker = true;


                int select = getRandomIntOrder(true).First(); // Nur eine Antwort entfernen, daher Take(1) und First()





                      replaceAnswerText("Daniel hat die Antwort gegessen",select);
                    playDanielSound();

            }
                if (type.Equals("50-50-Joker"))
                {
                    usedThisRoundJoker = true;

                    foreach(int select in getRandomIntOrder(true).Take(2))
                    {
                         replaceAnswerText("Nicht verfügbar",select);
                    }


                }

        }



        private void playHuetherSound()
        {
            _mediaPlayer.Open(new Uri("Assets/huther.wav", UriKind.RelativeOrAbsolute));
            _mediaPlayer.Play();
        }


        private void playRetrySound()
        {
            _mediaPlayer.Open(new Uri("Assets/neuerversuch.wav", UriKind.RelativeOrAbsolute));
            _mediaPlayer.Play();
        }

        private void playDanielSound()
        {
            _mediaPlayer.Open(new Uri("Assets/leckerschmecker.wav", UriKind.RelativeOrAbsolute));
            _mediaPlayer.Play();
        }



        private void replaceAnswerText(String toReplaceWith, int index)
        {


            switch (index)
            {
                case 1:
                    _currentQuestion.answer1 = toReplaceWith;
                    replacedAnswers.Add(toReplaceWith);
                    break;
                case 2:
                    _currentQuestion.answer2 = toReplaceWith;
                    replacedAnswers.Add(toReplaceWith);
                    break;
                case 3:
                    _currentQuestion.answer3 = toReplaceWith;
                    replacedAnswers.Add(toReplaceWith);
                    break;
                case 4:
                    _currentQuestion.answer4 = toReplaceWith;
                    replacedAnswers.Add(toReplaceWith);
                    break;
            }


            updateDisplay();



        }

        private List<int> getRandomIntOrder(Boolean removeRightAnswer)
        {
            List<int> arr = new List<int>() { 1, 2, 3, 4 };
            if(removeRightAnswer)
                arr.Remove(_currentQuestion.correctAnswer);
            
            Random.Shared.Shuffle(System.Runtime.InteropServices.CollectionsMarshal.AsSpan(arr));

            return arr;

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


            updateDisplay();
        }


        public void updateDisplay()
        {
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
                if (replacedAnswers.Contains(getText(selectedAnswerIndex)))
                {
                    if(getText(selectedAnswerIndex).Equals("Nicht verfügbar"))
                    {
                        MessageBox.Show("Diese Antwort wurde durch den 50-50-Joker entfernt!");
                    }
                    else if(getText(selectedAnswerIndex).Equals("Daniel hat die Antwort gegessen"))
                    {
                        MessageBox.Show("Diese Antwort wurde durch den Daniel Affe gegessen!");
                    }

                    return;

                }


                usedThisRoundJoker = false; 
                replacedAnswers.Clear();
                // Joker zurücksetzen für die nächste Frage

                if (selectedAnswerIndex == _currentQuestion.correctAnswer)
                {
                    if (_currentStageIndex == _stages.Count - 1) // 1 Millionen Frage erreicht
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
                    int wonAmount = GetSafeZoneAmount();


                  if(UserName == null)
                    {
                        playRetrySound();

                    }


                    if (UserName != null)
                    {
                        String lowcase = UserName.ToLower();

                        if (lowcase == "hüther" || lowcase == "carsten" || lowcase == "huether")
                            playHuetherSound();
                        else
                            playRetrySound();


                    }
                      


                    EndGame(wonAmount, "Falsche Antwort! Die richtige Antwort war " + getRightAnswerText() + ". Du hast " + wonAmount + " € erhalten!");
                }
            }
        }


        public String getText(int index)
        {


            if (index == 1)
            {
                return Answer1Text;
            }
            if (index == 2)
            {
                return Answer2Text;
            }
            if (index == 3)
            {
                return Answer3Text;
            }
            if (index == 4)
            {
                return Answer4Text;
            }


            return "";
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

            usedJokers.Clear();

            gameService.highscoreService.AddOrUpdateScore(gameService.UserName, score);

            onPlayPage(gameService);
        }
    }



    

}