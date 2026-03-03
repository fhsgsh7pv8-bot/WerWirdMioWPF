using WerWirdMioWPF.Model;

namespace WerWirdMioWPF.Service
{
    public class QuestionService
    {

        Random random = new Random();
        Dictionary<int, List<Question>> questions = new Dictionary<int, List<Question>>();

        public Question getRandomQuestionFromDifficulty(int difficulty)
        {
            if (questions.ContainsKey(difficulty))
            {
                List<Question> list = questions[difficulty];
                int index = random.Next(list.Count);
                return list[index];
            }
            else
            {
                return null;
            }
        }

        public void loadQuestions()
        {

            this.questions.Clear();

            List<Question> importedQuestions = File.FileUtil.ReadJsonFromFileAsync("questions.json").Result;
            foreach (Question q in importedQuestions)
            {
                if (questions.ContainsKey(q.diffuculty))
                {
                    List<Question> list = questions[q.diffuculty];
                    list.Add(q);
                }
                else
                {
                    questions.Add(q.diffuculty, new List<Question>() { q });
                }
            }


        }


    }
}
