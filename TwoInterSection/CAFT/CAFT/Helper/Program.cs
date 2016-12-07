using CAFT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Drawing;
using System.Windows;

namespace CAFT.Helper
{
    class Program
    {
        /// <summary>
        /// At Every Time Interval, this function will be called first
        /// At a time maximum 4 vehicles may be generated at the same time as of now - Rutul
        /// </summary>
        /// 
        #region Variables
        Grid MainGrid;

        public GlobalVariables globalVariables;
        public Functions functions;

        public Dictionary<int, KeyValuePair<int, int>> _spacePerTime = new Dictionary<int, KeyValuePair<int, int>>();

        public double AvgSpeed;
        public int NoOfVehiclesPerKm;

        public int cntTypeOfVehicles = 0, cntTypeOfVehiclesTop = 0, cntTypeOfVehiclesRight = 0, cntTypeOfVehiclesLeft = 0;
        public int cntVehicleDirection = 0;
        public int NoOfVehicles = 1000;
        public int cntNoiseProbFactorVehicles = 0;

        public int SignalTick = 1;
        public bool IsMidBlockRed = true;

        public int ActualGridRowCount;

        int[] requiredToGenerateTop, requiredToGenerateRight;

        public int TwoWheelRatio, ThreeWheelRatio, FourWheenRatio, LCVRatio, HCVRatio;
        public int LeftRatio, StraightRatio, RightRatio;
        #endregion

        public Program()
        {

        }

        public Program(int noOfVehicles, int _actualGridRowCount, GlobalVariables _globalVariables, Functions _functions)
        {
            NoOfVehicles = noOfVehicles;
            ActualGridRowCount = _actualGridRowCount;
            globalVariables = _globalVariables;
            functions = _functions;
            requiredToGenerateTop = functions.GetUniqueRandoms(1, 101);
            requiredToGenerateRight = functions.GetUniqueRandoms(1, 101);


        }

        public void Flow(Grid maingrid)
        {
            this.MainGrid = maingrid;

            if (!globalVariables.StopExecution)
            {
                if (CaftSettings.Default.lefttraffic)
                {
                    //GenerateVehiclesLeft();
                }

                if (CaftSettings.Default.righttraffic)
                {
                    //GenerateVehiclesRight();
                }

                if (CaftSettings.Default.bottomtraffic)
                {
                    GenerateVehicles();
                }

                if (CaftSettings.Default.toptraffic)
                {
                    //GenerateVehiclesTop();
                }
            }

            if (CaftSettings.Default.lefttraffic)
            {
                //ProcessQueue_Left();
            }

            if (CaftSettings.Default.righttraffic)
            {
                //ProcessQueue_Right();
            }

            if (CaftSettings.Default.bottomtraffic)
            {
                ProcessQueue();
            }

            if (CaftSettings.Default.toptraffic)
            {
                //ProcessQueue_Top();
            }
        }


        #region Vehicle Generation, Assigning Lane, directions

