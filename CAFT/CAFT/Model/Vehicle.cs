using CAFT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFT.Model
{
    class Vehicle
    {
        #region properties 
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private double _MaxSpeed;

        public double MaxSpeed
        {
            get { return _MaxSpeed; }
            set { _MaxSpeed = value; }
        }

        private double _Width;

        public double Width
        {
            get { return _Width; }
            set { _Width = value; }
        }


        private double _Length;

        public double Length
        {
            get { return _Length; }
            set { _Length = value; }
        }


        private int _WidthInCells;

        public int WidthInCells
        {
            get { return _WidthInCells; }
            set { _WidthInCells = value; }
        }

        private int _LengthInCells;

        public int LengthInCells
        {
            get { return _LengthInCells; }
            set { _LengthInCells = value; }
        }

        private VehicleType _Type;

        public VehicleType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        
        #endregion  

        #region Public Method
        public Vehicle CreateVehicle(VehicleType type)
        {
            return new Vehicle();
        }
        #endregion
    }
}
