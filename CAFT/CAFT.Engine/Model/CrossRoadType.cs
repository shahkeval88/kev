using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Engine.Model
{
    class CrossRoadType
    {
        public int Id { get; set; }

        public int LegAId { get; set; }

        public int LegBId { get; set; }

        public int LegCId { get; set; }

        public int LegDId { get; set; }
    }
}
