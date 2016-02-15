using System.ComponentModel;

namespace TestingUI.Objects
{
    public class Bookmark : INotifyPropertyChanged
    {
        private string _Memo;

        private string _SystemName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Memo
        {
            get { return _Memo; }
            set
            {
                if (_Memo == value) return;
                _Memo = value;
                OnPropertyChanged("Memo");
            }
        }

        public string SystemName
        {
            get { return _SystemName; }
            set
            {
                if (_SystemName == value) return;
                _SystemName = value;
                OnPropertyChanged("SystemName");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}