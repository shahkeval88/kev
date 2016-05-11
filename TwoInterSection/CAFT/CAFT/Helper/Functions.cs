using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
