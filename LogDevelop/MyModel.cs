using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LogDevelop
{
    class MyModel : INotifyPropertyChanged
    {
        //正则表达式
        public string Pattern { get { return _Pattern; } set { if (_Pattern == value) return; _Pattern = value; OnPropertyChanged(nameof(Pattern)); } }
        private string _Pattern;
        //读取的文本内容text
        public string TargetText { get { return _TargetText; } set { if (_TargetText == value) return; _TargetText = value; OnPropertyChanged(nameof(TargetText)); } }
        private string _TargetText;

        private static Encoding[] _Encodings = new Encoding[]
        {
            Encoding.Default,
            Encoding.ASCII,
            Encoding.Unicode,
            Encoding.UTF8,
            Encoding.UTF7,
            Encoding.UTF32,
            Encoding.BigEndianUnicode
        };

        public Encoding[] Encodings { get { return _Encodings; } }

        public Encoding  CurrentEncoding { get { return _CurrentEncoding; } set { if (_CurrentEncoding == value) return; _CurrentEncoding = value; OnPropertyChanged(nameof(CurrentEncoding)); } }
        private Encoding  _CurrentEncoding=Encoding.Default;
        
        //读取文件
        public void LoadTargetTextForm(string aFileName)
        {
            TargetText = File.ReadAllText(aFileName, CurrentEncoding);
        }


        
        //保存文件
        public void SaveTargetTextForm(string aFileName)
        {   if(TargetText !=null)
             File.WriteAllText(aFileName, TargetText, CurrentEncoding);
            else
            {

            }
        }

        public void Dofilter()
        {
            
        }
        


        private void OnPropertyChanged(string aPropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(aPropertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void SaveTargetTextForm(string fileName, TextBox textbox1)
        {
            throw new NotImplementedException();
        }
    }
}
