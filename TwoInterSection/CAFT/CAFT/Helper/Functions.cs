using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAFT.Models;

namespace CAFT.Helper
{
    public class Functions
    {
        public GlobalVariables globalVariables;

        public Functions(GlobalVariables _globalVariables)
        {
            globalVariables = _globalVariables;
        }

        /// <summary>
        /// Generates Random number
        /// </summary>
        /// <param name="Min"></param>
        /// <param name="Max"></param>
        /// <returns></returns>
        public int RandomNumberGenerator(int Min, int Max)
        {
            int RandomNumber = 0;

            RandomNumber = globalVariables.RndmNo.Next(Min, Max);

            return RandomNumber;
        }

        public int[] GetUniqueRandoms(int min, int max)
        {
            int[] randomNos = new int[max - 1];
            Random rnd = new Random();

            int cnt = 0;
        start:
            if (cnt < max - 1)
            {
                int aNum = rnd.Next(min, max);

                if (!randomNos.Contains(aNum))
                {
                    randomNos[cnt] = aNum;
                    cnt++;
                }
                goto start;
            }
            return randomNos;
        }

        public int[] VehicleAssignmentSession(int sessionno, int totalvehicles)
        {
            int[] randomNos = new int[sessionno];
            Random rnd = new Random();

            int medianlowerlimit = Convert.ToInt16(Math.Floor(((double)totalvehicles / sessionno)));
            int medianupperlimit = Convert.ToInt16(Math.Ceiling(((double)totalvehicles / sessionno)));
            int totalvehiclesleft = totalvehicles;
            int aNum = 0;

            for (int cnt = 0; cnt < sessionno; cnt++)
			{
                if (totalvehiclesleft > medianlowerlimit && cnt != (sessionno - 1))
                {
                    aNum = rnd.Next(medianlowerlimit, medianupperlimit + 1);
                    randomNos[cnt] = Convert.ToInt16(aNum);
                    totalvehiclesleft = totalvehiclesleft - aNum;
                }
                else
                {
                    randomNos[cnt] = totalvehiclesleft;
                }
			}
            
            return randomNos;
        }

        public int KmToCell(int p)
        {
            var curCellSize = CaftSettings.Default.CellSize_Height;

            return (int)Math.Round((((Convert.ToDecimal(p) * 1000) / 3600) / Convert.ToDecimal(curCellSize)) / 1, 0);
        }

        public int SpeedBumpB60(VehicleType vhType)
        {
            switch (vhType)
            {
                case VehicleType.TwoWheel:
                    return KmToCell(globalVariables.RndmNo.Next(45, 57));
                case VehicleType.ThreeWheel:
                    return KmToCell(globalVariables.RndmNo.Next(38, 42));
                case VehicleType.FourWheel:
                    return KmToCell(globalVariables.RndmNo.Next(55, 65));
                case VehicleType.LCV1:
                case VehicleType.LCV2:
                    return KmToCell(globalVariables.RndmNo.Next(45, 52));
                case VehicleType.HCV1:
                case VehicleType.HCV2:
                    return KmToCell(globalVariables.RndmNo.Next(36, 39));
                default:
                    return KmToCell(globalVariables.RndmNo.Next(45, 57));
            }
        }

        public int SpeedBumpB40(VehicleType vhType)
        {
            switch (vhType)
            {
                case VehicleType.TwoWheel:
                    return KmToCell(globalVariables.RndmNo.Next(36, 40));
                case VehicleType.ThreeWheel:
                    return KmToCell(globalVariables.RndmNo.Next(34, 37));
                case VehicleType.FourWheel:
                    return KmToCell(globalVariables.RndmNo.Next(45, 55));
                case VehicleType.LCV1:
                case VehicleType.LCV2:
                    return KmToCell(globalVariables.RndmNo.Next(35, 37));
                case VehicleType.HCV1:
                case VehicleType.HCV2:
                    return KmToCell(globalVariables.RndmNo.Next(31, 55));
                default:
                    return KmToCell(globalVariables.RndmNo.Next(36, 40));
            }
        }

