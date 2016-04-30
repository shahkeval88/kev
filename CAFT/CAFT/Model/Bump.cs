using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Model
{
    public class BumpType
    {
        private double _Length;

        public double Length
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

        private int _PositionOnRoad;

        public int PositionOnRoad
        {
            get { return _PositionOnRoad; }
            set { _PositionOnRoad = value; }
        }
        
    }
}
