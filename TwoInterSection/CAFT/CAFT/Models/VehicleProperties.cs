using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAFT.Models
{
    public class VehicleProperties
    {

        public VehicleProperties()
        {
 
        }

        public VehicleProperties(VehicleType type, int rowoccupancy, int columnoccupancy, int maxcellspeed)
        {
            this.Type = type;
            this.RowOccupancy = rowoccupancy;
            this.ColumnOccupancy = columnoccupancy;
            this.MaxCellSpeed = maxcellspeed;
        }

        public VehicleType Type { get; set; }

        public int RowOccupancy { get; set; }

        public int ColumnOccupancy { get; set; }

        private int _MaxCellSpeed;

        public int MaxCellSpeed
        {
            get { return _MaxCellSpeed; }
            set { _MaxCellSpeed = value; }
        }

        public VehicleStatus Status { get; set; }

        public VehicleDirectionChange DirectionStatus { get; set; }

        public bool IsDirectionChanged { get; set; }

        public int VehicleLaneChanged { get; set; }

    }

    public enum VehicleType { TwoWheel = 0, ThreeWheel = 1, FourWheel = 2, LCV1 = 3, LCV2 = 4, HCV1 = 5, HCV2 = 6 }

    public enum VehicleStatus { InQueue = 0, InProgress = 1, Completed = 2, AtMidSignal = 4}

    public enum VehicleDirectionChange { NoNeedToChange = 0, NeedToChange = 1, Changed = 2 }

    public enum VehicleSide { Bottom = 0, Left = 1, Top = 2, Right = 3 }

}
