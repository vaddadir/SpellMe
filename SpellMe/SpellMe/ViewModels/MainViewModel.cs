using SpellMe.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace SpellMe.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainViewModel()
        {
            SayAWordButtonText = "Say the word";
            UseItInASentenceButtonText = "Use the word in a sentence";
            CloseButtonText = "Close";
            SubmitButtonText = "Test Me";

            CloseCommand = new ClickCommand("CloseButton", OnCloseClick);
            SayAWordCommand = new ClickCommand("SayAWordButton", OnSayAWord);
            SubmitCommand = new ClickCommand("SubmitButton", OnSubmitClick);
        }

        private void OnSubmitClick(object obj)
        {
            Results = "Correct !!!";
        }

        private void OnSayAWord(object obj)
        {
        }

        private void OnCloseClick(object obj)
        {
            App.Current.Shutdown();
        }

        private static MainViewModel _instance = null;

        public static MainViewModel Instance
        {
            get { return _instance ?? (_instance = new MainViewModel()); }
        }

        public String SayAWordButtonText { get; set; }

        public String UseItInASentenceButtonText { get; set; }

        public String CloseButtonText { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand SayAWordCommand { get; set; }

        public String SubmitButtonText { get; set; }

        public ClickCommand SubmitCommand { get; set; }

        private String _results = String.Empty;

        public String Results
        {
            get { return _results; }
            set
            {
                _results = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Results"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}