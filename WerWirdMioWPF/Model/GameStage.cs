namespace WerWirdMioWPF.Model
{
    public class GameStage
    {
        public string GameStageName { get; set; }
        public int Difficulty { get; set; }
        public int PrizeAmount { get; set; }
        public bool IsSafeZone { get; set; }

        public GameStage(int difficulty, string name, int prize, bool isSafeZone = false)
        {
            Difficulty = difficulty;
            GameStageName = name;
            PrizeAmount = prize;
            IsSafeZone = isSafeZone;
        }
    }
}
