using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DevHelper
{
    public class DevHelperViewModel : INotifyPropertyChanged
    {
        private string inputtext = "";
        private string inputtext2 = "";
        public string INPUTTEXT
        {
            get => inputtext;
            set
            {
                inputtext = value;
                NotifyPropertyChanged();
            }
        }
        public string INPUTTEXT2
        {
            get => inputtext2;
            set
            {
                inputtext2 = value;
                NotifyPropertyChanged2();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string inputtext = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(inputtext));
        }

        public event PropertyChangedEventHandler PropertyChanged2;

        public void NotifyPropertyChanged2([CallerMemberName] string inputtext2 = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(inputtext2));
        }

    }
}
