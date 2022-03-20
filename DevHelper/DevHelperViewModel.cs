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
        public string INPUTTEXT
        {
            get => inputtext;
            set
            {
                inputtext = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string inputtext = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(inputtext));
        }


    }
}
