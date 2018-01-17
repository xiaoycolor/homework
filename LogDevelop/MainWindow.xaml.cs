using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogDevelop
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _Model = new MyModel();
            this.DataContext = _Model;
        }
        private MyModel _Model;

        private void lstTexts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void onLoadTargetText_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog aDlg = new OpenFileDialog();
                aDlg.Filter = "文本文件|*.txt;*.htmi;*.log|所有文件|*.*";
                if (aDlg.ShowDialog() != true) return;
                _Model.LoadTargetTextForm(aDlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onLoadTargetText_CanExectue(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void onSaveFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                //if (text1.Text != null)
                {

                    SaveFileDialog saveDlg = new SaveFileDialog();
                    saveDlg.Filter = "文本文件|*.txt;*.htmi;*.log|所有文件|*.*";
                    if (true == saveDlg.ShowDialog())
                    {
                        _Model.SaveTargetTextForm(saveDlg.FileName);
                    }
                }
               // else
                {
                   // MessageBoxResult result = MessageBox.Show("内容为空，无法保存","提示" , MessageBoxButton.OKCancel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void onSaveFile_CanExectue(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void onCloseFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("退出前请先保存文件, yes or not", "退出", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Filter = "文本文件|*.txt;*.htmi;*.log|所有文件|*.*";
                if (true == saveDlg.ShowDialog())
                {
                    _Model.SaveTargetTextForm(saveDlg.FileName);
                }
            }
            else if (result == MessageBoxResult.No)
            {
                this.Close();
            }
        }

        private void onHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("需要什么帮助", "帮助", MessageBoxButton.OKCancel);
        }

        private void onGetMatches_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                _Model.GetMatchs();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onGetMatches_CanExectue(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model != null && _Model.CanStartMatch;
        }

        private void onStartReplace_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                _Model.Replace();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onStartReplace_CanExectue(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model != null && _Model.CanStartReplace;
        }
    }
   

 
}
