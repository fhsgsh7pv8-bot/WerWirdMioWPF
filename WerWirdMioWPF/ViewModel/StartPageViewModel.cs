using System.Windows.Input;
using WerWirdMioWPF.Service;

namespace WerWirdMioWPF.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {


        private readonly DelegateCommand _setusernamecommand;
        public ICommand SetUserNameCommand { get { return _setusernamecommand; } }



        public StartPageViewModel(GameService gameService) : base(gameService)
        {

            _setusernamecommand = new DelegateCommand(onSetUsername);
        }


        private void onSetUsername(object parameters)
        {

            this.onPlayPage(parameters);

        }


    }
}
