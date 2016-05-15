﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Models
{
    public class NoiseProbabiltyFactorType
    {
        public int NoisyCount { get; set; }
        public int TotalCount { get; set; }
    }

    public class LegType
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
