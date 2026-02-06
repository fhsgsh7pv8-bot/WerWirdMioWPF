using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class BaseViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler? PropertyChanged;


        public NavigationService navigationService;



        private readonly DelegateCommand _startpagecommand;
        public ICommand StartPageCommand { get { return _startpagecommand; } }



        private readonly DelegateCommand _playpagecommand;
        public ICommand PlayPageCommand { get { return _playpagecommand; } }




        private readonly DelegateCommand _highscorepagecommand;
        public ICommand HighScorePageCommand { get { return _highscorepagecommand; } }




        private readonly DelegateCommand _endpagecommand;
        public ICommand EndPageCommand { get { return _endpagecommand; } }




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


        public BaseViewModel(NavigationService navi, string userName)
        {
            navigationService = navi;
            _username = userName;

            _startpagecommand = new DelegateCommand(onStartPage);
            _playpagecommand = new DelegateCommand(onPlayPage);
            _highscorepagecommand = new DelegateCommand(onHighScorePage);
            _endpagecommand = new DelegateCommand(onEndPage);

        }

    
        public void onStartPage(object param)
        {
            navigationService.Navigate(new StartPage(navigationService));
        }
        public void onPlayPage(object parameters)
        {
            navigationService.Navigate(new PlayPage(navigationService,UserName));
        }
        public void onHighScorePage(object param)
        {
            navigationService.Navigate(new HighscorePage());
        }
        public void onEndPage(object param)
        {
            navigationService.Navigate(new EndPage());
        }






        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }

}
