using SpellMe.Commands;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpellMe.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private String _results = String.Empty;
        private readonly SpeechSynthesizer _speechEngine = null;
        private const String WordFilesDirectory = @"D:\MyWork\Words";
        private const String FileNames = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly WordList _words = new WordList();
        private String _currentWord = String.Empty;
        private string _word;

        #region Constructor

        private MainViewModel()
        {
            SayAWordButtonText = "Next Word";
            UseItInASentenceButtonText = "Use the word in a sentence";
            CloseButtonText = "Close";
            SubmitButtonText = "Test Me";

            CloseCommand = new ClickCommand("CloseButton", OnCloseClick);
            SayAWordCommand = new ClickCommand("SayAWordButton", OnSayAWord);
            SubmitCommand = new ClickCommand("SubmitButton", OnSubmitClick);
            RepeatWordCommand = new ClickCommand("RepeatWord", OnRepeatWord);
            UseItInASentence = new ClickCommand("UseItInASentence", OnUseItInASentence);
            _speechEngine = new SpeechSynthesizer();
            Rate = -4;
            LoadWords();
        }

        private void OnUseItInASentence(object obj)
        {
            new DictionaryService().GetResponse(_currentWord);
        }

        private void LoadWords()
        {
            foreach (var fileName in FileNames)
            {
                var fullPath = Path.ChangeExtension(Path.Combine(WordFilesDirectory, fileName.ToString()), "txt");
                _words.Load(fullPath);
            }
        }

        #endregion Constructor

        private void OnSubmitClick(object obj)
        {
            var correct = _currentWord.Trim().ToLower() == Word.Trim().ToLower();
            if (correct)
            {
                Results = "Correct !!!";
                ReadWord(Rate, Results);
            }
            else
            {
                Results = String.Format("Sorry, The correct spelling is {0}", _currentWord);
                ReadWord(Rate, "Sorry, The correct spelling is");
                for (var i = 0; i < _currentWord.Length; i++)
                {
                    ReadWord(2, _currentWord[i].ToString(CultureInfo.InvariantCulture));
                };
                _speechEngine.Rate = Rate;
            }
        }

        private void OnSayAWord(object obj)
        {
            Results = String.Empty;
            Word = String.Empty;
            _currentWord = _words.GetNextWord();
            ReadWord(Rate, _currentWord);
        }

        private void OnRepeatWord(object obj)
        {
            ReadWord(Rate, _currentWord);
        }

        private void ReadWord(int rate, String word)
        {
            if (String.IsNullOrEmpty(_currentWord)) return;
            _speechEngine.Rate = rate;
            new Task(() => _speechEngine.Speak(word)).Start();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            dynamic anon = e.Argument;
            anon.Engine.Speak(anon.word);
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

        public ClickCommand RepeatWordCommand { get; set; }

        public String Word
        {
            get { return _word; }
            set
            {
                _word = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Word"));
            }
        }

        public int Rate { get; set; }

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

        public ClickCommand UseItInASentence { get; set; }
    }
}