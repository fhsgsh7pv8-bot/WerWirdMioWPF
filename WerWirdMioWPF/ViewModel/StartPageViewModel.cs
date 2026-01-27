using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WerWirdMioWPF.View;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WerWirdMioWPF.ViewModel
{
    public class StartPageViewModel : BaseViewModel
    {

        public string UserName
        {
            get { return _username; }
            set
            {

                _username = value;
                RaisePropertyChanged("UserName");
              
            }
        }


        private string _username;






        private readonly DelegateCommand _setusernamecommand;
        public ICommand SetUserNameCommand { get { return _setusernamecommand; } }






        public StartPageViewModel(NavigationService navi) : base(navi, "")
        {

            _setusernamecommand = new DelegateCommand(onSetUsername);
        }





        private void onSetUsername(object parameters)
        {
           // navigationService.Navigate(new PlayPage(navigationService, userName));
            this.onPlayPage(parameters);

            MessageBox.Show(UserName);
        }


    }
}
