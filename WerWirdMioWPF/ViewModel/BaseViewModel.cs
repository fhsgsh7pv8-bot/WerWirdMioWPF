using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;
using WerWirdMioWPF.File; // Falls für das Auskommentierte benötigt
using WerWirdMioWPF.Model;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.View;

namespace WerWirdMioWPF.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public NavigationService navigationService;

        private readonly DelegateCommand _startpagecommand;
        public ICommand StartPageCommand { get { return _startpagecommand; } }

        private readonly DelegateCommand _playpagecommand;
        public ICommand PlayPageCommand { get { return _playpagecommand; } }

        private readonly DelegateCommand _highscorepagecommand;
        public ICommand HighScorePageCommand { get { return _highscorepagecommand; } }

        private readonly DelegateCommand _endpagecommand;
        public ICommand EndPageCommand { get { return _endpagecommand; } }

        private readonly DelegateCommand _questionspagecommand;
        public ICommand QuestionsPageCommand { get { return _questionspagecommand; } }

        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                // WICHTIG: Name auch im Service speichern
                if (gameService != null)
                {
                    gameService.UserName = value;
                }
                RaisePropertyChanged("UserName");
            }
        }

        private string _username;

        public GameService gameService;
        public QuestionService questionService;

        public BaseViewModel(GameService gameService)
        {
            this.gameService = gameService;
            this.questionService = gameService.questionService;
            this.navigationService = gameService.navigationService;

            _username = gameService.UserName;

            // Optional: Einmalig einkommentieren, um questions.json zu erstellen, falls noch nicht vorhanden
            /*
            List<Question> Questions = new List<Question>();
            for (int i = 0; i < 15; i++)
            {
                Question q = new Question();
                q.question = "Frage " + (i + 1);
                q.answer1 = "Antwort 1";
                q.answer2 = "Antwort 2";
                q.answer3 = "Antwort 3";
                q.answer4 = "Antwort 4";
                q.correctAnswer = 1;
                q.diffuculty = i / 5 + 1; // Beachtet den Tippfehler im Model
                Questions.Add(q);
            }
            FileUtil.WriteQuestions("questions.json", Questions);
            */

            _startpagecommand = new DelegateCommand(onStartPage);
            _playpagecommand = new DelegateCommand(onPlayPage);
            _highscorepagecommand = new DelegateCommand(onHighScorePage);
            _endpagecommand = new DelegateCommand(onEndPage);
            _questionspagecommand = new DelegateCommand(onQuestionsPage);
        }

        public void onQuestionsPage(object param)
        {
            navigationService.Navigate(new QuestionsPage(gameService));
        }

        public void onStartPage(object param)
        {
            navigationService.Navigate(new StartPage(gameService));
        }

        public void onPlayPage(object parameters)
        {
            navigationService.Navigate(new PlayPage(gameService));
        }

        public void onHighScorePage(object param)
        {
            navigationService.Navigate(new HighscorePage());
        }

        public void onEndPage(object param)
        {
            navigationService.Navigate(new EndPage());
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}