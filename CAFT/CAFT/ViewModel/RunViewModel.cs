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

            Road r1 = new Road(50, 10);
            this.TrackList.Add(r1);

            r1.CellHighlighter = new CellHighlighterType[1];
            r1.CellHighlighter[0] = new CellHighlighterType() { ColorCode = 1, RowNumber = 1, ColumnNumber = 1 };

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
