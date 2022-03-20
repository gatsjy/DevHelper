using System;
using System.Collections.Generic;
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

namespace DevHelper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private DevHelperViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DevHelperViewModel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CONVERTRESULT.Text = ((System.Windows.Controls.TextBox)sender).Text;

            // 1. 변환하기 위한 값 tmp 변수에 넣기
            string tmpResult = ((System.Windows.Controls.TextBox)sender).Text;

            // 2. '\r\n' 파싱하기
            string[] sep = { "\r\n" };
            string[] words = tmpResult.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);

            // 3. 출력
            // 3-1. 출력 전 클리어
            CONVERTRESULT.Text = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("IN (");
            for (int i = 0; i < words.Length; i++)
            {

                if (i == 0 && words.Length == 1)
                {
                    sb.Append(string.Format("'{0}')", words[i]));
                }
                else if (i == 0)
                {
                    sb.Append(string.Format("'{0}'", words[i]));
                }
                else if (i == words.Length - 1)
                {
                    sb.Append(string.Format(",'{0}')", words[i]));
                }
                else
                {
                    sb.Append(string.Format(",'{0}'", words[i]));
                }
                sb.Append("\n");
            }
            CONVERTRESULT.Text = sb.ToString();
            //foreach(string res in words)
            //{
            //    Console.WriteLine(res);
            //}
        }
    }
}