        public int SpeedBumpB20(VehicleType vhType)
        {
            switch (vhType)
            {
                case VehicleType.TwoWheel:
                    return KmToCell(globalVariables.RndmNo.Next(25, 28));
                case VehicleType.ThreeWheel:
                    return KmToCell(globalVariables.RndmNo.Next(24, 29));
                case VehicleType.FourWheel:
                    return KmToCell(globalVariables.RndmNo.Next(25, 30));
                case VehicleType.LCV1:
                case VehicleType.LCV2:
                    return KmToCell(globalVariables.RndmNo.Next(28, 30));
                case VehicleType.HCV1:
                case VehicleType.HCV2:
                    return KmToCell(globalVariables.RndmNo.Next(21, 25));
                default:
                    return KmToCell(globalVariables.RndmNo.Next(25, 28));
            }
        }

        public int SpeedOnBump(VehicleType vhType)
        {
            switch (vhType)
            {
                case VehicleType.TwoWheel:
                    return KmToCell(globalVariables.RndmNo.Next(15, 16));
                case VehicleType.ThreeWheel:
                    return KmToCell(globalVariables.RndmNo.Next(14, 15));
                case VehicleType.FourWheel:
                    return KmToCell(globalVariables.RndmNo.Next(15, 17));
                case VehicleType.LCV1:
                case VehicleType.LCV2:
                    return KmToCell(globalVariables.RndmNo.Next(13, 14));
                case VehicleType.HCV1:
                case VehicleType.HCV2:
                    return KmToCell(globalVariables.RndmNo.Next(12, 14));
                default:
                    return KmToCell(globalVariables.RndmNo.Next(15, 16));
            }
        }

        public int SpeedBumpA20(VehicleType vhType)
        {
            switch (vhType)
            {
                case VehicleType.TwoWheel:
                    return KmToCell(globalVariables.RndmNo.Next(24, 26));
                case VehicleType.ThreeWheel:
                    return KmToCell(globalVariables.RndmNo.Next(20, 21));
                case VehicleType.FourWheel:
                    return KmToCell(globalVariables.RndmNo.Next(26, 32));
                case VehicleType.LCV1:
                case VehicleType.LCV2:
                    return KmToCell(globalVariables.RndmNo.Next(18, 25));
                case VehicleType.HCV1:
                case VehicleType.HCV2:
                    return KmToCell(globalVariables.RndmNo.Next(16, 20));
                default:
                    return KmToCell(globalVariables.RndmNo.Next(24, 26));
            }
        }

        public int SpeedBumpA40(VehicleType vhType)
        {
            switch (vhType)
            {
                case VehicleType.TwoWheel:
                    return KmToCell(globalVariables.RndmNo.Next(33, 35));
                case VehicleType.ThreeWheel:
                    return KmToCell(globalVariables.RndmNo.Next(26, 28));
                case VehicleType.FourWheel:
                    return KmToCell(globalVariables.RndmNo.Next(38, 44));
                case VehicleType.LCV1:
                case VehicleType.LCV2:
                    return KmToCell(globalVariables.RndmNo.Next(29, 35));
                case VehicleType.HCV1:
                case VehicleType.HCV2:
                    return KmToCell(globalVariables.RndmNo.Next(20, 23));
                default:
                    return KmToCell(globalVariables.RndmNo.Next(33, 35));
            }
        }

        public int SpeedBumpA60(VehicleType vhType)
        {
            switch (vhType)
            {
                case VehicleType.TwoWheel:
                    return KmToCell(globalVariables.RndmNo.Next(38, 41));
                case VehicleType.ThreeWheel:
                    return KmToCell(globalVariables.RndmNo.Next(28, 31));
                case VehicleType.FourWheel:
                    return KmToCell(globalVariables.RndmNo.Next(45, 54));
                case VehicleType.LCV1:
                case VehicleType.LCV2:
                    return KmToCell(globalVariables.RndmNo.Next(40, 45));
                case VehicleType.HCV1:
                case VehicleType.HCV2:
                    return KmToCell(globalVariables.RndmNo.Next(23, 25));
                default:
                    return KmToCell(globalVariables.RndmNo.Next(38, 41));
            }
        }
    }
}
