using System.Windows.Navigation;

namespace WerWirdMioWPF.Service
{
    public class GameService
    {

        public QuestionService questionService;
        public SoundService soundService;
        public NavigationService navigationService;
        public HighscoreService highscoreService;

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
