using Newtonsoft.Json;
using WerWirdMioWPF.Model;

namespace WerWirdMioWPF.Service
{
    public class HighscoreService
    {
        private const string FilePath = "highscores.json";
        public List<UserScore> Leaderboard { get; private set; }

        public HighscoreService()
        {
            Leaderboard = new List<UserScore>();
            LoadScores();
        }

        private void LoadScores()
        {

            if (System.IO.File.Exists(FilePath))
            {
                string json = System.IO.File.ReadAllText(FilePath);
                var items = JsonConvert.DeserializeObject<List<UserScore>>(json);
                if (items != null)
                {
                    Leaderboard = items;
                }
            }
        }

        public void AddOrUpdateScore(string userName, int wonAmount)
        {
            if (string.IsNullOrWhiteSpace(userName))
                userName = "Anonym";

            var existingUser = Leaderboard.FirstOrDefault(u => u.UserName == userName);

            if (existingUser != null)
            {
                if (existingUser.TotalScore <= wonAmount)
                {
                    existingUser.TotalScore = wonAmount;
                }
            }
            else
            {
                Leaderboard.Add(new UserScore { UserName = userName, TotalScore = wonAmount });
            }

            Leaderboard = Leaderboard.OrderByDescending(u => u.TotalScore).ToList();

            SaveScores();
        }

        private void SaveScores()
        {
            string json = JsonConvert.SerializeObject(Leaderboard, Formatting.Indented);
            System.IO.File.WriteAllText(FilePath, json);
        }
    }
}
