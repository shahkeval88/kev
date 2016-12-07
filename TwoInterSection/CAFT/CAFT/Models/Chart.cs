using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Models
{
    class Chart
    {
        
    }

    public class SpacePerTime
    {
        public double CellDistance { get; set; }
        public int time { get; set; }
        public double speed { get; set; }
    }

    public class DensityPerSpeed
    {
        public int Density { get; set; }
        public double Speed { get; set; }
        public int TickTime { get; set; }
    }
}
