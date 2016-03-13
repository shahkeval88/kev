using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.ViewModel
{
    class RunViewModel : ViewModelBase
    {
        public RunViewModel()
        {
            this.Header = "Keval";
        }

        private string _Header;

        public string Header
        {
            get { return _Header; }
            set 
            { 
                _Header = value;
                RaisePropertyChanged("Header");
            }
        }
        
    }
}
