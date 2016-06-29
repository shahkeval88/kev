using CAFT.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace CAFT.ViewModel
{
    class RunViewModel : ViewModelBase
    {

        Road r1 = new Road(50, 10);

        public RunViewModel()
        {
            this.ChangeWidthCommand = new RelayCommand(OnWidthChanged);

            this.TrackList = new ObservableCollection<Road>();
            this.TrackList.Add(r1);

            //r1.UpdateCellHighlighter(CellHighlighter);
            
            //r1.CellHighlighter = new CellHighlighterType[1];
            //r1.CellHighlighter[0] = new CellHighlighterType() { ColorCode = 1, RowNumber = 1, ColumnNumber = 1 };

        }

        private void OnWidthChanged()
        {
            var abc = new ObservableCollection<CellHighLighterType>();
            abc.Add(new CellHighLighterType()
            {
                ColorCode = 1,
                ColumnNumber = 1,
                RowNumber = 1
            });

            this.TrackList[0].GridHighLighter = abc;
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

        public RelayCommand ChangeWidthCommand { get; private set; }
        
    }
}
