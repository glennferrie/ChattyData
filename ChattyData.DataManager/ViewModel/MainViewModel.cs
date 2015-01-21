using GalaSoft.MvvmLight;

namespace ChattyData.Data.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                this.Title = "ChattyData - Design mode";
            }
            else
            {
                // Code runs "for real"
                this.Title = "Chatty Data";
            }
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                RaisePropertyChanged("Title");
            }
        }
    }
}