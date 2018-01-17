using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace LogDevelop
{
    class MyModel : INotifyPropertyChanged
    {
        //正则表达式
        public string Pattern { get { return _Pattern; } set { if (_Pattern == value) return; _Pattern = value; OnPropertyChanged(nameof(Pattern)); } }
        private string _Pattern;

        public string ReplaceContent { get { return _ReplaceContent; } set { if (_ReplaceContent == value) return; _ReplaceContent = value; OnPropertyChanged(nameof(ReplaceContent)); } }
        private string _ReplaceContent;
        //读取的文本内容text
        public string TargetText { get { return _TargetText; } set { if (_TargetText == value) return; _TargetText = value; OnPropertyChanged(nameof(TargetText)); } }
        private string _TargetText;
        //匹配文本结果
        public string CollectedText { get { return _CollectedText; } private set { if (_CollectedText == value) return; _CollectedText = value; OnPropertyChanged(nameof(CollectedText)); } }
        private string _CollectedText;

        public MatchCollection Matches { get { return _Matches; } set { if (_Matches == value) return; _Matches = value; OnPropertyChanged(nameof(Matches)); } }
        private MatchCollection _Matches;
        //匹配判断
        public bool CanStartMatch
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Pattern) && !string.IsNullOrWhiteSpace(TargetText);
            }
        }
        //替换判断
        public bool CanStartReplace
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Pattern) && !string.IsNullOrWhiteSpace(ReplaceContent) && !string.IsNullOrWhiteSpace(TargetText);
            }
        }

        public string ReplaceResult { get { return _ReplaceResult; } private set { if (_ReplaceResult == value) return; _ReplaceResult = value; OnPropertyChanged(nameof(ReplaceResult)); } }
        private string _ReplaceResult;
        //替换
        public void Replace()
        {
            Regex aRegex = new Regex(Pattern);
            TargetText = aRegex.Replace(TargetText, ReplaceContent);
        }
        //匹配
        public void GetMatchs()
        {
            Regex aRegex = new Regex(Pattern);
            Matches = aRegex.Matches(TargetText);
            StringBuilder aStringBuilder = new StringBuilder();
            foreach(Match aMatch in Matches)
            {
                aStringBuilder.AppendLine(aMatch.Value);
                
            }
            CollectedText = aStringBuilder.ToString();
        }

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
