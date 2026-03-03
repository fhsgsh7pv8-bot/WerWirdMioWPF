using System.Windows.Input;
using WerWirdMioWPF.Service;

namespace WerWirdMioWPF.ViewModel
{
    public class LosePageViewModel : BaseViewModel
    {


        private readonly DelegateCommand _backcommand;
        public ICommand BackCommand { get { return _backcommand; } }



        public String EndMessage { get { return _endmessage; } set { _endmessage = value; RaisePropertyChanged("EndMessage"); } }

        private String _endmessage = "";

        public LosePageViewModel(GameService gameService) : base(gameService)
        {

            _backcommand = new DelegateCommand(onBackCommand);
        }


        private void onBackCommand(object parameters)
        {

            this.onPlayPage(parameters);

        }


        public void setEndMessage(String message)
        {
            EndMessage = message;
        }


    }
}