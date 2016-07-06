using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Engine.Model
{
    class RoadType
    {
        public int Id { get; set; }

        private int[,] _CellGrid;
        public int[,] CellGrid
        { 
            get { return _CellGrid; }
            private set
            {
                _CellGrid = value;
                //We may required to delegate a function here which
                //will call the View to update grid based upon flag
                //we have set
            }
        }

        public int ActualStartLine { get; set; }

        public int ActualEndLine { get; set; }

        public int CrossRoadId { get; set; }

        public int WidthInCell { get; set; }

        public int LengthInCell { get; set; }

        public int HeadwayLine { get; set; }

        public List<BumpType> Bumps { get; set; }

        public List<SpeedReductionZoneType> SpeedReductionZones { get; set; }




    }
}
