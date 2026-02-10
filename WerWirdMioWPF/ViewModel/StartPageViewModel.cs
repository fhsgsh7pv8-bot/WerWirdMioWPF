using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WerWirdMioWPF.Service;
using WerWirdMioWPF.View;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
