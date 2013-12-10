using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellMe.ViewModels
{
    public class MainViewModel
    {
        private MainViewModel()
        {
            SayItText = "Say It";
        }
        private static MainViewModel _instance = null;

        public static MainViewModel Instance
        {
            get { return _instance ?? (_instance = new MainViewModel()); }
        }

        public String SayItText { get; set; }
    }
}
