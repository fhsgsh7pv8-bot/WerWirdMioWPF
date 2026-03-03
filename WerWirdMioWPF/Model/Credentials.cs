using System.ComponentModel;

namespace WerWirdMioWPF
{
    class Credentials : INotifyPropertyChanged
    {

        public String name;
        public String password;


        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public String Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        public Credentials()
        {
            Name = "Name";
            Password = "Password";
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
