using CAFT.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.ViewModel
{
    class RunViewModel : ViewModelBase
    {
        public RunViewModel()
        {
            this.TrackList = new ObservableCollection<Road>();
            this.TrackList.Add(new Road(500, 10));
            this.TrackList.Add(new Road(500, 10));
        }


        private ObservableCollection<Road> _TrackList;

        public ObservableCollection<Road> TrackList
        {
            get { return _TrackList; }
            set 
            { 
                _TrackList = value;
                RaisePropertyChanged("TrackList");
            }
        }
        
        
    }
}
