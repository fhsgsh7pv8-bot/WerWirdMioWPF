using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WerWirdMioWPF.Service
{
    public class GameService
    {

        public QuestionService questionService { get; }
        public SoundService soundService { get; }
        public NavigationService navigationService { get; }
        public HighscoreService highscoreService { get; }

        public String UserName;

        public GameService()
        {
            questionService = new QuestionService();
            soundService = new SoundService();
            highscoreService = new HighscoreService();

            questionService.loadQuestions();
        }


        


    }
}
