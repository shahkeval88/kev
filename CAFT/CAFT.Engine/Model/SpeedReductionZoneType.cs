using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAFT.Engine.Model
{
    class SpeedReductionZoneType
    {
        public int Id { get; set; }

        public int CrossId { get; set; }

        public int StartLine { get; set; }

        public int EndLine { get; set; }

        public int MaximumSpeedInCell { get; set; }

        public int MinimumSpeedInCell { get; set; }

        private double _MaximumSpeedInKm;

        private double MaximumSpeedInKm
        {
            get { return _MaximumSpeedInKm; }
            set 
            { 
                _MaximumSpeedInKm = value;
                if (value > 0.0)
                {
                    //Convert Km to Cell Function General Code...
                }
            }
        }

        private double _MinimumSpeedInKm;

        private double MinimumSpeedInKm
        {
            get { return _MinimumSpeedInKm; }
            set
            {
                _MinimumSpeedInKm = value;
                if (value > 0.0)
                {
                    //Convert Km to Cell Function General Code...
                }
            }
        }

        public SpeedReductionZoneType(int id, int crossid, int startline, int endline, double maximumspeed, double minimumspeed)
        {
            this.Id = id;
            this.CrossId = crossid;
            this.StartLine = startline;
            this.EndLine = endline;
            this.MaximumSpeedInKm = maximumspeed;
            this.MinimumSpeedInKm = minimumspeed;
        }
        
    }
}
