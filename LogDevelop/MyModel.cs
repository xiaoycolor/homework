using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDevelop
{
    class MyModel : INotifyPropertyChanged
    {
        

        public void Load()
        {

        }
        public void Dofilter()
        {

        }

        public string[] SourceText {
            get {   return _SourceText;   }
            set {
                if (_SourceText == value)    return;
                 _SourceText = value;
                 OnPropertyChanged(nameof(SourceText)); } }
        private string[] _SourceText;

        private void OnPropertyChanged(string aPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(aPropertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
