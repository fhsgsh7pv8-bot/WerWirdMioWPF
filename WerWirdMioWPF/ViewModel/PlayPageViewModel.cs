using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WerWirdMioWPF.Service;

namespace WerWirdMioWPF.ViewModel
{
    public class PlayPageViewModel : BaseViewModel
    {

        private readonly DelegateCommand _changeuser;
        public ICommand ChangeUserCommand { get { return _changeuser; } }


        public PlayPageViewModel(GameService gameService) : base(gameService)
        {
            _changeuser = new DelegateCommand(onChangeUser);
        }
        
        private void onChangeUser(object parameters)
        {
            this.onStartPage(parameters);
        }
    
    }

}
