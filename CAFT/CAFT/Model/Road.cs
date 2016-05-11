using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Model
{
    public class Road
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        

        private int _Length;

        public int Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        private double _Width;

        public double Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private bool _HasCrossRoads;

        public bool HasCrossRoads
        {
            get { return _HasCrossRoads; }
            set { _HasCrossRoads = value; }
        }

        private bool _HasBump;

        public bool HasBump
        {
            get { return _HasBump; }
            set { _HasBump = value; }
        }

        public BumpType Bump { get; set; }

        public double SpeedLimitKm { get; set; }

        public double SpeedLimitStart { get; set; }

        public double SpeedLimitEnd { get; set; }
        
        public CrossRoadType CrossRoadStart { get; set; }
        
        public CrossRoadType CrossRoadEnd { get; set; }

        public CellHighlighterType[] CellHighlighter { get; set; }
        

        public Road(int length, int width)
        {
            this.Length = length;
            this.Width = width;
        }
        
    }
}
