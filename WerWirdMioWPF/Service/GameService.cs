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

        public QuestionService questionService = new QuestionService();
        public NavigationService navigationService;

        public String UserName;

        public GameService()
        {
            questionService.loadQuestions();
        }


    }
}
