using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Models
{
    public class Vehicle
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private VehicleProperties _Properties;
        public VehicleProperties Properties
        {
            get { return _Properties; }
            set { _Properties = value; }
        }

        private DirectionType _Direction;

        public DirectionType Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }

        private CurrentPositionType _CurrentPosition;

        public CurrentPositionType CurrentPosition
        {
            get { return _CurrentPosition; }
            set { _CurrentPosition = value; }
        }

        private int _CurrentCellSpeed;
        public int CurrentCellSpeed
        {
            get { return _CurrentCellSpeed; }
            set { _CurrentCellSpeed = value; }
        }

        private bool _IsAccelerated;

        public bool IsAccelerated
        {
            get { return _IsAccelerated; }
            set { _IsAccelerated = value; }
        }

        private bool _IsOvertaken;
        public bool IsOvertaken
        {
            get { return _IsOvertaken; }
            set { _IsOvertaken = value; }
        }

        private int _Vehicle_Headway;

        public int Vehicle_Headway
        {
            get { return _Vehicle_Headway; }
            set { _Vehicle_Headway = value; }
        }

        private int _Vehicle_Headway_Intersection;
        public int Vehicle_Headway_Intersection
        {
            get { return _Vehicle_Headway_Intersection; }
            set { _Vehicle_Headway_Intersection = value; }
        }

        private int _StartTime;

        public int StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        private int _EndTime;

        public int EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public int SaturationTime { get; set; }

        public int StartTimeIntersection { get; set; }

        public int EndTimeIntersection { get; set; }

        public int IsStoppedForSignal { get; set; }

        public int IsStoppedForSignalFirst { get; set; }

        public bool IsNoisy { get; set; }

    }
}
