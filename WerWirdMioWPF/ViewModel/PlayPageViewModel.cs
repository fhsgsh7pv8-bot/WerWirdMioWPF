using System.Windows.Input;
using WerWirdMioWPF.Service;

namespace WerWirdMioWPF.ViewModel
{
    public class PlayPageViewModel : BaseViewModel
    {

        private readonly DelegateCommand _changeuser;
        public ICommand ChangeUserCommand { get { return _changeuser; } }


        private readonly DelegateCommand _leaderboard;
        public ICommand LeaderboardPageCommand { get { return _leaderboard; } }


        public PlayPageViewModel(GameService gameService) : base(gameService)
        {
            _changeuser = new DelegateCommand(onChangeUser);
            _leaderboard = new DelegateCommand(onLeaderboard);
        }

        private void onChangeUser(object parameters)
        {
            this.onStartPage(parameters);
        }

        private void onLeaderboard(object parameters)
        {
            this.onHighScorePage(parameters);

        }
    }
}
