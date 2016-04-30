using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Model
{
    public class CrossRoadType
    {
        public bool HasLegA { get; set; }
        public bool HasLegB { get; set; }
        public bool HasLegC { get; set; }
        public bool HasLegD { get; set; }

        public int LegAId { get; set; }
        public int LegBId { get; set; }
        public int LegCId { get; set; }
        public int LegDId { get; set; }

        public int LegAGreenSignalTime { get; set; }
        public int LegAAmberTime { get; set; }

        public int LegBGreenSignalTime { get; set; }
        public int LegBAmberTime { get; set; }

        public int LegCGreenSignalTime { get; set; }
        public int LegCAmberTime { get; set; }

        public int LegDGreenSignalTime { get; set; }
        public int LegDAmberTime { get; set; }

    }
}
