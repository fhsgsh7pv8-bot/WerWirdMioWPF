using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WerWirdMioWPF.ViewModel
{
    internal class PlayPageViewModel : BaseViewModel
    {

        private readonly DelegateCommand _changeuser;
        public ICommand ChangeUserCommand { get { return _changeuser; } }







        public PlayPageViewModel(NavigationService navi, String username) : base(navi, username)
        {
            _changeuser = new DelegateCommand(onChangeUser);
        }
        
        private void onChangeUser(object parameters)
        {
            this.onStartPage(parameters);
        }
    
    }

}