        /// <summary>
        /// To Generate all types of vehicle list
        //  Create object of Vehicle and assign default properties.
        //  Type - as per 60-15-15-10 Random probability
        //  Update Global Counter as per vehicleCounter
        /// </summary>
        /// <param name="vehicleCounter">Auto Incremented counter to be passed</param>
        private void GenerateVehicles()
        {

            #region Vehicle Generation logic as per frequency distribution
            int vehicleCounter = 0;
            int totalvehicles = globalVariables.VehicleList.Count();
            //if (totalvehicles >= 1) 
            //    return new List<Vehicle>();
            

            if ((bool)CaftSettings.Default.vhRangeInclude)
            {
                #region old Vehicle Generation Logic - per 60
                //int VhSession = globalVariables.VhSession;
                //if (VhSession < globalVariables.SecondsForHeadwayDist)
                //{
                //    if (globalVariables.VhAssignment.Count() == globalVariables.SecondsForHeadwayDist)
                //    {
                //        if (globalVariables.TickCount == 0 || globalVariables.TickCount % globalVariables.SecondsForHeadwayDist > 0)
                //        {
                //            int vhLeft = globalVariables.VhAssignment[VhSession] - globalVariables.VhGenerated[VhSession];
                //            int rndNo = functions.RandomNumberGenerator(0, 2);
                //            int maxval = 0;

                //            if (vhLeft > 5)
                //            {
                //                maxval = 5;
                //            }
                //            else
                //            {
                //                maxval = functions.RandomNumberGenerator(0, vhLeft + 1);
                //            }

                //            if (rndNo == 0)
                //            {
                //                vehicleCounter = functions.RandomNumberGenerator(0, maxval + 1);
                //                globalVariables.VhGenerated[VhSession] += vehicleCounter;
                //            }
                //        }
                //        else
                //        {
                //            if (globalVariables.VhGenerated[VhSession] < globalVariables.VhAssignment[VhSession])
                //            {
                //                vehicleCounter = globalVariables.VhAssignment[VhSession] - globalVariables.VhGenerated[VhSession];
                //                globalVariables.VhGenerated[VhSession] += vehicleCounter;
                //            }

                //            globalVariables.VhSession += 1;
                //        }
                //    }
                //    else
                //    {
                //        if (globalVariables.TickCount == 0 || globalVariables.TickCount % (globalVariables.SecondsForHeadwayDist * globalVariables.VhSessiongap) > 0)
                //        {
                //            int vhLeft = globalVariables.VhAssignment[VhSession] - globalVariables.VhGenerated[VhSession];
                //            int rndNo = functions.RandomNumberGenerator(0, 2);
                //            int maxval = 0;

                //            if (vhLeft > 5)
                //            {
                //                maxval = 5;
                //            }
                //            else
                //            {
                //                maxval = functions.RandomNumberGenerator(0, vhLeft + 1);
                //            }

                //            if (rndNo == 0)
                //            {
                //                vehicleCounter = functions.RandomNumberGenerator(0, maxval + 1);
                //                globalVariables.VhGenerated[VhSession] += vehicleCounter;
                //            }
                //        }
                //        else
                //        {
                //            if (globalVariables.VhGenerated[VhSession] < globalVariables.VhAssignment[VhSession])
                //            {
                //                vehicleCounter = globalVariables.VhAssignment[VhSession] - globalVariables.VhGenerated[VhSession];
                //                globalVariables.VhGenerated[VhSession] += vehicleCounter;
                //            }

                //            globalVariables.VhSession += 1;
                //        }

                //    }
                //}
#endregion

                /*-- Vehicle Generation Logic  --*/


                if (globalVariables.TickCount == 0 || globalVariables.TickCount % globalVariables.HeadwayDist == 0)
                {

                    globalVariables.TotalVehicleInOneHeadwayDist = new int[globalVariables.HeadwayDist];

                    if (globalVariables.HeadwayDist > globalVariables.perHeadWayDistVehicle)
                    {
                        int singleVehicleDistributionSecond = (int)Math.Floor(globalVariables.HeadwayDist / globalVariables.perHeadWayDistVehicle);
                        for (int i = 0; i < globalVariables.perHeadWayDistVehicle; i++)
                        {
                            var rnd = new Random().Next(0, singleVehicleDistributionSecond + 1);
                           globalVariables.TotalVehicleInOneHeadwayDist[(i * singleVehicleDistributionSecond) + rnd] = 1;
                        }
                    }
                    else
                    {
                        if (globalVariables.perHeadWayDistVehicle - globalVariables.HeadwayDist < 5)
                        {
                            int zeroVehiclePlaceCount = (int)((globalVariables.HeadwayDist * 30) / 100);
                            for (int i = 0; i < zeroVehiclePlaceCount; i++)
                            {
                                globalVariables.TotalVehicleInOneHeadwayDist[new Random().Next(0, globalVariables.HeadwayDist)] = 999;
                            }
                        }
                        int temp = (int)globalVariables.perHeadWayDistVehicle;
                        for (int i = 0; i < globalVariables.HeadwayDist; i++)
                        {
                            if (globalVariables.TotalVehicleInOneHeadwayDist[i] != 999)
                            {
                                globalVariables.TotalVehicleInOneHeadwayDist[i] = 1;
                                temp--;
                            }
                        }
                        if (temp > 0)
                        {
                            int maxVhPerSecond = 2;
                            while (temp > 0)
                            {
                                if (temp < 5 && globalVariables.perHeadWayDistVehicle > globalVariables.HeadwayDist * 2)
                                {
                                    //maxVhPerSecond = 2 is not possible in this case... 
                                    //must be road is wide enough to accomodate 3 vh.
                                    maxVhPerSecond = 3;
                                }

                                var rnd = new Random().Next(0, globalVariables.HeadwayDist);
                                if (globalVariables.TotalVehicleInOneHeadwayDist[rnd] != 999
                                    && globalVariables.TotalVehicleInOneHeadwayDist[rnd] != maxVhPerSecond)
                                {
                                    globalVariables.TotalVehicleInOneHeadwayDist[rnd] = globalVariables.TotalVehicleInOneHeadwayDist[rnd] + 1;
                                    temp--;
                                }
                            }
                        }

                        for (int i = 0; i < globalVariables.TotalVehicleInOneHeadwayDist.Length; i++)
                        {
                            if (globalVariables.TotalVehicleInOneHeadwayDist[i] == 999)
                            {
                                globalVariables.TotalVehicleInOneHeadwayDist[i] = 0;
                            }
                        }

                        //}
                    }
                }

                /*-- Vehicle Generation Logic  End --*/
                vehicleCounter = globalVariables.TotalVehicleInOneHeadwayDist[globalVariables.TickCount % globalVariables.HeadwayDist];
            }
            else if (totalvehicles > 1)
            {
                int headywayvehicledistribution = 0;

                for (int i = 1; i < globalVariables.TickCount; i++)
                {
                    var count = globalVariables.VehicleList.Where(p => p.Vehicle_Headway == i).Count();
                    if (count > 0)
                    {
                        headywayvehicledistribution = headywayvehicledistribution + count;
                    }
                }

                float frequencydistribution1 = (headywayvehicledistribution * 100) / totalvehicles;


                if (frequencydistribution1 < 65.60)
                {
                    int randno = functions.RandomNumberGenerator(0, 2);
                    if (randno == 0)
                    {
                        vehicleCounter = functions.RandomNumberGenerator(2, 5);
                    }
                }
                else
                {
                    vehicleCounter = functions.RandomNumberGenerator(0, 2);
                }
            }
            else
            {
                vehicleCounter = functions.RandomNumberGenerator(1, 5);
            }
            #endregion




            //need to keep the track of the lane we have assigned to a vehicle ..


            /* lane which is assigned to last vehicle can not assigned to other 
            vehicle in next seconds as part of the vehicle is still on that lane */

            for (int i = 0; i < vehicleCounter; i++)
            {
                Vehicle objVehicle = new Vehicle();
                objVehicle.CurrentPosition = new CurrentPositionType();
                objVehicle.Properties = new VehicleProperties();

                #region Setting Default Properties
                objVehicle.Id = globalVariables.VehicleList.Count() + 1;
                //objVehicle.CurrentCellSpeed = 1;
                objVehicle.CurrentPosition.Row = globalVariables.TotalGridRowCount;


                #endregion

                #region Determining Vehicle Type


                if (cntTypeOfVehicles == NoOfVehicles - 1)
                {
                    cntTypeOfVehicles = 0;
                }
                int VehicleType = ((globalVariables.rdTypeOfVehicles[cntTypeOfVehicles]) * 100) / NoOfVehicles; // functions.RandomNumberGenerator(1, 101);
                cntTypeOfVehicles++;

                if (cntVehicleDirection > 99)
                {
                    cntVehicleDirection = 0;
                }
                int VehicleDirection = globalVariables.rdVehicleDirectionRatio[cntVehicleDirection];
                cntVehicleDirection++;



                if (VehicleType > 0 && VehicleType <= TwoWheelRatio && CaftSettings.Default.TwoVhInclude)
                {
                    // Two Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.TwoWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.TwoVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(25, 31));
                }
                else if (VehicleType > TwoWheelRatio && VehicleType <= ThreeWheelRatio && CaftSettings.Default.ThreeVhInclude)
                {
                    // Three Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.ThreeWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.ThreeVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.ThreeVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.ThreeVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(20, 26));
                }
                else if (VehicleType > ThreeWheelRatio && VehicleType <= FourWheenRatio && CaftSettings.Default.FourVhInclude)
                {
                    // Four Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.FourWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.FourVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.FourVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.FourVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(30, 35));
                }
                else if (VehicleType > FourWheenRatio && VehicleType <= LCVRatio && (CaftSettings.Default.LCV1VhInclude || CaftSettings.Default.LCV2VhInclude))
                {
                    int randomLCVHCV = functions.RandomNumberGenerator(0, 2);
                    switch (randomLCVHCV)
                    {
                        case 1:
                            // LCV1
                            objVehicle.Properties.Type = Models.VehicleType.LCV1;
                            objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Row);
                            objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Column);
                            objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV1Vh_MaxAcceleration);
                            objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(18, 20));
                            break;
                        case 2:
                            // LCV2
                            objVehicle.Properties.Type = Models.VehicleType.LCV2;
                            objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV2VhSize_Row);
                            objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV2VhSize_Column);
                            objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV2Vh_MaxAcceleration);
                            objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                            break;
                        default:
                            // LCV1
                            objVehicle.Properties.Type = Models.VehicleType.LCV1;
                            objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Row);
                            objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Column);
                            objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV1Vh_MaxAcceleration);
                            objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(18, 20));
                            break;
                    }
                }
                else if (VehicleType > LCVRatio && VehicleType <= HCVRatio && (CaftSettings.Default.HCV1VhInclude || CaftSettings.Default.HCV2VhInclude))
                {
                    int randomLCVHCV = functions.RandomNumberGenerator(0, 2);
                    switch (randomLCVHCV)
                    {
                        case 1:
                            // HCV1
                            objVehicle.Properties.Type = Models.VehicleType.HCV1;
                            objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Row);
                            objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Column);
                            objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV1Vh_MaxAcceleration);
                            objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                            break;
                        case 2:
                            // HCV2
                            objVehicle.Properties.Type = Models.VehicleType.HCV2;
                            objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV2VhSize_Row);
                            objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV2VhSize_Column);
                            objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV2Vh_MaxAcceleration);
                            objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                            break;
                        default:
                            // HCV1
                            objVehicle.Properties.Type = Models.VehicleType.HCV1;
                            objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Row);
                            objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Column);
                            objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV1Vh_MaxAcceleration);
                            objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                            break;
                    }
                }
                else
                {
                    // For default Vehicles   
                    // Two Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.TwoWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.TwoVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(25, 31));
                }
                #endregion

                objVehicle.IsAccelerated = false;
                objVehicle.IsOvertaken = false;
                objVehicle.Vehicle_Headway = 0;
                AssignVehicleLane(objVehicle, VehicleSide.Bottom);
                AssignVehicleDirections(objVehicle, true, VehicleDirection);
                //Add Vehicle to Common List
                globalVariables.VehicleList.Add(objVehicle);
            }
        }

        private void GenerateVehiclesTop()
        {

            // Old Logic
            //int vehicleCounter = 0;

            ////int temp = globalVariables.TickCount;
            //int temp = globalVariables.TickCount - (int)((int)(globalVariables.TickCount / 100) * 100);


            //if (requiredToGenerateTop[temp] < 65)
            //{
            //    vehicleCounter = globalVariables.RndmNo.Next(0, 3);
            //}
            //else
            //{
            //    vehicleCounter = 0;
            //}

            //if (temp == 99)
            //{
            //    requiredToGenerateTop = functions.GetUniqueRandoms(1, 101);
            //}


            // New Logic
            int vehicleCounter = 0;
            int totalvehicles = globalVariables.VehicleListTop.Count();

            if (totalvehicles > 1)
            {
                int headywayvehicledistribution = 0;

                headywayvehicledistribution = functions.RandomNumberGenerator(0, 5);

                float frequencydistribution1 = (headywayvehicledistribution * 100) / totalvehicles;

                if (frequencydistribution1 < 65.60)
                {
                    int randno = functions.RandomNumberGenerator(0, 2);
                    if (randno == 0)
                    {
                        vehicleCounter = functions.RandomNumberGenerator(2, 5);
                    }
                }
                else
                {
                    vehicleCounter = functions.RandomNumberGenerator(0, 2);
                }
            }
            else
            {
                vehicleCounter = functions.RandomNumberGenerator(1, 5);
            }

            for (int i = 0; i < vehicleCounter; i++)
            {
                Vehicle objVehicle = new Vehicle();
                objVehicle.CurrentPosition = new CurrentPositionType();
                objVehicle.Properties = new VehicleProperties();

                #region Setting Default Properties
                objVehicle.Id = globalVariables.VehicleListTop.Count() + 1;
                //objVehicle.CurrentCellSpeed = 1;
                objVehicle.CurrentPosition.Row = 0;


                #endregion

                #region Determining Vehicle Type


                if (cntTypeOfVehiclesTop == NoOfVehicles - 1)
                {
                    cntTypeOfVehiclesTop = 0;
                }
                int VehicleType = ((globalVariables.rdTypeOfVehiclesTop[cntTypeOfVehiclesTop]) * 100) / NoOfVehicles; // functions.RandomNumberGenerator(1, 101);
                cntTypeOfVehiclesTop++;

                if (VehicleType >= 1 && VehicleType < 64 && CaftSettings.Default.TwoVhInclude)
                {
                    // Two Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.TwoWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.TwoVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 64 && VehicleType < 78 && CaftSettings.Default.ThreeVhInclude)
                {
                    // Three Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.ThreeWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.ThreeVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.ThreeVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.ThreeVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 78 && VehicleType < 93 && CaftSettings.Default.FourVhInclude)
                {
                    // Four Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.FourWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.FourVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.FourVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.FourVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 93 && VehicleType <= 94 && CaftSettings.Default.LCV1VhInclude)
                {
                    // LCV1
                    objVehicle.Properties.Type = Models.VehicleType.LCV1;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV1Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 93 && VehicleType <= 94 && CaftSettings.Default.LCV2VhInclude)
                {
                    // LCV2
                    objVehicle.Properties.Type = Models.VehicleType.LCV2;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV2VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV2VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV2Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 94 && VehicleType <= 95 && CaftSettings.Default.HCV1VhInclude)
                {
                    // HCV1
                    objVehicle.Properties.Type = Models.VehicleType.HCV1;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV1Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 94 && VehicleType <= 95 && CaftSettings.Default.HCV2VhInclude)
                {
                    // HCV2
                    objVehicle.Properties.Type = Models.VehicleType.HCV2;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV2VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV2VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV2Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else
                {
                    // Two Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.TwoWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.TwoVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                #endregion

                objVehicle.IsAccelerated = false;
                objVehicle.IsOvertaken = false;
                objVehicle.Vehicle_Headway = 0;
                AssignVehicleLane(objVehicle, VehicleSide.Top);

                AssignVehicleDirections(objVehicle, true);

                //Add Vehicle to Common List
                globalVariables.VehicleListTop.Add(objVehicle);
            }
        }

        private void GenerateVehiclesRight()
        {

            // Old Logic
            //int vehicleCounter = 0;

            ////int temp = globalVariables.TickCount;
            //int temp = globalVariables.TickCount - (int)((int)(globalVariables.TickCount / 100) * 100);


            //if (requiredToGenerateRight[temp] < 65)
            //{
            //    vehicleCounter = globalVariables.RndmNo.Next(0, 3);
            //}
            //else
            //{
            //    vehicleCounter = 0;
            //}

            //if (temp == 99)
            //{
            //    requiredToGenerateRight = functions.GetUniqueRandoms(1, 101);
            //}

            // New Logic
            int vehicleCounter = 0;
            int totalvehicles = globalVariables.VehicleListRight.Count();

            if (totalvehicles > 1)
            {
                int headywayvehicledistribution = 0;

                headywayvehicledistribution = functions.RandomNumberGenerator(0, 5);

                float frequencydistribution1 = (headywayvehicledistribution * 100) / totalvehicles;

                if (frequencydistribution1 < 65.60)
                {
                    int randno = functions.RandomNumberGenerator(0, 2);
                    if (randno == 0)
                    {
                        vehicleCounter = functions.RandomNumberGenerator(2, 5);
                    }
                }
                else
                {
                    vehicleCounter = functions.RandomNumberGenerator(0, 2);
                }
            }
            else
            {
                vehicleCounter = functions.RandomNumberGenerator(1, 5);
            }

            for (int i = 0; i < vehicleCounter; i++)
            {
                Vehicle objVehicle = new Vehicle();
                objVehicle.CurrentPosition = new CurrentPositionType();
                objVehicle.Properties = new VehicleProperties();

                #region Setting Default Properties
                objVehicle.Id = globalVariables.VehicleListRight.Count() + 1;
                //objVehicle.CurrentCellSpeed = 1;
                //objVehicle.CurrentPosition.Column = globalVariables.ExtraRoadCellsS;
                objVehicle.CurrentPosition.Column = globalVariables.TotalGridColumnCount;

                #endregion

                #region Determining Vehicle Type

                if (cntTypeOfVehiclesRight == NoOfVehicles - 1)
                {
                    cntTypeOfVehiclesRight = 0;
                }
                int VehicleType = ((globalVariables.rdTypeOfVehiclesRight[cntTypeOfVehiclesRight]) * 100) / NoOfVehicles; // functions.RandomNumberGenerator(1, 101);
                cntTypeOfVehiclesRight++;

                if (VehicleType >= 1 && VehicleType < 64 && CaftSettings.Default.TwoVhInclude)
                {
                    // Two Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.TwoWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.TwoVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 64 && VehicleType < 78 && CaftSettings.Default.ThreeVhInclude)
                {
                    // Three Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.ThreeWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.ThreeVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.ThreeVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.ThreeVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 78 && VehicleType < 93 && CaftSettings.Default.FourVhInclude)
                {
                    // Four Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.FourWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.FourVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.FourVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.FourVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 93 && VehicleType <= 94 && CaftSettings.Default.LCV1VhInclude)
                {
                    // LCV1
                    objVehicle.Properties.Type = Models.VehicleType.LCV1;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV1Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 93 && VehicleType <= 94 && CaftSettings.Default.LCV2VhInclude)
                {
                    // LCV2
                    objVehicle.Properties.Type = Models.VehicleType.LCV2;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV2VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV2VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV2Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 94 && VehicleType <= 95 && CaftSettings.Default.HCV1VhInclude)
                {
                    // HCV1
                    objVehicle.Properties.Type = Models.VehicleType.HCV1;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV1Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 94 && VehicleType <= 95 && CaftSettings.Default.HCV2VhInclude)
                {
                    // HCV2
                    objVehicle.Properties.Type = Models.VehicleType.HCV2;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV2VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV2VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV2Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else
                {
                    // Two Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.TwoWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.TwoVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                #endregion

                objVehicle.IsAccelerated = false;
                objVehicle.IsOvertaken = false;
                objVehicle.Vehicle_Headway = 0;
                AssignVehicleLane(objVehicle, VehicleSide.Right);

                AssignVehicleDirections(objVehicle, true);

                //Add Vehicle to Common List
                globalVariables.VehicleListRight.Add(objVehicle);
            }
        }

        private void GenerateVehiclesLeft()
        {

            /// Old Logic 
            //int vehicleCounter = 0;

            ////int temp = globalVariables.TickCount;
            //int temp = globalVariables.TickCount - (int)((int)(globalVariables.TickCount / 100) * 100);


            //if (requiredToGenerateRight[temp] < 65)
            //{
            //    vehicleCounter = globalVariables.RndmNo.Next(0, 3);
            //}
            //else
            //{
            //    vehicleCounter = 0;
            //}

            //if (temp == 99)
            //{
            //    requiredToGenerateRight = functions.GetUniqueRandoms(1, 101);
            //}

            // New Logic -
            int vehicleCounter = 0;
            int totalvehicles = globalVariables.VehicleListLeft.Count();

            if (totalvehicles > 1)
            {
                int headywayvehicledistribution = 0;

                headywayvehicledistribution = functions.RandomNumberGenerator(0, 5);

                float frequencydistribution1 = (headywayvehicledistribution * 100) / totalvehicles;

                if (frequencydistribution1 < 65.60)
                {
                    int randno = functions.RandomNumberGenerator(0, 2);
                    if (randno == 0)
                    {
                        vehicleCounter = functions.RandomNumberGenerator(2, 5);
                    }
                }
                else
                {
                    vehicleCounter = functions.RandomNumberGenerator(0, 2);
                }
            }
            else
            {
                vehicleCounter = functions.RandomNumberGenerator(1, 5);
            }

            for (int i = 0; i < vehicleCounter; i++)
            {
                Vehicle objVehicle = new Vehicle();
                objVehicle.CurrentPosition = new CurrentPositionType();
                objVehicle.Properties = new VehicleProperties();

                #region Setting Default Properties
                objVehicle.Id = globalVariables.VehicleListLeft.Count() + 1;
                //objVehicle.CurrentCellSpeed = 1;
                objVehicle.CurrentPosition.Column = 0;


                #endregion

                #region Determining Vehicle Type


                if (cntTypeOfVehiclesLeft == NoOfVehicles - 1)
                {
                    cntTypeOfVehiclesLeft = 0;
                }
                int VehicleType = ((globalVariables.rdTypeOfVehiclesLeft[cntTypeOfVehiclesLeft]) * 100) / NoOfVehicles; // functions.RandomNumberGenerator(1, 101);
                cntTypeOfVehiclesLeft++;

                if (VehicleType >= 1 && VehicleType < 64 && CaftSettings.Default.TwoVhInclude)
                {
                    // Two Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.TwoWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.TwoVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 64 && VehicleType < 78 && CaftSettings.Default.ThreeVhInclude)
                {
                    // Three Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.ThreeWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.ThreeVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.ThreeVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.ThreeVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 78 && VehicleType < 93 && CaftSettings.Default.FourVhInclude)
                {
                    // Four Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.FourWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.FourVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.FourVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.FourVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 93 && VehicleType <= 94 && CaftSettings.Default.LCV1VhInclude)
                {
                    // LCV1
                    objVehicle.Properties.Type = Models.VehicleType.LCV1;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV1VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV1Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 93 && VehicleType <= 94 && CaftSettings.Default.LCV2VhInclude)
                {
                    // LCV2
                    objVehicle.Properties.Type = Models.VehicleType.LCV2;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.LCV2VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.LCV2VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.LCV2Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 94 && VehicleType <= 95 && CaftSettings.Default.HCV1VhInclude)
                {
                    // HCV1
                    objVehicle.Properties.Type = Models.VehicleType.HCV1;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV1VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV1Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else if (VehicleType >= 94 && VehicleType <= 95 && CaftSettings.Default.HCV2VhInclude)
                {
                    // HCV2
                    objVehicle.Properties.Type = Models.VehicleType.HCV2;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.HCV2VhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.HCV2VhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.HCV2Vh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                else
                {
                    // Two Wheeler
                    objVehicle.Properties.Type = Models.VehicleType.TwoWheel;
                    objVehicle.Properties.RowOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Row);
                    objVehicle.Properties.ColumnOccupancy = Convert.ToInt16(CaftSettings.Default.TwoVhSize_Column);
                    objVehicle.Properties.MaxCellSpeed = Convert.ToInt16(CaftSettings.Default.TwoVh_MaxAcceleration);
                    objVehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(15, 20));
                }
                #endregion

                objVehicle.IsAccelerated = false;
                objVehicle.IsOvertaken = false;
                objVehicle.Vehicle_Headway = 0;
                AssignVehicleLane(objVehicle, VehicleSide.Left);

                AssignVehicleDirections(objVehicle, true);

                //Add Vehicle to Common List
                globalVariables.VehicleListLeft.Add(objVehicle);
            }
        }


        private void AssignVehicleLane(Vehicle objVehicle, VehicleSide vehicleSide)
        {
            int VehicleColumnSize = objVehicle.Properties.ColumnOccupancy;



            switch (vehicleSide)
            {
                case VehicleSide.Bottom:
                    int MaxCol = globalVariables.MaxColumn - objVehicle.Properties.ColumnOccupancy;
                    objVehicle.CurrentPosition.Column = functions.RandomNumberGenerator(globalVariables.ExtraRoadCellsLR + 1, MaxCol);
                    break;

                case VehicleSide.Left:

                    int minRow_Left = globalVariables.RowIntersection - globalVariables.TwoLaneColumnCount * 2 + objVehicle.Properties.ColumnOccupancy;
                    int maxRow_Left = globalVariables.RowIntersection - globalVariables.TwoLaneColumnCount * 1 - objVehicle.Properties.ColumnOccupancy;
                    objVehicle.CurrentPosition.Row = functions.RandomNumberGenerator(minRow_Left, maxRow_Left);
                    break;

                case VehicleSide.Top:
                    MaxCol = globalVariables.MaxColumn + globalVariables.TwoLaneColumnCount - objVehicle.Properties.ColumnOccupancy;
                    objVehicle.CurrentPosition.Column = functions.RandomNumberGenerator(globalVariables.ExtraRoadCellsLR + globalVariables.TwoLaneColumnCount + 1, MaxCol);
                    break;
                case VehicleSide.Right:
                    int maxRow = globalVariables.RowIntersection - objVehicle.Properties.ColumnOccupancy;
                    int minRow = (globalVariables.RowIntersection - globalVariables.TwoLaneColumnCount) + objVehicle.Properties.ColumnOccupancy;
                    objVehicle.CurrentPosition.Row = functions.RandomNumberGenerator(minRow, maxRow);
                    break;

                default:
                    break;
            }

            #region Old Logic
            //We need to set this as per vehicle type...
            // As four wheeler/Heavy vehicle can only be placed in 0 and 2nd column..

            //if (objVehicle.Properties.Type == VehicleType.TwoWheel)
            //{
            //    objVehicle.CurrentPosition.Column = functions.RandomNumberGenerator(0, 4);
            //}
            //else
            //{
            //    int random = functions.RandomNumberGenerator(1, 3);
            //    switch (random)
            //    {
            //        case 1:
            //            objVehicle.CurrentPosition.Column = 0;
            //            break;

            //        default:
            //            objVehicle.CurrentPosition.Column = 2;
            //            break;
            //    }
            //}
            #endregion

        }

        private void AssignVehicleDirections(Vehicle objVehicle, bool isotherdirection = false, int random = 0)
        {
            if (random == 0) return;
            if (random > 0 && random <= StraightRatio)
            {
                //Straight
                objVehicle.Direction = DirectionType.Straight;
            }
            else if (random > StraightRatio && random <= RightRatio)
            {
                //Right
                objVehicle.Direction = DirectionType.Right;
            }
            else if (random > RightRatio && random <= LeftRatio)
            {
                //Left
                objVehicle.Direction = DirectionType.Left;
            }
            else
            {
                //Left - Default
                objVehicle.Direction = DirectionType.Left;
            }

            objVehicle.Properties.IsDirectionChanged = true;

            #region Assigning Directions

            //int DirectionRandomNumber = 0;

            //if (!isotherdirection)
            //{
            //    int mincol = globalVariables.MinColumn;
            //    int maxcol = globalVariables.MaxColumn;
            //    int mediator = globalVariables.MaxColumn - globalVariables.MinColumn;
            //    int med = mediator / 2;
            //    int curColumn = objVehicle.CurrentPosition.Column;

            //    if (curColumn >= mincol && curColumn <= mincol + med)
            //    {
            //        DirectionRandomNumber = functions.RandomNumberGenerator(1, 3); // Vehicles in First or Second Lane will head for Straight or Left
            //    }
            //    else
            //    {
            //        DirectionRandomNumber = functions.RandomNumberGenerator(2, 4); // Vehicles in third of fourth Lane will head for Straight or Right
            //    }
            //}
            //else
            //{
            //    // Assigning Vehicle directions for vehicles generated form left, top and right side for data analysis purpose 
            //    DirectionRandomNumber = functions.RandomNumberGenerator(1, 4);
            //}

            //switch (DirectionRandomNumber)
            //{
            //    case 1:
            //        objVehicle.Direction = Models.DirectionType.Left;
            //        break;

            //    case 2:
            //        objVehicle.Direction = Models.DirectionType.Straight;
            //        break;

            //    case 3:
            //        objVehicle.Direction = Models.DirectionType.Right;
            //        break;

            //    default:
            //        objVehicle.Direction = Models.DirectionType.Straight;
            //        break;
            //}

            //objVehicle.Properties.IsDirectionChanged = true;

            //// Testing each direction
            ////objVehicle.Direction = Models.DirectionType.Right;
            #endregion
        }

        private static void AssignColorToVehicle(Vehicle vhcle, System.Windows.Shapes.Rectangle rectangle)
        {
            if (vhcle.Properties.Type == VehicleType.TwoWheel)
            {
                rectangle.Fill = new SolidColorBrush(Colors.Black);
            }
            else if (vhcle.Properties.Type == VehicleType.ThreeWheel)
            {
                rectangle.Fill = new SolidColorBrush(Colors.Yellow);
            }
            else if (vhcle.Properties.Type == VehicleType.FourWheel)
            {
                rectangle.Fill = new SolidColorBrush(Colors.Red);
            }
            else if (vhcle.Properties.Type == VehicleType.LCV1)
            {
                rectangle.Fill = new SolidColorBrush(Colors.Green);
            }
            else if (vhcle.Properties.Type == VehicleType.LCV2)
            {
                rectangle.Fill = new SolidColorBrush(Colors.YellowGreen);
            }
            else if (vhcle.Properties.Type == VehicleType.HCV1)
            {
                rectangle.Fill = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                rectangle.Fill = new SolidColorBrush(Colors.BlueViolet);
            }
        }

        #endregion

        #region Process Queue

        /// <summary>
        /// To be developed by Keval -
        /// </summary>
        private void ProcessQueue()
        {
            //File.AppendAllText("Logs.txt", "===ProcessQueue_Start===\r\n");
            ProcessMidSignal();


            ProcessNoiseProbabilty();


            foreach (var vehicle in globalVariables.VehicleList)
            {
                if (vehicle.CurrentPosition.PreviousRow == vehicle.CurrentPosition.Row
                    && vehicle.Properties.Status == VehicleStatus.InProgress
                    && !globalVariables.LaneSignalOn
                    && vehicle.CurrentPosition.Row < globalVariables.RowIntersection + 100
                    && vehicle.CurrentPosition.Row >= globalVariables.RowIntersection)
                {
                    vehicle.IsStoppedForSignal += 1;
                }

                if (vehicle.CurrentPosition.PreviousRow == vehicle.CurrentPosition.Row
                    && vehicle.Properties.Status == VehicleStatus.InProgress
                    && !globalVariables.LaneSignalOnFirst
                    && vehicle.CurrentPosition.Row < CaftSettings.Default.BumpLine + 100
                    && vehicle.CurrentPosition.Row >= CaftSettings.Default.BumpLine)
                {
                    vehicle.IsStoppedForSignalFirst += 1;
                }

                vehicle.CurrentPosition.PreviousRow = vehicle.CurrentPosition.Row;


                #region MidBlock
                if (!IsMidBlockRed)
                {
                    if (vehicle.Properties.Status == VehicleStatus.AtMidSignal)
                    {
                        vehicle.Properties.Status = VehicleStatus.InProgress;
                    }
                }
                else
                {
                    if (vehicle.Properties.Status == VehicleStatus.AtMidSignal)
                    {
                        CalculateSpacePerTime(vehicle);
                    }
                }
                #endregion

                #region Normal Flow
                if (vehicle.Properties.Status != VehicleStatus.Completed
                    && vehicle.Properties.Status != VehicleStatus.AtMidSignal)
                {
                    if (globalVariables.LaneSignalOn || vehicle.Direction == DirectionType.Left)
                    {
                        partial_ProcessQueue(vehicle);
                    }
                    else
                    {
                        if (vehicle.CurrentPosition.Row >= globalVariables.RowIntersection && vehicle.CurrentPosition.Row - vehicle.CurrentCellSpeed < globalVariables.RowIntersection + 10)
                        {
                            if (!vehicle.Properties.IsDirectionChanged)
                            {
                                AssignVehicleDirections(vehicle);
                            }

                            if (vehicle.CurrentPosition.Row > globalVariables.RowIntersection)
                            {

                                vehicle.CurrentCellSpeed = vehicle.CurrentPosition.Row - globalVariables.RowIntersection;

                            REDUCESPEED3:
                                if (CheckNextCell(vehicle))
                                {
                                    vehicle.Properties.Status = VehicleStatus.InProgress;
                                    MoveVehicle(vehicle);
                                }
                                else
                                {
                                    if (vehicle.CurrentPosition.Row > globalVariables.RowIntersection && vehicle.CurrentCellSpeed > 1)
                                    {
                                        vehicle.CurrentCellSpeed -= 1;
                                        goto REDUCESPEED3;
                                    }
                                }
                            }

                            // Setting Headway
                            if (vehicle.CurrentPosition.Row <= CaftSettings.Default.headway && vehicle.Vehicle_Headway == 0)
                            {
                                vehicle.Vehicle_Headway = globalVariables.TickCount;
                            }

                            //if (vehicle.CurrentPosition.Row < globalVariables.RowIntersection)
                            //{
                            //    vehicle.SaturationTime = globalVariables.TickCount;
                            //}
                        }
                        else
                        {
                            partial_ProcessQueue(vehicle);
                        }
                    }

                    if (vehicle.Properties.Status != VehicleStatus.InQueue)
                    {
                        CalculateSpacePerTime(vehicle);
                    }

                    ReduceSpeedIfNearToIntersection(vehicle);

                    CalculateIntersectionHeadway(vehicle);

                    PartialCalculateSpeedPerTime(vehicle);

                    PartialCalculateDensityPerTime(vehicle);

                    CalculateSaturationBottom(vehicle);

                    CalculateInterSectionDelay(vehicle);

                    //CalculateBumpBeforeAfter(vehicle);

                }
                #endregion
            }

            CalculateSpeedPerTime();
            CalculateDensityPerTime();
            CalculateDensityPerSpeed();
            CalculateNoiseProbFactor();

            AvgSpeed = 0;
            NoOfVehiclesPerKm = 0;

            globalVariables.TotalVehicles_BL = globalVariables.VehicleList.Count();
            globalVariables.InProgress_BL = globalVariables.VehicleList.Where(qs => qs.Properties.Status == VehicleStatus.InProgress).Count();
            globalVariables.InQueue_BL = globalVariables.VehicleList.Where(qs => qs.Properties.Status == VehicleStatus.InQueue).Count();
            globalVariables.Completed_BL = globalVariables.VehicleList.Where(qs => qs.Properties.Status == VehicleStatus.Completed).Count();

            //File.AppendAllText("Logs.txt", "===ProcessQueue_End===\r\n");
        }

        public void CalculateBumpBeforeAfter(Vehicle vehicle)
        {
            int row = vehicle.CurrentPosition.Row;

            if (row < globalVariables.bumpB60 
                && row > globalVariables.bumpB40 && vehicle.bumpB60 == 0.0)
            {
                vehicle.bumpB60 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                return;
            }

            if (row < globalVariables.bumpB40 
                && row > globalVariables.bumpB20 && vehicle.bumpB40 == 0.0)
            {
                vehicle.bumpB40 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                return;
            }

            if (row < globalVariables.bumpB20
                && row > CaftSettings.Default.BumpLine + ((int)Math.Ceiling(3 / CaftSettings.Default.CellSize_Height)) && vehicle.bumpB20 == 0.0)
            {
                vehicle.bumpB20 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                return;
            }

            if (row < CaftSettings.Default.BumpLine + ((int)Math.Ceiling(3 / CaftSettings.Default.CellSize_Height))
                && row > CaftSettings.Default.BumpLine && vehicle.OnBump == 0.0)
            {
                vehicle.OnBump = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                return;
            }

            if (row < CaftSettings.Default.BumpLine
                && row > globalVariables.bumpA20 && vehicle.bumpA20 == 0.0)
            {
                vehicle.bumpA20 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                return;
            }

            if (row < globalVariables.bumpA20 
                && row > globalVariables.bumpA40 && vehicle.bumpA40 == 0.0)
            {
                vehicle.bumpA40 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                return;
            }

            if (row < globalVariables.bumpA40
                && row > globalVariables.bumpA60 && vehicle.bumpA60 == 0.0)
            {
                vehicle.bumpA60 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                return;
            }
        }



        private void ProcessNoiseProbabilty()
        {
            var vhCount = globalVariables.VehicleList.Where(p => p.Properties.DirectionStatus == VehicleDirectionChange.NoNeedToChange).Count();
            if (vhCount > 0)
            {
                int vhToNoiseCount = Convert.ToInt16(Math.Round((((double)vhCount * (double)CaftSettings.Default.NoiseProbFactor) / 100)));
                List<int> RandomArray = functions.GetUniqueRandoms(0, vhCount).Take(vhToNoiseCount).ToList<int>();
                foreach (var item in RandomArray)
                {
                    var tempVh = globalVariables.VehicleList.Where(p => p.Properties.DirectionStatus == VehicleDirectionChange.NoNeedToChange);
                    if (tempVh != null) tempVh.ElementAt(item).IsNoisy = true;
                }
                globalVariables.NoiseProbFactors.Add(globalVariables.TickCount, new NoiseProbabiltyFactorType() { TotalCount = vhCount, NoisyCount = vhToNoiseCount });
                //if(vhToNoiseCount > vhCount)
                //{

                //}

            }
        }


        /// <summary>
        /// To be developed by Keval -
        /// </summary>
        private void ProcessQueue_Top()
        {
            foreach (var vehicle in globalVariables.VehicleListTop)
            {
                #region Normal Flow
                if (vehicle.Properties.Status != VehicleStatus.Completed)
                {
                    if (globalVariables.LaneSignalOn_Top || vehicle.Direction == DirectionType.Right)
                    {
                        partial_ProcessQueue_Top(vehicle);
                    }
                    else
                    {
                        int rowintersection = globalVariables.RowIntersection - globalVariables.TwoLaneColumnCount * 2;
                        if (vehicle.CurrentPosition.Row < rowintersection && vehicle.CurrentPosition.Row + vehicle.CurrentCellSpeed > rowintersection - 10)
                        {
                            //if (!vehicle.Properties.IsDirectionChanged)
                            //{
                            //    AssignVehicleDirections(vehicle);
                            //}

                            if (vehicle.CurrentPosition.Row < rowintersection)
                            {
                                vehicle.CurrentCellSpeed = rowintersection - vehicle.CurrentPosition.Row;

                            REDUCESPEED3:
                                if (CheckNextCellTop(vehicle))
                                {
                                    vehicle.Properties.Status = VehicleStatus.InProgress;
                                    UpdateVehiclePositionTop(vehicle);
                                }
                                else
                                {
                                    if (vehicle.CurrentPosition.Row < rowintersection && vehicle.CurrentCellSpeed > 1)
                                    {
                                        vehicle.CurrentCellSpeed -= 1;
                                        goto REDUCESPEED3;
                                    }
                                }
                            }
                        }
                        else if (vehicle.CurrentPosition.Row < rowintersection)
                        {
                            vehicle.CurrentCellSpeed += 1;
                        REDUCESPEED4:
                            if (CheckNextCellTop(vehicle))
                            {
                                vehicle.Properties.Status = VehicleStatus.InProgress;
                                UpdateVehiclePositionTop(vehicle);
                            }
                            else
                            {
                                if (vehicle.CurrentPosition.Row < rowintersection && vehicle.CurrentCellSpeed > 1)
                                {
                                    vehicle.CurrentCellSpeed -= 1;
                                    goto REDUCESPEED4;
                                }
                            }
                        }
                        else if (vehicle.CurrentPosition.Row > rowintersection)
                        {
                            partial_ProcessQueue_Top(vehicle);
                        }
                        else
                        {
                            vehicle.CurrentCellSpeed = 1;
                        }
                    }

                    CalculateSaturationTop(vehicle);
                }
                #endregion
            }

            globalVariables.TotalVehicles_TL = globalVariables.VehicleListTop.Count();
            globalVariables.InProgress_TL = globalVariables.VehicleListTop.Where(qs => qs.Properties.Status == VehicleStatus.InProgress).Count();
            globalVariables.InQueue_TL = globalVariables.VehicleListTop.Where(qs => qs.Properties.Status == VehicleStatus.InQueue).Count();
            globalVariables.Completed_TL = globalVariables.VehicleListTop.Where(qs => qs.Properties.Status == VehicleStatus.Completed).Count();

            //File.AppendAllText("Logs.txt", "===ProcessQueue_End===\r\n");
        }


        /// <summary>
        /// To be developed by Keval -
        /// </summary>
        private void ProcessQueue_Right()
        {
            foreach (var vehicle in globalVariables.VehicleListRight)
            {
                #region Normal Flow
                if (vehicle.Properties.Status != VehicleStatus.Completed)
                {
                    if (globalVariables.LaneSignalOn_Right)
                    {
                        partial_ProcessQueue_Right(vehicle);
                    }
                    else
                    {
                        int columnintersection = globalVariables.TotalGridColumnCount - globalVariables.ExtraRoadCellsLR;

                        if (vehicle.CurrentPosition.Column > columnintersection && vehicle.CurrentPosition.Column - vehicle.CurrentCellSpeed < columnintersection + 5)
                        {
                            if (vehicle.CurrentPosition.Column > columnintersection)
                            {
                                vehicle.CurrentCellSpeed = vehicle.CurrentPosition.Column - columnintersection;

                            REDUCESPEED3:
                                if (CheckNextCellLeft(vehicle))
                                {
                                    vehicle.Properties.Status = VehicleStatus.InProgress;
                                    UpdateVehiclePosition_Right(vehicle);
                                }
                                else
                                {
                                    if (vehicle.CurrentPosition.Column > columnintersection && vehicle.CurrentCellSpeed > 1)
                                    {
                                        vehicle.CurrentCellSpeed -= 1;
                                        goto REDUCESPEED3;
                                    }
                                }
                            }
                        }
                        else if (vehicle.CurrentPosition.Column > columnintersection)
                        {
                            vehicle.CurrentCellSpeed += 1;
                        REDUCESPEED4:
                            if (CheckNextCellLeft(vehicle))
                            {
                                vehicle.Properties.Status = VehicleStatus.InProgress;
                                UpdateVehiclePosition_Right(vehicle);
                            }
                            else
                            {
                                if (vehicle.CurrentPosition.Column > columnintersection && vehicle.CurrentCellSpeed > 1)
                                {
                                    vehicle.CurrentCellSpeed -= 1;
                                    goto REDUCESPEED4;
                                }
                            }
                        }
                        else if (vehicle.CurrentPosition.Column < columnintersection)
                        {
                            partial_ProcessQueue_Right(vehicle);
                        }
                        else
                        {
                            vehicle.CurrentCellSpeed = 1;
                        }
                    }

                    CalculateSaturationRight(vehicle);
                }
                #endregion
            }

            globalVariables.TotalVehicles_RL = globalVariables.VehicleListRight.Count();
            globalVariables.InProgress_RL = globalVariables.VehicleListRight.Where(qs => qs.Properties.Status == VehicleStatus.InProgress).Count();
            globalVariables.InQueue_RL = globalVariables.VehicleListRight.Where(qs => qs.Properties.Status == VehicleStatus.InQueue).Count();
            globalVariables.Completed_RL = globalVariables.VehicleListRight.Where(qs => qs.Properties.Status == VehicleStatus.Completed).Count();

            //File.AppendAllText("Logs.txt", "===ProcessQueue_End===\r\n");
        }

        private void ProcessQueue_Left()
        {
            foreach (var vehicle in globalVariables.VehicleListLeft)
            {
                #region Normal Flow
                if (vehicle.Properties.Status != VehicleStatus.Completed)
                {
                    if (globalVariables.LaneSignalOn_Left)
                    {
                        partial_ProcessQueue_Left(vehicle);
                    }
                    else
                    {

                        // Intersection:
                        int columnintersection = globalVariables.ExtraRoadCellsLR;

                        if (vehicle.CurrentPosition.Column < columnintersection && vehicle.CurrentPosition.Column + vehicle.CurrentCellSpeed > columnintersection - 5)
                        {
                            if (vehicle.CurrentPosition.Column < columnintersection)
                            {
                                vehicle.CurrentCellSpeed = columnintersection - vehicle.CurrentPosition.Column;

                            REDUCESPEED3:
                                if (CheckNextCellRight(vehicle))
                                {
                                    vehicle.Properties.Status = VehicleStatus.InProgress;
                                    UpdateVehiclePosition_Left(vehicle);
                                }
                                else
                                {
                                    if (vehicle.CurrentPosition.Column < columnintersection && vehicle.CurrentCellSpeed > 1)
                                    {
                                        vehicle.CurrentCellSpeed -= 1;
                                        goto REDUCESPEED3;
                                    }
                                }
                            }
                        }
                        else if (vehicle.CurrentPosition.Column < columnintersection)
                        {
                            vehicle.CurrentCellSpeed += 1;
                        REDUCESPEED4:
                            if (CheckNextCellRight(vehicle))
                            {
                                vehicle.Properties.Status = VehicleStatus.InProgress;
                                UpdateVehiclePosition_Left(vehicle);
                            }
                            else
                            {
                                if (vehicle.CurrentPosition.Column < columnintersection && vehicle.CurrentCellSpeed > 1)
                                {
                                    vehicle.CurrentCellSpeed -= 1;
                                    goto REDUCESPEED4;
                                }
                            }
                        }
                        else if (vehicle.CurrentPosition.Column > columnintersection)
                        {
                            partial_ProcessQueue_Left(vehicle);
                        }
                        else
                        {
                            vehicle.CurrentCellSpeed = 1;
                        }
                    }

                    CalculateSaturationLeft(vehicle);
                }
                #endregion
            }

            globalVariables.TotalVehicles_LL = globalVariables.VehicleListLeft.Count();
            globalVariables.InProgress_LL = globalVariables.VehicleListLeft.Where(qs => qs.Properties.Status == VehicleStatus.InProgress).Count();
            globalVariables.InQueue_LL = globalVariables.VehicleListLeft.Where(qs => qs.Properties.Status == VehicleStatus.InQueue).Count();
            globalVariables.Completed_LL = globalVariables.VehicleListLeft.Where(qs => qs.Properties.Status == VehicleStatus.Completed).Count();

            //File.AppendAllText("Logs.txt", "===ProcessQueue_End===\r\n");
        }

        private void partial_ProcessQueue(Vehicle vehicle)
        {
            // Testing
            //vehicle.Direction = Models.DirectionType.Right;

            //// Assigning Lane just before crossing intersection
            //if (vehicle.CurrentPosition.Row >= globalVariables.RowIntersection && vehicle.CurrentPosition.Row - vehicle.CurrentCellSpeed < globalVariables.RowIntersection)
            //{
            //    AssignVehicleDirections(vehicle);
            //}


            if (vehicle.Properties.DirectionStatus == VehicleDirectionChange.NoNeedToChange)
            {

                if (CheckNextCell(vehicle))
                {
                    if (vehicle.Properties.Status == VehicleStatus.InQueue)
                    {
                        vehicle.StartTime = globalVariables.TickCount;
                    }

                    vehicle.Properties.Status = VehicleStatus.InProgress;

                    // Random Acceleration - 
                    //int i = functions.RandomNumberGenerator(0, 101);

                    if (!vehicle.IsNoisy || vehicle.CurrentPosition.Row < globalVariables.RowIntersection - 1
                        || (globalVariables.LaneSignalOn && vehicle.CurrentPosition.Row > globalVariables.RowIntersection))
                    {

                        if (CanAccelerate(vehicle))
                        {
                            //vehicle.CurrentCellSpeed += 1;
                            // Speed of the vehicle is increased after acceleration
                            DoAcceleration(vehicle);

                        REDUCESPEED1:
                            if (!CheckNextCell(vehicle))
                            {
                                if (vehicle.CurrentCellSpeed > 1)
                                {
                                    vehicle.CurrentCellSpeed -= 1;
                                    goto REDUCESPEED1;
                                }
                            }
                        }
                    }
                    bool isVehicleMoved = false;
                    if (CaftSettings.Default.signalInclude)
                    {
                        isVehicleMoved = CheckWhetherSignal(vehicle, CaftSettings.Default.BumpLine, globalVariables.LaneSignalOnFirst);
                    }

                    if (CaftSettings.Default.bumpInclude)
                    {
                        ProcessBump(vehicle);
                    }

                    if (CaftSettings.Default.signalIncludePed)
                    {
                        isVehicleMoved = CheckWhetherSignal(vehicle, CaftSettings.Default.SignalLinePed, !IsMidBlockRed);
                    }


                    // Assigning Lane just before crossing intersection
                    if (vehicle.CurrentPosition.Row >= globalVariables.RowIntersection && vehicle.CurrentPosition.Row - vehicle.CurrentCellSpeed < globalVariables.RowIntersection)
                    {
                        if (!vehicle.Properties.IsDirectionChanged)
                        {
                            AssignVehicleDirections(vehicle);
                        }

                    }


                    // Vehicle Direction Changing
                    if (vehicle.Direction != DirectionType.Straight)
                    {
                        int speeddiff = vehicle.CurrentPosition.Row - vehicle.CurrentCellSpeed; ;

                        if (speeddiff < globalVariables.RowIntersection)
                        {

                            vehicle.CurrentCellSpeed = vehicle.CurrentPosition.Row - globalVariables.RowIntersection;


                            if (vehicle.CurrentCellSpeed < 1)
                            {
                                vehicle.CurrentCellSpeed = 1;
                            }
                            vehicle.Properties.DirectionStatus = VehicleDirectionChange.NeedToChange;
                            //MessageBox.Show("Direction of Vehicle Changed");
                            // Changing Lane
                        }
                    }

                    if (!isVehicleMoved) MoveVehicle(vehicle);

                }
                else if (vehicle.CurrentPosition.Row == globalVariables.TotalGridRowCount)
                {
                    vehicle.Properties.Status = VehicleStatus.InQueue;
                }
                //else if (false) // for overtaking 
                else if (CheckLaneOvertake(vehicle, CaftSettings.Default.signalIncludePed ? !IsMidBlockRed : globalVariables.LaneSignalOnFirst, CaftSettings.Default.signalIncludePed ? CaftSettings.Default.SignalLinePed : CaftSettings.Default.BumpLine)) // for overtaking
                {
                    vehicle.IsOvertaken = true;
                    vehicle.Properties.Status = VehicleStatus.InProgress;
                }
                else if (vehicle.CurrentCellSpeed > 1)
                {
                REDUCESPEED2:
                    if (!CheckNextCell(vehicle))
                    {
                        if (vehicle.CurrentCellSpeed > 1)
                        {
                            vehicle.CurrentCellSpeed -= 1;
                            goto REDUCESPEED2;
                        }
                    }
                    else
                    {
                        bool isVehicleMoved = false;
                        if (CaftSettings.Default.bumpInclude)
                        {
                            ProcessBump(vehicle);
                        }
                        if (CaftSettings.Default.signalIncludePed)
                        {
                            isVehicleMoved = CheckWhetherSignal(vehicle, CaftSettings.Default.SignalLinePed, !IsMidBlockRed);
                        }

                        if (!isVehicleMoved)
                        {
                            MoveVehicle(vehicle);
                        }

                    }
                }

                if (vehicle.CurrentPosition.Row <= (CaftSettings.Default.HCV2VhSize_Row + 1) * (-1))
                {
                    vehicle.CurrentPosition.Row = -9000;
                    vehicle.Properties.Status = VehicleStatus.Completed;

                    vehicle.EndTime = globalVariables.TickCount;
                }

                // Setting Headway
                if (vehicle.CurrentPosition.Row <= CaftSettings.Default.headway && vehicle.Vehicle_Headway == 0)
                {
                    vehicle.Vehicle_Headway = globalVariables.TickCount;
                }

            }
            else
            {
                // Logic for Displacing Vehicle whose lane should be changed
                //MessageBox.Show("Vehicle Direction has been changed. Need displacement of the vehicle.");

                if (CheckNextCell(vehicle, true))
                {
                    UpdateVehiclePosition_DirectionChange(vehicle);
                }
                else
                {
                    // Logic for changing Vehicle Lane in different Row - Work in progress
                    //vehicle.CurrentPosition.Row = vehicle.CurrentPosition.Row - vehicle.Properties.ColumnOccupancy;
                    //if (CheckNextCell(vehicle, true))
                    //{
                    //    UpdateVehiclePosition_DirectionChange(vehicle);
                    //}
                    //else
                    //{
                    //    vehicle.CurrentPosition.Row = vehicle.CurrentPosition.Row + vehicle.Properties.ColumnOccupancy;
                    //}
                }

                if (vehicle.Direction == DirectionType.Left)
                {
                    if (vehicle.CurrentPosition.Column <= (CaftSettings.Default.HCV2VhSize_Row + 1) * (-1))
                    {
                        vehicle.CurrentPosition.Column = -9000;
                        vehicle.Properties.Status = VehicleStatus.Completed;
                        vehicle.EndTime = globalVariables.TickCount;
                    }
                }
                else
                {
                    if (vehicle.CurrentPosition.Column > (CaftSettings.Default.HCV2VhSize_Row + 1) + globalVariables.TotalGridColumnCount)
                    {
                        vehicle.CurrentPosition.Column = -9000;
                        vehicle.Properties.Status = VehicleStatus.Completed;
                        vehicle.EndTime = globalVariables.TickCount;
                    }
                }


            }



        }

       

        private void partial_ProcessQueue_Top(Vehicle vehicle)
        {
            // Testing
            //vehicle.Direction = Models.DirectionType.Right;

            if (vehicle.Properties.DirectionStatus == VehicleDirectionChange.NoNeedToChange)
            {
                vehicle.CurrentCellSpeed += 1;

                if (CheckNextCellTop(vehicle))
                {
                    if (vehicle.Properties.Status == VehicleStatus.InQueue)
                    {
                        vehicle.StartTime = globalVariables.TickCount;
                    }

                    vehicle.Properties.Status = VehicleStatus.InProgress;

                    int rowintersection = globalVariables.RowIntersection - globalVariables.TwoLaneColumnCount * 2;

                    // Assigning Lane just before crossing intersection
                    //if (vehicle.CurrentPosition.Row <= rowintersection && vehicle.CurrentPosition.Row + vehicle.CurrentCellSpeed > globalVariables.RowIntersection)
                    //{
                    //    if (!vehicle.Properties.IsDirectionChanged)
                    //    {
                    //        AssignVehicleDirections(vehicle);
                    //    }

                    //}

                    // Vehicle Direction Changing
                    //if (vehicle.Direction != DirectionType.Straight)
                    //{
                    //    int speeddiff = vehicle.CurrentPosition.Row + vehicle.CurrentCellSpeed; ;

                    //    if (speeddiff > rowintersection)
                    //    {
                    //        vehicle.CurrentCellSpeed = rowintersection - vehicle.CurrentPosition.Row;

                    //        if (vehicle.CurrentCellSpeed < 1)
                    //        {
                    //            vehicle.CurrentCellSpeed = 1;
                    //        }

                    //        vehicle.Properties.DirectionStatus = VehicleDirectionChange.NeedToChange;
                    //    }
                    //}

                    UpdateVehiclePositionTop(vehicle);
                }
                else if (vehicle.CurrentPosition.Row == 0)
                {
                    vehicle.Properties.Status = VehicleStatus.InQueue;
                }
                else if (vehicle.CurrentCellSpeed > 1)
                {
                REDUCESPEED2:
                    if (!CheckNextCellTop(vehicle))
                    {
                        if (vehicle.CurrentCellSpeed > 1)
                        {
                            vehicle.CurrentCellSpeed -= 1;
                            goto REDUCESPEED2;
                        }
                    }
                }

                if (vehicle.CurrentPosition.Row >= (CaftSettings.Default.HCV2VhSize_Row + 1) + globalVariables.TotalGridRowCount)
                {
                    vehicle.CurrentPosition.Row = -9000;
                    vehicle.Properties.Status = VehicleStatus.Completed;

                    vehicle.EndTime = globalVariables.TickCount;
                }
            }
            else
            {
                // Logic for Displacing Vehicle whose lane should be changed
                //MessageBox.Show("Vehicle Direction has been changed. Need displacement of the vehicle.");

                if (CheckNextCell(vehicle, true))
                {
                    UpdateVehiclePosition_DirectionChange(vehicle);
                }

                if (vehicle.Direction == DirectionType.Left)
                {
                    if (vehicle.CurrentPosition.Column <= (CaftSettings.Default.HCV2VhSize_Row + 1) * (-1))
                    {
                        vehicle.CurrentPosition.Column = -9000;
                        vehicle.Properties.Status = VehicleStatus.Completed;
                        vehicle.EndTime = globalVariables.TickCount;
                    }
                }
                else
                {
                    if (vehicle.CurrentPosition.Column > (CaftSettings.Default.HCV2VhSize_Row + 1) + (globalVariables.MaxColumn * 2))
                    {
                        vehicle.CurrentPosition.Column = -9000;
                        vehicle.Properties.Status = VehicleStatus.Completed;
                        vehicle.EndTime = globalVariables.TickCount;
                    }
                }


            }



        }

        private void partial_ProcessQueue_Right(Vehicle vehicle)
        {
            if (vehicle.Properties.DirectionStatus == VehicleDirectionChange.NoNeedToChange)
            {
                vehicle.CurrentCellSpeed += 1;

                if (CheckNextCellLeft(vehicle))
                {
                    if (vehicle.Properties.Status == VehicleStatus.InQueue)
                    {
                        vehicle.StartTime = globalVariables.TickCount;
                    }
                    vehicle.Properties.Status = VehicleStatus.InProgress;
                    UpdateVehiclePosition_Right(vehicle);
                }
                else if (vehicle.CurrentPosition.Column == globalVariables.TotalGridColumnCount)
                {
                    vehicle.Properties.Status = VehicleStatus.InQueue;
                }
                else if (vehicle.CurrentCellSpeed > 1)
                {
                REDUCESPEED2:
                    if (!CheckNextCellLeft(vehicle))
                    {
                        if (vehicle.CurrentCellSpeed > 1)
                        {
                            vehicle.CurrentCellSpeed -= 1;
                            goto REDUCESPEED2;
                        }
                    }
                }

                if (vehicle.CurrentPosition.Column <= (CaftSettings.Default.HCV2VhSize_Row + 1) * (-1))
                {
                    vehicle.CurrentPosition.Row = -9000;
                    vehicle.Properties.Status = VehicleStatus.Completed;
                    vehicle.EndTime = globalVariables.TickCount;
                }
            }
        }

        private void partial_ProcessQueue_Left(Vehicle vehicle)
        {
            if (vehicle.Properties.DirectionStatus == VehicleDirectionChange.NoNeedToChange)
            {
                vehicle.CurrentCellSpeed += 1;

                if (CheckNextCellRight(vehicle))
                {
                    if (vehicle.Properties.Status == VehicleStatus.InQueue)
                    {
                        vehicle.StartTime = globalVariables.TickCount;
                    }

                    vehicle.Properties.Status = VehicleStatus.InProgress;

                    int columnIntersection = globalVariables.ExtraRoadCellsLR;

                    UpdateVehiclePosition_Left(vehicle);
                }
                else if (vehicle.CurrentPosition.Column == 0)
                {
                    vehicle.Properties.Status = VehicleStatus.InQueue;
                }
                else if (vehicle.CurrentCellSpeed > 1)
                {
                REDUCESPEED2:
                    if (!CheckNextCellRight(vehicle))
                    {
                        if (vehicle.CurrentCellSpeed > 1)
                        {
                            vehicle.CurrentCellSpeed -= 1;
                            goto REDUCESPEED2;
                        }
                    }
                }

                if (vehicle.CurrentPosition.Column >= (CaftSettings.Default.HCV2VhSize_Row + 1) + globalVariables.TotalGridColumnCount)
                {
                    vehicle.CurrentPosition.Row = -9000;
                    vehicle.Properties.Status = VehicleStatus.Completed;

                    vehicle.EndTime = globalVariables.TickCount;
                }
            }
            else // Vehicle whose lane should be changed
            {

            }
        }

        private void ReduceSpeedIfNearToIntersection(Vehicle vehicle)
        {
            if (!globalVariables.LaneSignalOn)
            {
                int RowToCount = globalVariables.RowIntersection + (int)(CaftSettings.Default.CellSize_Height * CaftSettings.Default.IntersectionSpeedZone);
                if (vehicle.CurrentPosition.Row < RowToCount)
                {
                    if (vehicle.CurrentCellSpeed > 6) vehicle.CurrentCellSpeed -= 2;
                }

                if (vehicle.CurrentPosition.Row < RowToCount - 20)
                {
                    if (vehicle.CurrentCellSpeed > 4) vehicle.CurrentCellSpeed -= 2;
                }
            }
        }


        private void ProcessBump(Vehicle vehicle)
        {
            try
            {
                if (vehicle.CurrentPosition.Row < globalVariables.bumpB60
                            && vehicle.CurrentPosition.Row > globalVariables.bumpB40)
                {
                    vehicle.CurrentCellSpeed = functions.SpeedBumpB60(vehicle.Properties.Type);
                    vehicle.bumpB60 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                    
                }

                if (vehicle.CurrentPosition.Row < globalVariables.bumpB40
                 && vehicle.CurrentPosition.Row > globalVariables.bumpB20)
                {
                    vehicle.CurrentCellSpeed = functions.SpeedBumpB40(vehicle.Properties.Type);
                    vehicle.bumpB40 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                }

                if (vehicle.CurrentPosition.Row < globalVariables.bumpB20
                    && vehicle.CurrentPosition.Row > CaftSettings.Default.BumpLine + ((int)Math.Ceiling(3 / CaftSettings.Default.CellSize_Height)))
                {
                    vehicle.CurrentCellSpeed = functions.SpeedBumpB20(vehicle.Properties.Type);
                    vehicle.bumpB20 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                }

                //Consider it as on bump
                if (vehicle.CurrentPosition.Row > CaftSettings.Default.BumpLine
                 && vehicle.CurrentPosition.Row < CaftSettings.Default.BumpLine + ((int)Math.Ceiling(3 / CaftSettings.Default.CellSize_Height)))
                {
                    vehicle.CurrentCellSpeed = functions.SpeedOnBump(vehicle.Properties.Type);
                    vehicle.OnBump = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                }

                if (vehicle.CurrentPosition.Row < CaftSettings.Default.BumpLine
                 && vehicle.CurrentPosition.Row > globalVariables.bumpA20)
                {
                    vehicle.CurrentCellSpeed = functions.SpeedBumpA20(vehicle.Properties.Type);
                    vehicle.bumpA20 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                }

                if (vehicle.CurrentPosition.Row < globalVariables.bumpA20
                 && vehicle.CurrentPosition.Row > globalVariables.bumpA40)
                {
                    vehicle.CurrentCellSpeed = functions.SpeedBumpA40(vehicle.Properties.Type);
                    vehicle.bumpA40 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                }

                if (vehicle.CurrentPosition.Row < globalVariables.bumpA40
                    && vehicle.CurrentPosition.Row > globalVariables.bumpA60)
                {
                    vehicle.CurrentCellSpeed = functions.SpeedBumpA60(vehicle.Properties.Type);
                    vehicle.bumpA60 = ((((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000);
                }

            }
            catch (Exception)
            {
                //Nothing to do for speed, keep speed as it is.
            }
        }

        private bool ProcessPedestrianSignal(Vehicle vehicle)
        {
            if (vehicle.CurrentPosition.Row >= CaftSettings.Default.SignalLinePed)
            {

                if (IsMidBlockRed)
                {
                    if (vehicle.CurrentPosition.Row < CaftSettings.Default.SignalLinePed + 60
                        && vehicle.CurrentPosition.Row > CaftSettings.Default.SignalLinePed + 40)
                    {
                        vehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(35, 40));
                    }

                    if (vehicle.CurrentPosition.Row < CaftSettings.Default.SignalLinePed + 40
                     && vehicle.CurrentPosition.Row > CaftSettings.Default.SignalLinePed + 20)
                    {
                        vehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(20, 35));
                    }

                    if (vehicle.CurrentPosition.Row < CaftSettings.Default.SignalLinePed + 20)
                    {
                        vehicle.CurrentCellSpeed = functions.KmToCell(globalVariables.RndmNo.Next(10, 20));
                    }

                    if ((bool)CaftSettings.Default.signalIncludePed)
                    {
                        if (vehicle.CurrentPosition.Row < CaftSettings.Default.SignalLinePed + 10)
                        {
                            vehicle.CurrentCellSpeed = 2;
                        }
                        if (vehicle.CurrentPosition.Row < CaftSettings.Default.SignalLinePed + 5)
                        {
                            vehicle.CurrentCellSpeed = 1;
                        }
                        if (vehicle.CurrentPosition.Row == CaftSettings.Default.SignalLinePed + 1)
                        {
                            vehicle.CurrentCellSpeed = 0;
                            vehicle.Properties.Status = VehicleStatus.AtMidSignal;
                            return true;
                        }
                    }

                }
            }
            return false;
        }

       
        private bool CheckWhetherSignal(Vehicle vehicle, int _line, bool signalToCheck)
        {
            // Vehicle Speed should start decreasing as it reaches near Bump (10% of total Grid count)
            //decimal CellArndBump = Math.Round(Convert.ToDecimal(globalVariables.TotalGridRowCount) / Convert.ToDecimal(10), 0);
            var line = _line;
            if (!signalToCheck)
            {
                if (vehicle.CurrentPosition.Row >= line
                    && vehicle.CurrentPosition.Row - vehicle.CurrentCellSpeed < line + 30)
                {
                    if (vehicle.CurrentPosition.Row > line)
                    {
                        vehicle.CurrentCellSpeed = vehicle.CurrentPosition.Row - line;
                    REDUCESPEED4:
                        if (CheckNextCell(vehicle))
                        {
                            //vehicle.Properties.Status = VehicleStatus.InProgress;
                            MoveVehicle(vehicle);

                        }
                        else
                        {
                            if (vehicle.CurrentPosition.Row > line && vehicle.CurrentCellSpeed > 1)
                            {
                                vehicle.CurrentCellSpeed -= 1;
                                goto REDUCESPEED4;
                            }
                        }
                        return true;
                    }
                    else if (vehicle.CurrentPosition.Row == line)
                    {
                        return true;
                    }
                }
                else
                {
                    //if (vehicle.CurrentPosition.Row < CaftSettings.Default.BumpLine + 80)
                    //{
                    //    if (vehicle.CurrentCellSpeed < 10) vehicle.CurrentCellSpeed = globalVariables.RndmNo.Next(15, 20);
                    //}
                }
            }
            else
            {
                //Vehicle Bunch going at slow speed after signal on...
                //logic to speed up vehicle and move it fast
                if (CaftSettings.Default.signalInclude || CaftSettings.Default.signalIncludePed)
                {
                    if (vehicle.CurrentPosition.Row < line
                        && vehicle.CurrentPosition.Row > line - (globalVariables.TwoLaneColumnCount * 2)
                        && CheckNextCell(vehicle))
                    {
                        if (CanAccelerate(vehicle))
                        {
                            //vehicle.CurrentCellSpeed += 1;
                            // Speed of the vehicle is increased after acceleration
                            DoAcceleration(vehicle);
                            MoveVehicle(vehicle);
                            return true;
                        }
                    }
                }

            }

            return false;
        }


        #endregion

        #region Functions

        private void CalculateInterSectionDelay(Vehicle vehicle)
        {
            if (vehicle.Direction == DirectionType.Left)
            {
                //vehicle.StartTimeIntersection = 9999;
                //vehicle.EndTimeIntersection = 9999;
                return;
            }

            int startLine = CaftSettings.Default.DelayDistance;

            int cellStart = globalVariables.RowIntersection + (int)(startLine / CaftSettings.Default.CellSize_Height);
            int cellEnd = globalVariables.RowIntersection - (globalVariables.TwoLaneColumnCount * 1);

            if (vehicle.CurrentPosition.Row < cellStart
                && vehicle.StartTimeIntersection == 0)
            {
                vehicle.StartTimeIntersection = globalVariables.TickCount;
            }

            if (vehicle.CurrentPosition.Row < cellEnd
                && vehicle.EndTimeIntersection == 0)
            {
                vehicle.EndTimeIntersection = globalVariables.TickCount;
            }
        }



        private void ProcessMidSignal()
        {
            if ((bool)CaftSettings.Default.signalIncludePed)
            {
                if (SignalTick > CaftSettings.Default.SGreenSignalPed + CaftSettings.Default.SRedSignalPed + CaftSettings.Default.SAmberSignalPed)
                {
                    //Reset SignalTick
                    SignalTick = 1;
                    var vhlist = globalVariables.VehicleList.Where(p => (p.Properties.Status == VehicleStatus.InProgress
                                        || p.Properties.Status == VehicleStatus.AtMidSignal)
                                        && p.CurrentCellSpeed == 1).OrderByDescending(p => p.CurrentPosition.Row).FirstOrDefault();

                    if (vhlist != null)
                    {
                        globalVariables.QueueList.Add(globalVariables.TickCount, (int)((vhlist.CurrentPosition.Row - CaftSettings.Default.BumpLine) * CaftSettings.Default.CellSize_Height));
                    }
                    else
                    {
                        globalVariables.QueueList.Add(globalVariables.TickCount, 0);
                    }
                }

                if (SignalTick <= CaftSettings.Default.SGreenSignalPed + CaftSettings.Default.SAmberSignalPed)
                {
                    //Green Mid Block Signal
                    IsMidBlockRed = false;
                }
                else if (SignalTick > CaftSettings.Default.SGreenSignalPed + CaftSettings.Default.SAmberSignalPed
                    && SignalTick <= CaftSettings.Default.SGreenSignalPed + CaftSettings.Default.SAmberSignalPed + CaftSettings.Default.SRedSignalPed)
                {
                    //Red Mid Block Signal
                    IsMidBlockRed = true;
                }

                SignalTick++;
            }
            else
            {

            }
        }

        private bool CanAccelerate(Vehicle vehicle)
        {
            //return false;
            if (vehicle.CurrentCellSpeed >= vehicle.Properties.MaxCellSpeed)
            {
                return false;
            }

            //Check if vehicle is not there in next 10 cells then increase the speed.n
            bool canAcc = true;

            for (int i = Convert.ToInt16(CaftSettings.Default.Vh_AccelerationGap); i > 1; i--)
            {
                if (GetCellValue(vehicle.CurrentPosition.Row - i, vehicle.CurrentPosition.Column))
                {
                    return false;
                }


                if (vehicle.Properties.Type != VehicleType.TwoWheel)
                {
                    if (GetCellValue(vehicle.CurrentPosition.Row - i, vehicle.CurrentPosition.Column + 1))
                    {
                        return false;
                    }
                }
            }

            return canAcc;
        }

        private void DoAcceleration(Vehicle vehicle)
        {
            switch (vehicle.Properties.Type)
            {
                case VehicleType.TwoWheel:
                    vehicle.CurrentCellSpeed += CaftSettings.Default.TwoVhAccParam;
                    break;
                case VehicleType.ThreeWheel:
                    vehicle.CurrentCellSpeed += CaftSettings.Default.ThreeVhAccParam;
                    break;
                case VehicleType.FourWheel:
                    vehicle.CurrentCellSpeed += CaftSettings.Default.FourVhAccParam;
                    break;
                case VehicleType.LCV1:
                    vehicle.CurrentCellSpeed += CaftSettings.Default.LCV1VhAccParam;
                    break;
                case VehicleType.LCV2:
                    vehicle.CurrentCellSpeed += CaftSettings.Default.LCV2VhAccParam;
                    break;
                case VehicleType.HCV1:
                    vehicle.CurrentCellSpeed += CaftSettings.Default.HCV1VhAccParam;
                    break;
                case VehicleType.HCV2:
                    vehicle.CurrentCellSpeed += CaftSettings.Default.HCV2VhAccParam;
                    break;
                default:
                    break;
            }

            if (vehicle.CurrentCellSpeed > vehicle.Properties.MaxCellSpeed)
            {
                vehicle.CurrentCellSpeed = vehicle.Properties.MaxCellSpeed;
            }
        }



        /// <summary>
        /// Check next cell to decide whether to move ahead or stop
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        private bool CheckNextCell(Vehicle vehicle, bool isDirectionChanged = false)
        {

            int curLane = vehicle.CurrentPosition.Column;
            int curRow = vehicle.CurrentPosition.Row;
            int MaxCol = vehicle.Properties.ColumnOccupancy;
            int MaxRow = vehicle.Properties.RowOccupancy;

            bool readyToMove = true;

            if (!isDirectionChanged)
            {
                //Last Change 19112015 vehicle.CurrentCellSpeed + 1
                for (int i = vehicle.CurrentCellSpeed; i > 0; i--)
                {
                    for (int j = 0; j < MaxCol; j++)
                    {
                        //Last Change 19112015 (i == 2)
                        if (i == 1)
                        {
                            if (GetCellValue(curRow - 2, curLane + j)
                                    && !globalVariables.LaneSignalOn
                                    && vehicle.Properties.Status == VehicleStatus.InProgress)
                            {
                                //vehicle.IsStoppedForSignal += 1;
                            }
                        }

                        if (GetCellValue(curRow - i, curLane + j))
                        {
                            return false;
                        }
                    }

                }
            }
            else
            {
                // Logic for Vehicle whose direction needs to be changed

                if (vehicle.Properties.DirectionStatus == VehicleDirectionChange.NeedToChange)
                {

                    //int MaxLimit = globalVariables.RowIntersection / 2;

                    int ExtraCells = globalVariables.MaxColumn - globalVariables.MinColumn + 1;
                    int MinLimit = globalVariables.RowIntersection;
                    int MaxLimit = globalVariables.RowIntersection - (ExtraCells * 2);



                    if (vehicle.Direction == DirectionType.Left)
                    {
                        vehicle.CurrentCellSpeed = functions.RandomNumberGenerator(1, ExtraCells - MaxCol);
                    }
                    else
                    {
                        vehicle.CurrentCellSpeed = functions.RandomNumberGenerator(ExtraCells + 1, ExtraCells * 2 - MaxCol);
                    }

                    //vehicle.CurrentCellSpeed = 1;

                    // Left direction 
                    if (vehicle.Direction == DirectionType.Left)
                    {
                        for (int i = MaxRow; i > 0; i--)
                        {
                            for (int j = 0; j < MaxCol; j++)
                            {
                                if (GetCellValue(curRow - j - vehicle.CurrentCellSpeed, curLane - i - 1))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Right Direction
                        for (int i = MaxRow; i > 0; i--)
                        {
                            for (int j = 0; j < MaxCol; j++)
                            {
                                if (GetCellValue(curRow - j - vehicle.CurrentCellSpeed, curLane + i))
                                {
                                    return false;
                                }
                            }
                        }

                    }
                }
                else
                {
                    // For Vehicle whose direction is already changed
                    if (vehicle.Direction == DirectionType.Left)
                    {
                        for (int i = vehicle.CurrentCellSpeed + MaxRow; i > 0; i--)
                        {
                            for (int j = 0; j < MaxCol; j++)
                            {
                                if (GetCellValue(curRow + j, curLane - i))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Right Hand side
                        for (int i = vehicle.CurrentCellSpeed + MaxRow; i > 0; i--)
                        {
                            for (int j = 0; j < MaxCol; j++)
                            {
                                if (GetCellValue(curRow + j, curLane + i))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

            }

            return readyToMove;
        }

        private bool CheckLaneOvertake(Vehicle vehicle, bool signalToCheck, int signalLine)
        {
            //if (!globalVariables.LaneSignalOnFirst && vehicle.CurrentPosition.Row > CaftSettings.Default.BumpLine)
            //{
            //    return true;
            //}

            int prevlane = vehicle.CurrentPosition.Column;
            int curLane = vehicle.CurrentPosition.Column;
            int curRow = vehicle.CurrentPosition.Row;
            int VhColOccupancy = vehicle.Properties.ColumnOccupancy;
            int VhRowOccupancy = vehicle.Properties.RowOccupancy;
            bool readyToMove = false;
            bool isovertakefromlefthandside = false;
            bool isovertakefromrighthandside = false;


            // Check Right hand side overtake
            if (curLane + VhColOccupancy <= globalVariables.MaxColumn - 1)
            {
                // Checking columns on the right hand side 
                for (int c = curLane; c < globalVariables.MaxColumn - VhColOccupancy + 1; c++)
                {
                    if (CheckCellsRightHandSide(vehicle, c))
                    {
                        readyToMove = true;
                        curLane = c;

                        //Checking Current Gap
                        for (int r = 1; r <= VhColOccupancy; r++)
                        {
                            for (int k = 0; k < VhRowOccupancy; k++)
                            {
                                if (GetCellValue(curRow - k, (c + VhColOccupancy - 1) + r))
                                {
                                    return false;
                                }
                            }
                        }

                        if (readyToMove)
                        {
                            isovertakefromrighthandside = true;
                        }
                        break;
                    }
                    else
                    {
                        readyToMove = false;
                    }

                }
            }
            // Check Left hand side overtake
            if (readyToMove == false && curLane - VhColOccupancy + 1 >= 1)
            {
                // Checking columns on the right hand side 
                for (int c = curLane - 1; c >= globalVariables.MinColumn; c--)
                {
                    if (CheckCellsLeftHandSide(vehicle, c))
                    {
                        readyToMove = true;
                        curLane = c;

                        int differenceinlane = vehicle.CurrentPosition.Column - c;
                        //Checking Current Gap
                        for (int r = 0; r < differenceinlane; r++)
                        {
                            //if (c - r == 0)
                            //{
                            //    readyToMove = false;
                            //    break;
                            //}

                            for (int k = 0; k < VhRowOccupancy; k++)
                            {
                                if (c != vehicle.CurrentPosition.Column)
                                {
                                    if (GetCellValue(curRow - k, c + r))
                                    {
                                        readyToMove = false;
                                        return false;

                                    }
                                }
                            }
                        }


                        if (readyToMove)
                        {
                            isovertakefromlefthandside = true;
                        }
                        break;
                    }
                    else
                    {
                        readyToMove = false;
                    }

                }
            }


            // Preventing Overtake for Vehicles whose Lane direction is different
            if (vehicle.Direction != DirectionType.Straight)
            {
                int speeddiff = vehicle.CurrentPosition.Row - vehicle.CurrentCellSpeed; ;

                if (speeddiff < globalVariables.RowIntersection)
                {
                    readyToMove = false;
                }
            }


            if (readyToMove)
            {

                // Disabling current Cell
                for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
                {
                    for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                    {
                        SetCellValue(i + vehicle.CurrentPosition.Row, j + vehicle.CurrentPosition.Column, vehicle, false);
                    }
                }

                // Changing Vehicle Lane

                vehicle.CurrentPosition.Column = curLane;
                vehicle.CurrentPosition.Row = curRow;

                vehicle.CurrentPosition.Row -= vehicle.CurrentCellSpeed;

                //bool noNeedToIncreaseSpeed = false;

                if (!signalToCheck
                    && vehicle.CurrentPosition.Row <= signalLine
                    && curRow > signalLine)
                {
                    /* First Signal is ON and vehicle is before the signal line
                     but due to overtaking logic, it is now going beyond the signal
                     which is wrong, so making the speed of vehicle to 1 which will
                     cause the vehicle to slow down as well as next loop will take care
                     of everything else. */
                    vehicle.CurrentPosition.Row = curRow - 1;
                    //noNeedToIncreaseSpeed = true;
                }

                if (isovertakefromrighthandside)
                {
                    vehicle.CurrentPosition.Column += 1;

                    // Enabling Overtaking Cell
                    for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
                    {
                        for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                        {
                            SetCellValue(vehicle.CurrentPosition.Row + i, vehicle.CurrentPosition.Column + j, vehicle, true);
                        }
                    }
                }
                else if (isovertakefromlefthandside)
                {
                    //if (vehicle.CurrentPosition.Column > 1)
                    //{
                    //    vehicle.CurrentPosition.Column -= 1;
                    //}

                    // Enabling Overtaking Cell
                    for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
                    {
                        for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                        {
                            //if (vehicle.CurrentPosition.Column != 1)
                            //{
                            //    SetCellValue(vehicle.CurrentPosition.Row + i, vehicle.CurrentPosition.Column - j + 1, vehicle, true);
                            //}
                            //else
                            //{
                            SetCellValue(vehicle.CurrentPosition.Row + i, vehicle.CurrentPosition.Column + j, vehicle, true);
                            //}

                        }
                    }
                }


                // Increase speed of the vehicle
                if ((vehicle.CurrentCellSpeed + 2) < vehicle.Properties.MaxCellSpeed)// && !noNeedToIncreaseSpeed)
                {
                    vehicle.CurrentCellSpeed += 1;
                }

                //for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
                //{
                //    for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                //    {
                //        SetCellValue(i + vehicle.CurrentPosition.Row, j + vehicle.CurrentPosition.Column, vehicle, true);
                //    }
                //}

                //for (int i = 0; i <= vehicle.CurrentCellSpeed; i++)
                //{
                //    for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                //    {
                //        SetCellValue(vehicle.CurrentPosition.Row + vehicle.Properties.RowOccupancy + i, vehicle.CurrentPosition.Column - 1 - j, vehicle, false);
                //    }
                //}
            }

            if (readyToMove && IsLaneChanged(vehicle, prevlane))
            {
                vehicle.Properties.VehicleLaneChanged++;
                //MessageBox.Show("Vehicle Lane Changed");
            }

            return readyToMove;

            #region Old Code

            //if (vehicle.Properties.Type != VehicleType.TwoWheel && curLane > 1)
            //{
            //    return false;
            //}
            //else if (curLane == 3)
            //{
            //    return false;
            //}

            // readyToMove = true;
            //for (int i = Convert.ToInt16(CaftSettings.Default.Vh_OvertakingGap); i > 0; i--)
            //{
            //    if (vehicle.Properties.Type != VehicleType.TwoWheel)
            //    {
            //        if (curLane < 2)
            //        {
            //            if (vehicle.Properties.Type != VehicleType.TwoWheel)
            //            {
            //                if (GetCellValue(curRow - i, curLane + 2))
            //                {
            //                    readyToMove = false;
            //                    break;
            //                }
            //            }

            //            if (vehicle.Properties.Type != VehicleType.TwoWheel)
            //            {
            //                if (GetCellValue(curRow - i, curLane + 3))
            //                {
            //                    readyToMove = false;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (GetCellValue(curRow - i, curLane + 1))
            //        {
            //            readyToMove = false;
            //            break;
            //        }
            //    }
            //}

            //if (readyToMove)
            //{
            //    if (vehicle.Properties.Type != VehicleType.TwoWheel)
            //    {
            //        vehicle.CurrentPosition.Column += 2;
            //    }
            //    else
            //    {
            //        vehicle.CurrentPosition.Column += 1;
            //    }

            //    if (vehicle.Properties.MaxCellSpeed != vehicle.CurrentCellSpeed)
            //    {
            //        vehicle.CurrentCellSpeed += 1;
            //    }
            //}
            //return readyToMove;

            #endregion
        }

        private bool IsLaneChanged(Vehicle vehicle, int prevlane)
        {
            int Columns = globalVariables.MaxColumn - globalVariables.MinColumn;
            int curcolumn = vehicle.CurrentPosition.Column;
            int prevvehiclelane, curvehiclelane;
            prevvehiclelane = curvehiclelane = 0;

            if (prevlane != curcolumn)
            {

                #region Deciding previous Lane
                if (prevlane >= globalVariables.MinColumn && prevlane < globalVariables.MinColumn + (Columns / 2))
                {
                    prevvehiclelane = 1;
                }
                else if (prevlane >= globalVariables.MinColumn + (Columns / 2) && prevlane < globalVariables.MaxColumn)
                {
                    prevvehiclelane = 2;
                }
                #endregion

                #region Deciding Current Lane
                if (curcolumn >= globalVariables.MinColumn && curcolumn < globalVariables.MinColumn + (Columns / 2))
                {
                    curvehiclelane = 1;
                }
                else if (curcolumn > globalVariables.MinColumn + (Columns / 2) && prevlane < globalVariables.MaxColumn)
                {
                    curvehiclelane = 2;
                }
                #endregion

                if (prevvehiclelane != curvehiclelane)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckCellsRightHandSide(Vehicle objvehicle, int c)
        {
            // Checking Overtaking Gap
            for (int i = Convert.ToInt16(CaftSettings.Default.Vh_OvertakingGap) + objvehicle.CurrentCellSpeed; i > 0; i--)
            {
                for (int j = 1; j <= objvehicle.Properties.ColumnOccupancy; j++)
                {
                    if (c + j >= globalVariables.MaxColumn)
                    {
                        return false;
                    }

                    if (GetCellValue(objvehicle.CurrentPosition.Row - i, c + j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckCellsLeftHandSide(Vehicle objvehicle, int c)
        {
            // Checking Overtaking Gap
            for (int i = Convert.ToInt16(CaftSettings.Default.Vh_OvertakingGap) + objvehicle.CurrentCellSpeed; i > 0; i--)
            {
                for (int j = 0; j < objvehicle.Properties.ColumnOccupancy; j++)
                {
                    //if (c - j < 1)
                    //{
                    //    return false;
                    //}

                    if (GetCellValue(objvehicle.CurrentPosition.Row - i, c + j))
                    {
                        return false;
                    }
                }
            }



            return true;
        }

        /// <summary>
        /// Main Function to set the Grid Postions of Vehicle
        /// </summary>
        /// <param name="vehicle">Pass current Vehicle</param>
        private Vehicle MoveVehicle(Vehicle vehicle)
        {
            vehicle.CurrentPosition.Row -= vehicle.CurrentCellSpeed;

            //Update full vehicle as per the current cell speed.
            UpdateVehiclePosition(vehicle);

            return vehicle;
        }

        private void UpdateVehiclePosition(Vehicle vehicle)
        {
            //SetCellValue(vehicle.CurrentPosition.Row, vehicle.CurrentPosition.Column, vehicle, true);

            for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
            {
                for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                {
                    SetCellValue(i + vehicle.CurrentPosition.Row, j + vehicle.CurrentPosition.Column, vehicle, true);
                }
            }

            for (int i = 0; i < vehicle.CurrentCellSpeed; i++)
            {
                for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                {
                    SetCellValue(vehicle.CurrentPosition.Row + vehicle.Properties.RowOccupancy + i, j + vehicle.CurrentPosition.Column, vehicle, false);
                }
            }
        }

        private void UpdateVehiclePosition_ForOvertaking(Vehicle vehicle)
        {
            vehicle.CurrentPosition.Row -= vehicle.CurrentCellSpeed;

            for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
            {
                for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                {
                    SetCellValue(i + vehicle.CurrentPosition.Row, j + vehicle.CurrentPosition.Column, vehicle, true);
                }
            }

            for (int i = 0; i <= vehicle.CurrentCellSpeed; i++)
            {
                for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                {
                    SetCellValue(vehicle.CurrentPosition.Row + vehicle.Properties.RowOccupancy + i, vehicle.CurrentPosition.Column - 1 - j, vehicle, false);
                }
            }
        }

        private void UpdateVehiclePosition_DirectionChange(Vehicle vehicle)
        {
            //SetCellValue(vehicle.CurrentPosition.Row, vehicle.CurrentPosition.Column, vehicle, true);

            int curLane = vehicle.CurrentPosition.Column;
            int curRow = vehicle.CurrentPosition.Row;
            int MaxCol = vehicle.Properties.ColumnOccupancy;
            int MaxRow = vehicle.Properties.RowOccupancy;


            //vehicle.CurrentCellSpeed

            // Logic for shifting Vehicles for the firstime 
            // Updating Vehicle Status from NeedToChange to Changed
            if (vehicle.Properties.DirectionStatus == VehicleDirectionChange.NeedToChange)
            {
                if (vehicle.Direction == DirectionType.Left)
                {
                    for (int i = MaxRow; i > 0; i--)
                    {
                        for (int j = 0; j < MaxCol; j++)
                        {
                            SetCellValue(curRow - j - vehicle.CurrentCellSpeed, curLane - i, vehicle, true);
                        }
                    }


                    // disabling Current Vehicle - Wil be called only for the first time
                    for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
                    {
                        for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                        {
                            SetCellValue(vehicle.CurrentPosition.Row + i, j + vehicle.CurrentPosition.Column, vehicle, false);
                        }
                    }

                    vehicle.CurrentPosition.Row -= (MaxCol + vehicle.CurrentCellSpeed - 1);
                    vehicle.CurrentPosition.Column -= MaxRow;
                }
                else
                {
                    // Logic for shifting vehicles on right hand side
                    for (int i = MaxRow; i > 0; i--)
                    {
                        for (int j = 0; j < MaxCol; j++)
                        {
                            SetCellValue(curRow - j - vehicle.CurrentCellSpeed, curLane + i, vehicle, true);
                        }
                    }

                    // disabling Current Vehicle - Wil be called only for the first time
                    for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
                    {
                        for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                        {
                            SetCellValue(vehicle.CurrentPosition.Row + i, j + vehicle.CurrentPosition.Column, vehicle, false);
                        }
                    }

                    vehicle.CurrentPosition.Row -= (MaxCol + vehicle.CurrentCellSpeed - 1);
                    vehicle.CurrentPosition.Column += MaxRow;
                }

                vehicle.Properties.DirectionStatus = VehicleDirectionChange.Changed;

            }
            else
            {
                //MessageBox.Show("Changing Vehicle Direction for second time");

                if (vehicle.Direction == DirectionType.Left)
                {
                    for (int i = 0; i < MaxRow; i++)
                    {
                        for (int j = 0; j < MaxCol; j++)
                        {
                            SetCellValue(curRow + j, curLane + i - MaxRow - vehicle.CurrentCellSpeed, vehicle, true);
                        }
                    }

                    // disabling Current Vehicle - Wil be called only for the first time
                    for (int i = 0; i <= MaxRow; i++)
                    {
                        for (int j = 0; j < MaxCol; j++)
                        {
                            SetCellValue(curRow + j, curLane + i, vehicle, false);
                        }
                    }

                    vehicle.CurrentPosition.Column -= MaxRow + vehicle.CurrentCellSpeed;
                }
                else
                {
                    // Logic for shifting vehicles on right hand side
                    for (int i = 0; i < MaxRow; i++)
                    {
                        for (int j = 0; j < MaxCol; j++)
                        {
                            SetCellValue(curRow + j, curLane + i + vehicle.CurrentCellSpeed, vehicle, true);
                        }
                    }

                    // disabling Current Vehicle position
                    for (int i = 0; i < MaxRow; i++)
                    {
                        for (int j = 0; j < MaxCol; j++)
                        {
                            SetCellValue(curRow + j, curLane - i, vehicle, false);
                        }
                    }

                    vehicle.CurrentPosition.Column += (vehicle.CurrentCellSpeed + MaxRow - 1);
                }

                // Changing Vehicle movement for vehicles whose direction has already been changed
            }



            // disabling Current Vehicle position
        }

        private bool GetCellValue(int row, int column)
        {
            var border = MainGrid.Children.OfType<Border>().Where(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column).FirstOrDefault();

            if (border != null)
            {
                var rectangle = border.Child as System.Windows.Shapes.Rectangle;

                if (rectangle.Fill != null)
                {
                    System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(rectangle.Fill.ToString());

                    if (!color.Name.Contains("Transparent") && !color.Name.Contains("DimGray"))
                    {
                        if (!color.IsEmpty)
                        {
                            return true;
                        }
                    }
                }

                //if (rectangle != null
                //    && rectangle.GetValue(System.Windows.Shapes.Rectangle.FillProperty) == new SolidColorBrush(Colors.Black))
                //    return true;
            }
            return false;
        }

        private void SetCellValue(int row, int column, Vehicle vhcle, bool on)
        {
            var border = MainGrid.Children.OfType<Border>().Where(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column).FirstOrDefault();
            if (border != null)
            {
                var rectangle = border.Child as System.Windows.Shapes.Rectangle;
                if (rectangle != null)
                {
                    if (on)
                    {
                        AssignColorToVehicle(vhcle, rectangle);
                    }
                    else
                    {
                        rectangle.Fill = new SolidColorBrush(Colors.Transparent);
                    }
                }

            }


        }



        private Vehicle GetVehicleFromGrid(int row, int column)
        {
            var vehicle = globalVariables.VehicleList.Where(p => p.CurrentPosition.Row == row
                && p.CurrentPosition.Column == column).FirstOrDefault();

            return vehicle;
        }

        public int GetTotalONCells()
        {
            int totalONCells = 0;

            try
            {
                for (int i = 0; i < CaftSettings.Default.GridRowCount; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (GetCellValue(i, j))
                        {
                            totalONCells++;
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return totalONCells;
        }

        #region Functions for Top-Bottom Traffic
        private bool CheckNextCellTop(Vehicle vehicle)
        {

            int curLane = vehicle.CurrentPosition.Column;
            int curRow = vehicle.CurrentPosition.Row;
            int MaxCol = vehicle.Properties.ColumnOccupancy;
            int MaxRow = vehicle.Properties.RowOccupancy;

            bool readyToMove = true;

            for (int i = 1; i <= vehicle.CurrentCellSpeed + 1; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    if (i == vehicle.CurrentCellSpeed + 1)
                    {
                        if (GetCellValue(curRow + i + 1, curLane + j)
                            && !globalVariables.LaneSignalOn_Top)
                        {
                            vehicle.IsStoppedForSignal += 1;
                        }
                    }

                    if (GetCellValue(curRow + i, curLane + j))
                    {
                        return false;
                    }
                }

            }


            return readyToMove;
        }

        private bool CheckNextCellLeft(Vehicle vehicle)
        {

            int curLane = vehicle.CurrentPosition.Column;
            int curRow = vehicle.CurrentPosition.Row;
            int MaxCol = vehicle.Properties.ColumnOccupancy;
            int MaxRow = vehicle.Properties.RowOccupancy;

            bool readyToMove = true;


            for (int i = 1; i <= vehicle.CurrentCellSpeed + 1; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    if (i == 1)
                    {
                        if (GetCellValue(curRow + j, curLane - i - 1)
                            && !globalVariables.LaneSignalOn_Right)
                        {
                            vehicle.IsStoppedForSignal += 1;
                        }
                    }

                    if (GetCellValue(curRow + j, curLane - i))
                    {
                        return false;
                    }
                }
            }

            return readyToMove;
        }

        private bool CheckNextCellRight(Vehicle vehicle)
        {

            int curLane = vehicle.CurrentPosition.Column;
            int curRow = vehicle.CurrentPosition.Row;
            int MaxCol = vehicle.Properties.ColumnOccupancy;
            int MaxRow = vehicle.Properties.RowOccupancy;

            bool readyToMove = true;


            for (int i = 1; i <= vehicle.CurrentCellSpeed + 1; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    if (i == vehicle.CurrentCellSpeed + 1)
                    {
                        if (GetCellValue(curRow + j, vehicle.CurrentCellSpeed + 2)
                            && !globalVariables.LaneSignalOn_Left)
                        {
                            vehicle.IsStoppedForSignal += 1;
                        }
                    }

                    if (GetCellValue(curRow + j, curLane + i))
                    {
                        return false;
                    }
                }
            }

            return readyToMove;
        }

        private Vehicle MoveVehicleTop(Vehicle vehicle)
        {
            //Svehicle.CurrentPosition.Row += vehicle.CurrentCellSpeed;

            //Update full vehicle as per the current cell speed.
            //UpdateVehiclePositionTop(vehicle);

            return vehicle;
        }

        private void UpdateVehiclePositionTop(Vehicle vehicle)
        {
            int prevRow = vehicle.CurrentPosition.Row;

            vehicle.CurrentPosition.Row += vehicle.CurrentCellSpeed;

            for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
            {
                for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                {
                    SetCellValue(vehicle.CurrentPosition.Row - i, j + vehicle.CurrentPosition.Column, vehicle, true);
                }
            }

            for (int i = 0; i <= vehicle.CurrentCellSpeed; i++)
            {
                for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                {
                    SetCellValue(prevRow - vehicle.Properties.RowOccupancy + i, j + vehicle.CurrentPosition.Column, vehicle, false);
                }
            }
        }

        private void UpdateVehiclePosition_Right(Vehicle vehicle)
        {
            int curLane = vehicle.CurrentPosition.Column;
            int curRow = vehicle.CurrentPosition.Row;
            int MaxCol = vehicle.Properties.ColumnOccupancy;
            int MaxRow = vehicle.Properties.RowOccupancy;

            for (int i = 0; i < MaxRow; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    SetCellValue(curRow + j, curLane + i - vehicle.CurrentCellSpeed, vehicle, true);
                }
            }

            // disabling Current Vehicle - Wil be called only for the first time
            for (int i = 0; i < MaxRow; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    SetCellValue(curRow + j, curLane + i, vehicle, false);
                }
            }

            vehicle.CurrentPosition.Column -= vehicle.CurrentCellSpeed;


            // old logic
            //for (int i = 0; i < MaxRow; i++)
            //{
            //    for (int j = 0; j < MaxCol; j++)
            //    {
            //        SetCellValue(curRow + j, curLane + i - MaxRow - vehicle.CurrentCellSpeed, vehicle, true);
            //    }
            //}

            //// disabling Current Vehicle - Wil be called only for the first time
            //for (int i = 0; i <= MaxRow; i++)
            //{
            //    for (int j = 0; j < MaxCol; j++)
            //    {
            //        SetCellValue(curRow + j, curLane + i, vehicle, false);
            //    }
            //}

            //vehicle.CurrentPosition.Column -= MaxRow + vehicle.CurrentCellSpeed;
        }

        private void UpdateVehiclePosition_Left(Vehicle vehicle)
        {
            int curLane = vehicle.CurrentPosition.Column;
            int curRow = vehicle.CurrentPosition.Row;
            int MaxCol = vehicle.Properties.ColumnOccupancy;
            int MaxRow = vehicle.Properties.RowOccupancy;

            for (int i = 0; i < MaxRow; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    SetCellValue(curRow + j, curLane - i + vehicle.CurrentCellSpeed, vehicle, true);
                }
            }

            // disabling Current Vehicle - Wil be called only for the first time
            for (int i = 0; i < MaxRow; i++)
            {
                for (int j = 0; j < MaxCol; j++)
                {
                    SetCellValue(curRow + j, curLane - i, vehicle, false);
                }
            }

            vehicle.CurrentPosition.Column += vehicle.CurrentCellSpeed;
        }




        private void VehicleToRemove(Vehicle vehicle)
        {
            for (int i = 0; i < vehicle.Properties.RowOccupancy; i++)
            {
                for (int j = 0; j < vehicle.Properties.ColumnOccupancy; j++)
                {
                    //True the current position
                    SetCellValue(i + vehicle.CurrentPosition.Row, j + vehicle.CurrentPosition.Column, vehicle, false);
                }
            }
        }
        #endregion

        #endregion

        #region Logic for generating Graphs


        private void CalculateIntersectionHeadway(Vehicle vehicle)
        {
            // Setting Headway
            if (vehicle.CurrentPosition.Row < globalVariables.RowIntersection && vehicle.Vehicle_Headway_Intersection == 0)
            {
                vehicle.Vehicle_Headway_Intersection = globalVariables.TickCount;
            }
        }

        private void CalculateSaturationBottom(Vehicle vehicle)
        {
            if (vehicle.SaturationTime == 0
                && vehicle.CurrentPosition.Row < globalVariables.RowIntersection)
            {
                vehicle.SaturationTime = globalVariables.TickCount;
            }
        }

        private void CalculateSaturationLeft(Vehicle vehicle)
        {
            if (vehicle.SaturationTime == 0
                && vehicle.CurrentPosition.Column > globalVariables.ExtraRoadCellsLR)
            {
                vehicle.SaturationTime = globalVariables.TickCount;
            }
        }

        private void CalculateSaturationTop(Vehicle vehicle)
        {
            if (vehicle.SaturationTime == 0
                && vehicle.CurrentPosition.Row > globalVariables.ExtraRoadCellsS)
            {
                vehicle.SaturationTime = globalVariables.TickCount;
            }
        }

        private void CalculateSaturationRight(Vehicle vehicle)
        {
            if (vehicle.SaturationTime == 0
                && vehicle.CurrentPosition.Column < globalVariables.ExtraRoadCellsS + (globalVariables.TwoLaneColumnCount * 2))
            {
                vehicle.SaturationTime = globalVariables.TickCount;
            }
        }

        private void CalculateNoiseProbFactor()
        {
            //if(globalVariables.NoiseProbFactors.ContainsKey(globalVariables.TickCount))
            //{
            //    var totol = globalVariables.VehicleList.Where(p => p.CurrentPosition.Row > globalVariables.RowIntersection - 1 && p.CurrentPosition.Row < globalVariables.TotalGridRowCount).Count(); //globalVariables.VehicleList.Where(p => p.Properties.DirectionStatus == VehicleDirectionChange.NoNeedToChange).Count();
            //    globalVariables.NoiseProbFactors[globalVariables.TickCount].TotalCount = totol;

            //}
            //else
            //{
            //    globalVariables.NoiseProbFactors.Add(globalVariables.TickCount, new NoiseProbabiltyFactorType() { NoisyCount = 0, TotalCount = 0 });
            //}
            //cntNoiseProbFactorVehicles = 0;
        }

        private void CalculateDensityPerSpeed()
        {
            var totalVh = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.InProgress
                || p.Properties.Status == VehicleStatus.AtMidSignal);
            if (totalVh != null)
            {
                double curAvgSpeed = globalVariables.VehicleList.Count() > 0 ? Convert.ToDouble(AvgSpeed / totalVh.Count()) : 0.0;
                globalVariables.DensityPerSpeed.Add(new DensityPerSpeed()
                {
                    Density = NoOfVehiclesPerKm,
                    Speed = curAvgSpeed
                });


            }
        }

        private void CalculateDensityPerTime()
        {
            globalVariables.DensityPerTime.Add(globalVariables.TickCount, NoOfVehiclesPerKm);

            if (CaftSettings.Default.bumpInclude || CaftSettings.Default.signalInclude)
            {
                //int temp = (int)(CaftSettings.Default.bumpArea / CaftSettings.Default.CellSize_Height);

                var beforeBump = globalVariables.VehicleList.Where(p => (p.Properties.Status == VehicleStatus.InProgress || p.Properties.Status == VehicleStatus.AtMidSignal)
                    && p.CurrentPosition.Row > CaftSettings.Default.BumpLine
                    && p.CurrentPosition.Row < ActualGridRowCount);

                var actualBeforeBump = (int)((1000 * (int)beforeBump.Count()) / ((ActualGridRowCount - CaftSettings.Default.BumpLine) * (CaftSettings.Default.CellSize_Height)));

                var afterBump = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.InProgress
                    && p.CurrentPosition.Row < CaftSettings.Default.BumpLine
                    && p.CurrentPosition.Row > ActualGridRowCount - CaftSettings.Default.GridRowCount);



                // Keval need to check this out throwing exception when grid rows are set to 50 and bump line to 80 
                int actualAfterBump = 0;
                try
                {
                    actualAfterBump = (int)((1000 * (int)afterBump.Count()) / ((CaftSettings.Default.BumpLine - (ActualGridRowCount - CaftSettings.Default.GridRowCount)) * CaftSettings.Default.CellSize_Height));
                }
                catch (Exception)
                {
                    actualAfterBump = 0;
                    //throw;
                }

                //Calculation SpeedPerDensity in DensityPerTime to reduce the time to calculate the same thing (i.e I have done above)
                var actualAvgSpeedBeforeBump = (double)((beforeBump.Sum(p => ((((double)CaftSettings.Default.CellSize_Height * p.CurrentCellSpeed) * 3600) / 1000))) / beforeBump.Count());
                var actualAvgSpeedAfterBump = (double)((afterBump.Sum(p => ((((double)CaftSettings.Default.CellSize_Height * p.CurrentCellSpeed) * 3600) / 1000))) / afterBump.Count());


                globalVariables.DensityPerTimeBeforeBump.Add(globalVariables.TickCount, actualBeforeBump);
                globalVariables.DensityPerTimeAfterBump.Add(globalVariables.TickCount, actualAfterBump);

                double tempA;
                if (double.TryParse(actualAvgSpeedBeforeBump.ToString(), out tempA))
                {
                    if (tempA >= 0 && !double.IsNaN(tempA))
                        globalVariables.DensityPerSpeedBeforeBump.Add(new DensityPerSpeed() { Density = beforeBump.Count(), Speed = actualAvgSpeedBeforeBump });
                }

                if (double.TryParse(actualAvgSpeedAfterBump.ToString(), out tempA))
                {
                    if (tempA >= 0 && !double.IsNaN(tempA))
                        globalVariables.DensityPerSpeedAfterBump.Add(new DensityPerSpeed() { Density = afterBump.Count(), Speed = actualAvgSpeedAfterBump });
                }
            }
        }

        private void PartialCalculateDensityPerTime(Vehicle vehicle)
        {
            //it is been set for mid block section
            if (vehicle.CurrentPosition.Row < globalVariables.TotalGridRowCount && vehicle.CurrentPosition.Row > (globalVariables.TotalGridRowCount - CaftSettings.Default.GridRowCount - 2))
            {
                NoOfVehiclesPerKm++;
            }
            else
            {
                if (NoOfVehiclesPerKm > 0) NoOfVehiclesPerKm--;
            }

        }


        private void CalculateSpeedPerTime()
        {
            var vehicleCount = globalVariables.VehicleList.Where(p => p.CurrentPosition.Row < globalVariables.TotalGridRowCount
                && p.CurrentPosition.Row > (globalVariables.TotalGridRowCount - CaftSettings.Default.GridRowCount - 2))
                .Count();

            double FinalAvgSpeed = vehicleCount > 0 ? Convert.ToDouble(AvgSpeed / vehicleCount) : 0.0;
            globalVariables.speedPerTime.Add(globalVariables.TickCount, FinalAvgSpeed);

            FinalAvgSpeed = 0;
        }

        private void PartialCalculateSpeedPerTime(Vehicle vehicle)
        {
            if (vehicle.CurrentPosition.Row < globalVariables.TotalGridRowCount && vehicle.CurrentPosition.Row > (globalVariables.TotalGridRowCount - CaftSettings.Default.GridRowCount - 2))
            {
                AvgSpeed += (((double)CaftSettings.Default.CellSize_Height * vehicle.CurrentCellSpeed) * 3600) / 1000;
            }
        }

        public void CalculateSpacePerTime(Vehicle vehicle)
        {
            if (vehicle.CurrentPosition.Row < -7) return;

            List<SpacePerTime> temp;
            if (globalVariables.spacePerTime.TryGetValue(vehicle.Id, out temp))
            {
                if (temp == null)
                {
                    temp = new List<SpacePerTime>();
                }

                double cellDist = (globalVariables.TotalGridRowCount - vehicle.CurrentPosition.Row) * (double)CaftSettings.Default.CellSize_Height;
                if (cellDist > 0)
                {
                    temp.Add(new SpacePerTime()
                    {
                        time = globalVariables.TickCount,
                        CellDistance = cellDist,
                        speed = functions.GetSpeedInKmPerHr(vehicle.CurrentCellSpeed)
                    });
                }
                else
                {
                    //Debug (What's issue)
                }

            }
            else
            {
                temp = new List<SpacePerTime>();
                double cellDist = (globalVariables.TotalGridRowCount - vehicle.CurrentPosition.Row) * (double)CaftSettings.Default.CellSize_Height;
                if (cellDist > 0)
                {
                    temp.Add(new SpacePerTime()
                    {
                        time = globalVariables.TickCount,
                        CellDistance = cellDist,
                        speed = functions.GetSpeedInKmPerHr(vehicle.CurrentCellSpeed)
                    });

                    globalVariables.spacePerTime.Add(vehicle.Id, temp);
                }
                else
                {
                    //Debug (What's issue)
                }
            }

            //80 - vehicle.CurrentPosition.Row
        }

        #endregion
    }
}
