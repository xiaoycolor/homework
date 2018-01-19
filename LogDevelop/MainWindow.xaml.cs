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
using System.Xml;

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

        
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            if (textbox2.Text.Trim() != "")
            {
                remind.Remind(textbox2.Text.Trim());
                InitTextBoxRemind();
            }
        }
        */
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
             MessageBoxResult result = MessageBox.Show(_Model.returnmessage(), "帮助", MessageBoxButton.OKCancel);

        }

        private void onGetMatches_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                _Model.GetMatchs();
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onStartReplace_CanExectue(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model != null && _Model.CanStartReplace;
        }
        int x = 0;
        private void onView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (x != 0)
            {
                ListBox1.Items.Clear();
                x--;
            }
            if (x == 0)
            {
                x++;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("XMLFile1.xml");
                XmlNode xn = xmlDoc.SelectSingleNode("Contacts");

                XmlNodeList xnl = xn.ChildNodes;
                string[] strText = new string[3];
                foreach (XmlNode xnf in xnl)
                {
                    XmlElement xe = (XmlElement)xnf;
                    // str = xe.GetAttribute("Title");//显示属性值  

                    XmlNodeList xnf1 = xe.ChildNodes;
                    int i = 0;
                    foreach (XmlNode xn2 in xnf1)
                    {
                        strText[i] = xn2.InnerText;//显示子节点点文本  
                        i++;
                    }
                    // ListBox1.Items.Add(strText[0].ToString());
                    ListBox1.Items.Add(strText[1].ToString());
                }
            }
           


            // ListBox1.Items.Clear();
        }

        //将选中的listbox中内容放入正则表达式的Textbox中
        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBox1.SelectedItem != null)
            {
                textbox2.Text = this.ListBox1.SelectedItem.ToString();
            }
        }
        //添加正则表达式
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(textbox2.Text);
            if (textbox2.Text.Trim() != String.Empty)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("XMLFile1.xml");
                XmlNode xn = xmlDoc.SelectSingleNode("Contacts");
                XmlElement xe1 = xmlDoc.CreateElement("contact");//创建一个子节点
                                                                 // xe1.SetAttribute("Title", "正则表达式");//添加子节点熟悉，不是内容
                XmlElement xesub1 = xmlDoc.CreateElement("Title");
                xesub1.InnerText = "正则表达式";//设置文本节点             
                xe1.AppendChild(xesub1);//添加到<XMLFile1>节点中
                XmlElement xesub2 = xmlDoc.CreateElement("coment");
                xesub2.InnerText = textbox2.Text;//设置文本节点             
                xe1.AppendChild(xesub2);//添加到<XMLFile1>节点中

                xn.AppendChild(xe1);
                xmlDoc.Save("XMLFile1.xml");
                MessageBox.Show("添加成功");
            }
            else
            {
                MessageBox.Show("没有需要添加的正则表达式");
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            
            if (textbox2.Text.Trim() != String.Empty)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("XMLFile1.xml");
                XmlNodeList xnl = xmlDoc.SelectSingleNode("Contacts").ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    
                    XmlElement xe = (XmlElement)xn;
                    
                    if (xe.GetElementsByTagName("coment")[0].InnerText == textbox2.Text)
                    {
                        MessageBox.Show(textbox2.Text);
                        xe.RemoveAll();//删除该节点的全部内容 
                        MessageBox.Show("删除成功");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择需要删除哪一个正则表达式");
            }
        }


    }
}
