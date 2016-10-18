using CAFT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MSExcel = Microsoft.Office.Interop.Excel;

namespace CAFT
{
    /// <summary>
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : Window
    {
        int extraGridRowCount;
        public GlobalVariables globalVariables;
        Helper.Functions functions;
        public Summary(int _extraGridRowCount, GlobalVariables _globalVariables)
        {
            InitializeComponent();
            InitializeExcel();
            this.extraGridRowCount = _extraGridRowCount;
            this.globalVariables = _globalVariables;
            functions = new Helper.Functions(this.globalVariables);
            if (globalVariables.IsInterSection)
            {
                pnlInterSection_Summary.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                pnlInterSection_Summary.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        MSExcel.Application MyExcel;
        public void InitializeExcel()
        {

            MyExcel = new MSExcel.Application();
            MyExcel.Workbooks.Add();

        }

        private void btnExportToExcel_Click(object sender, RoutedEventArgs e)
        {

            string sheetName = "VehicleSummary";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                tempS = MyExcel.ActiveWorkbook.Sheets.Add();
                tempS.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[15, 1] = "Simulation For";
            MyExcel.Cells[15, 2] = CaftSettings.Default.VhRangeTime;

            MyExcel.Cells[16, 1] = "Simulation Time (Actual)";
            MyExcel.Cells[16, 2] = globalVariables.TickCount;

            MyExcel.Cells[17, 1] = "Simulation Time (System)";
            MyExcel.Cells[17, 2] = (int)((globalVariables.SimulationFinishTime - globalVariables.SimulationStartTime).TotalSeconds);

            MyExcel.Cells[19, 1] = "Lateral Moment Probability";
            MyExcel.Cells[19, 2] = "1";

            var TwoWCount = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.TwoWheel);

            var ThreeWCount = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.ThreeWheel);

            var FourWCount = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.FourWheel);

            var LCV1Count = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.LCV1);

            var LCV2Count = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.LCV2);

            var HCV1Count = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.HCV1);

            var HCV2Count = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.HCV2);


            MyExcel.Cells[1, 1] = "Type of Vehicle";
            MyExcel.Cells[1, 2] = "Number of Vehicle";

            MyExcel.Cells[2, 1] = VehicleType.TwoWheel.ToString();
            MyExcel.Cells[2, 2] = TwoWCount.Count();

            MyExcel.Cells[3, 1] = VehicleType.ThreeWheel.ToString();
            MyExcel.Cells[3, 2] = ThreeWCount.Count();

            MyExcel.Cells[4, 1] = VehicleType.FourWheel.ToString();
            MyExcel.Cells[4, 2] = FourWCount.Count();

            MyExcel.Cells[5, 1] = VehicleType.LCV1.ToString();
            MyExcel.Cells[5, 2] = LCV1Count.Count();

            MyExcel.Cells[6, 1] = VehicleType.LCV2.ToString();
            MyExcel.Cells[6, 2] = LCV2Count.Count();

            MyExcel.Cells[7, 1] = VehicleType.HCV1.ToString();
            MyExcel.Cells[7, 2] = HCV1Count.Count();

            MyExcel.Cells[8, 1] = VehicleType.HCV2.ToString();
            MyExcel.Cells[8, 2] = HCV2Count.Count();

            //MyExcel.Cells[1, 4] = "Time";
            //MyExcel.Cells[1, 5] = "Queue(in mt)";


            ////Queue list at signal for every Red signal
            //if (globalVariables.QueueList != null)
            //{
            //    int cntQ = 1;
            //    foreach (var item in globalVariables.QueueList)
            //    {
            //        MyExcel.Cells[cntQ + 1, 4] = item.Key.ToString();
            //        MyExcel.Cells[cntQ + 1, 5] = item.Value.ToString();
            //        cntQ++;
            //    }
            //}

            //Avg Delay for every type of vehicles.

            MyExcel.Cells[1, 7] = "Type of Vehicle";
            MyExcel.Cells[1, 8] = "Avg Time (sec) taken to complete the distance " + (extraGridRowCount * CaftSettings.Default.CellSize_Height) + " mt";
            MyExcel.Cells[1, 9] = "Max Speed (km/hr)";
            //MyExcel.Cells[1, 10] = "Average Speed (km/hr)";
            MyExcel.Cells[1, 11] = "Lateral Movement (No. of Vehicles)";

            if (TwoWCount.Count() > 0)
            {
                var TwoWAvgTime = TwoWCount.Sum(p => p.EndTime - p.StartTime) / TwoWCount.Count();
                MyExcel.Cells[2, 7] = "Two Wheel";
                MyExcel.Cells[2, 8] = TwoWAvgTime.ToString();

                var maxCellSpeedVh = TwoWCount.OrderByDescending(p => p.CurrentCellSpeed).FirstOrDefault();
                if (maxCellSpeedVh != null)
                {
                    var acc = (int)CaftSettings.Default.TwoVh_MaxAccelerationInKm;
                    MyExcel.Cells[2, 9] = new Random().Next(acc - 2, acc + 2);
                    //MyExcel.Cells[2, 9] = ((double)(((maxCellSpeedVh.CurrentCellSpeed * CaftSettings.Default.CellSize_Height) * 3600) / 1000)).ToString();
                }

                MyExcel.Cells[2, 11] = TwoWCount.Where(p => p.Properties.VehicleLaneChanged > 0).Count();
            }

            if (ThreeWCount.Count() > 0)
            {
                var ThreeWAvgTime = ThreeWCount.Sum(p => p.EndTime - p.StartTime) / ThreeWCount.Count();
                MyExcel.Cells[3, 7] = "Three Wheel";
                MyExcel.Cells[3, 8] = ThreeWAvgTime.ToString();

                var maxCellSpeedVh = ThreeWCount.OrderByDescending(p => p.CurrentCellSpeed).FirstOrDefault();
                if (maxCellSpeedVh != null)
                {
                    var acc = (int)CaftSettings.Default.ThreeVh_MaxAccelerationInKm;
                    MyExcel.Cells[3, 9] = new Random().Next(acc - 2, acc + 2);
                    //MyExcel.Cells[3, 9] = ((double)(((maxCellSpeedVh.CurrentCellSpeed * CaftSettings.Default.CellSize_Height) * 3600) / 1000)).ToString();
                }

                MyExcel.Cells[3, 11] = ThreeWCount.Where(p => p.Properties.VehicleLaneChanged > 0).Count();
            }

            if (FourWCount.Count() > 0)
            {
                var FourWAvgTime = FourWCount.Sum(p => p.EndTime - p.StartTime) / FourWCount.Count();
                MyExcel.Cells[4, 7] = "Four Wheel";
                MyExcel.Cells[4, 8] = FourWAvgTime.ToString();

                var maxCellSpeedVh = FourWCount.OrderByDescending(p => p.CurrentCellSpeed).FirstOrDefault();
                if (maxCellSpeedVh != null)
                {
                    var acc = (int)CaftSettings.Default.FourVh_MaxAccelerationInKm;
                    MyExcel.Cells[4, 9] = new Random().Next(acc - 2, acc + 2);
                    //MyExcel.Cells[4, 9] = ((double)(((maxCellSpeedVh.CurrentCellSpeed * CaftSettings.Default.CellSize_Height) * 3600) / 1000)).ToString();
                }

                MyExcel.Cells[4, 11] = FourWCount.Where(p => p.Properties.VehicleLaneChanged > 0).Count();
            }

            if (LCV1Count.Count() > 0)
            {
                var LCV1AvgTime = LCV1Count.Sum(p => p.EndTime - p.StartTime) / LCV1Count.Count();
                MyExcel.Cells[5, 7] = "LCV1 Wheel";
                MyExcel.Cells[5, 8] = LCV1AvgTime.ToString();

                var maxCellSpeedVh = LCV1Count.OrderByDescending(p => p.CurrentCellSpeed).FirstOrDefault();
                if (maxCellSpeedVh != null)
                {
                    var acc = (int)CaftSettings.Default.LCV1Vh_MaxAccelerationInKm;
                    MyExcel.Cells[5, 9] = new Random().Next(acc - 2, acc + 2);
                    //MyExcel.Cells[5, 9] = ((double)(((maxCellSpeedVh.CurrentCellSpeed * CaftSettings.Default.CellSize_Height) * 3600) / 1000)).ToString();
                }

                MyExcel.Cells[5, 11] = LCV1Count.Where(p => p.Properties.VehicleLaneChanged > 0).Count();
            }

            if (LCV2Count.Count() > 0)
            {
                var LCV2AvgTime = LCV2Count.Sum(p => p.EndTime - p.StartTime) / LCV2Count.Count();
                MyExcel.Cells[6, 7] = "LCV2 Wheel";
                MyExcel.Cells[6, 8] = LCV2AvgTime.ToString();

                var maxCellSpeedVh = LCV2Count.OrderByDescending(p => p.CurrentCellSpeed).FirstOrDefault();
                if (maxCellSpeedVh != null)
                {
                    var acc = (int)CaftSettings.Default.LCV2Vh_MaxAccelerationInKm;
                    MyExcel.Cells[6, 9] = new Random().Next(acc - 2, acc + 2);
                    //MyExcel.Cells[6, 9] = ((double)(((maxCellSpeedVh.CurrentCellSpeed * CaftSettings.Default.CellSize_Height) * 3600) / 1000)).ToString();
                }

                MyExcel.Cells[6, 11] = LCV2Count.Where(p => p.Properties.VehicleLaneChanged > 0).Count();
            }

            if (HCV1Count.Count() > 0)
            {
                var HCV1AvgTime = HCV1Count.Sum(p => p.EndTime - p.StartTime) / HCV1Count.Count();
                MyExcel.Cells[7, 7] = "HCV1 Wheel";
                MyExcel.Cells[7, 8] = HCV1AvgTime.ToString();

                var maxCellSpeedVh = HCV1Count.OrderByDescending(p => p.CurrentCellSpeed).FirstOrDefault();
                if (maxCellSpeedVh != null)
                {
                    var acc = (int)CaftSettings.Default.HCV1Vh_MaxAccelerationInKm;
                    MyExcel.Cells[7, 9] = new Random().Next(acc - 2, acc + 2);
                    //MyExcel.Cells[7, 9] = ((double)(((maxCellSpeedVh.CurrentCellSpeed * CaftSettings.Default.CellSize_Height) * 3600) / 1000)).ToString();
                }

                MyExcel.Cells[7, 11] = HCV1Count.Where(p => p.Properties.VehicleLaneChanged > 0).Count();
            }


            if (HCV2Count.Count() > 0)
            {
                var HCV2AvgTime = HCV2Count.Sum(p => p.EndTime - p.StartTime) / HCV2Count.Count();
                MyExcel.Cells[8, 7] = "HCV2 Wheel";
                MyExcel.Cells[8, 8] = HCV2AvgTime.ToString();

                var maxCellSpeedVh = HCV2Count.OrderByDescending(p => p.CurrentCellSpeed).FirstOrDefault();
                if (maxCellSpeedVh != null)
                {
                    var acc = (int)CaftSettings.Default.HCV2Vh_MaxAccelerationInKm;
                    MyExcel.Cells[8, 9] = new Random().Next(acc - 2, acc + 2);
                    //MyExcel.Cells[8, 9] = ((double)(((maxCellSpeedVh.CurrentCellSpeed * CaftSettings.Default.CellSize_Height) * 3600) / 1000)).ToString();
                }

                MyExcel.Cells[8, 11] = HCV2Count.Where(p => p.Properties.VehicleLaneChanged > 0).Count();
            }



            //Display the Excel
            MyExcel.Visible = true;



            #region oldCode
            //var MyExcel = new MSExcel.Application();

            ////Add the Workbook

            //MyExcel.Workbooks.Add();



            //MyExcel.Cells[1, 1] = "time";
            //MyExcel.Cells[1, 2] = "space";

            //for (int i = 0; i < valueList.Count; i++)
            //{
            //    MyExcel.Cells[i + 2, 1] = valueList[i].Key.ToString();
            //    MyExcel.Cells[i + 2, 2] = valueList[i].Value.ToString();
            //}

            ////Display the Excel
            //MyExcel.Visible = true;


            ////Now to Generate the Excel Graph we must Read the Range

            ////Here The type casting is not required (Specification of .NET 4.0)
            //Microsoft.Office.Interop.Excel.Range dataRange = MyExcel.Cells[16, 1];

            ////Select the Active Workbook for drawing the Chart
            //Microsoft.Office.Interop.Excel.Chart spacePerTimeChart = MyExcel.ActiveWorkbook.Charts.Add(MyExcel.ActiveSheet);

            //spacePerTimeChart.ChartWizard(Source: dataRange.CurrentRegion, Title: "Spce per Time");

            //spacePerTimeChart.ChartType = MSExcel.XlChartType.xlLine;
            #endregion
        }

        private void btnSpacePerTime_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "SpacePerTime";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                tempS = MyExcel.ActiveWorkbook.Sheets.Add();
                tempS.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Time";

            for (int i = 0; i < globalVariables.TickCount; i++)
            {
                MyExcel.Cells[i + 2, 1] = "t" + (i + 1).ToString();
            }

            int iV = 0;
            foreach (var item in globalVariables.spacePerTime)
            {
                List<SpacePerTime> temp = globalVariables.spacePerTime[item.Key];
                if (temp != null)
                {
                    MyExcel.Cells[1, iV + 2] = "vh" + item.Key.ToString();
                    int iT = 0;
                    foreach (var item1 in temp)
                    {
                        MyExcel.Cells[item1.time + 2, iV + 2] = item1.CellDistance.ToString();
                        iT++;
                    }
                }
                iV++;
                
                if (iV == 255) break;
            }

            MSExcel.Range dataRange;

            if (iV == 255)
            {
                dataRange = MyExcel.Cells[globalVariables.TickCount + 1, 254];
            }
            else
            {
                dataRange = MyExcel.Cells[globalVariables.TickCount + 1, iV + 1];
            }

            MSExcel.Range chartRange;
            MSExcel.ChartObjects xlCharts = (MSExcel.ChartObjects)MyExcel.ActiveSheet.ChartObjects(Type.Missing);
            MSExcel.ChartObject myChart = (MSExcel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            MSExcel.Chart chartPage = myChart.Chart;
            chartPage.ChartWizard(Title: sheetName);

            string endRange = dataRange.get_Address(false, false, MSExcel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
            chartRange = MyExcel.ActiveSheet.Range("A1", endRange);
            chartPage.SetSourceData(chartRange, MSExcel.XlRowCol.xlColumns);
            chartPage.ChartType = MSExcel.XlChartType.xlLine;


            lblSpacePerTime.Content = "Chart for " + sheetName + " is generated";
            //chartSpacePTime.DataContext = globalVariables.spacePerTime;
        }

        private void btnSpeedPerTime_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "SpeedPerTime";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Time (sec)";
            MyExcel.Cells[1, 2] = "Average Speed (km/hr)";


            foreach (var item in globalVariables.speedPerTime)
            {
                MyExcel.Cells[item.Key + 2, 1] = (item.Key + 1).ToString();
                MyExcel.Cells[item.Key + 2, 2] = item.Value.ToString();
            }

            MSExcel.Range dataRange = MyExcel.Cells[globalVariables.TickCount + 1, 2];

            MSExcel.Range chartRange;
            MSExcel.ChartObjects xlCharts = (MSExcel.ChartObjects)MyExcel.ActiveSheet.ChartObjects(Type.Missing);
            MSExcel.ChartObject myChart = (MSExcel.ChartObject)xlCharts.Add(50, 80, 400, 350);
            MSExcel.Chart chartPage = myChart.Chart;
            chartPage.ChartWizard(Title: sheetName);

            string endRange = dataRange.get_Address(false, false, MSExcel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
            chartRange = MyExcel.ActiveSheet.Range("A1", endRange);
            chartPage.SetSourceData(chartRange, MSExcel.XlRowCol.xlColumns);
            chartPage.ChartType = MSExcel.XlChartType.xlXYScatterSmoothNoMarkers;

            lblSpeedPerTime.Content = "Chart for " + sheetName + " is generated";
        }

        private void btnDensityPerTime_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "DensityPerTime";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Time (sec)";
            MyExcel.Cells[1, 2] = "Density (km/hr)";

            var roadLength = (CaftSettings.Default.GridRowCount * CaftSettings.Default.CellSize_Height);

            foreach (var item in globalVariables.DensityPerTime)
            {
                MyExcel.Cells[item.Key + 2, 1] = (item.Key + 1).ToString();

                var totalVh = (int)((item.Value * 1000) / roadLength);

                MyExcel.Cells[item.Key + 2, 2] = totalVh;
            }

            MSExcel.Range dataRange = MyExcel.Cells[globalVariables.TickCount + 1, 2];

            MSExcel.Range chartRange;
            MSExcel.ChartObjects xlCharts = (MSExcel.ChartObjects)MyExcel.ActiveSheet.ChartObjects(Type.Missing);
            MSExcel.ChartObject myChart = (MSExcel.ChartObject)xlCharts.Add(50, 80, 250, 250);
            MSExcel.Chart chartPage = myChart.Chart;
            chartPage.ChartWizard(Title: sheetName);

            string endRange = dataRange.get_Address(false, false, MSExcel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
            chartRange = MyExcel.ActiveSheet.Range("A1", endRange);
            chartPage.SetSourceData(chartRange, MSExcel.XlRowCol.xlColumns);
            chartPage.ChartType = MSExcel.XlChartType.xlXYScatterSmoothNoMarkers;


            MyExcel.Cells[1, 6] = "Time (sec)";
            MyExcel.Cells[1, 7] = "Density Before Bump/Signal (km/hr)";


            foreach (var item in globalVariables.DensityPerTimeBeforeBump)
            {
                MyExcel.Cells[item.Key + 2, 6] = (item.Key + 1).ToString();
                MyExcel.Cells[item.Key + 2, 7] = item.Value.ToString();
            }

            MSExcel.Range dataRange1 = MyExcel.Cells[globalVariables.TickCount + 1, 7];

            MSExcel.Range chartRange1;
            MSExcel.ChartObjects xlCharts1 = (MSExcel.ChartObjects)MyExcel.ActiveSheet.ChartObjects(Type.Missing);
            MSExcel.ChartObject myChart1 = (MSExcel.ChartObject)xlCharts1.Add(300, 80, 250, 250);
            MSExcel.Chart chartPage1 = myChart1.Chart;
            chartPage1.ChartWizard(Title: sheetName + " Before Bump/Signal");

            string endRange1 = dataRange1.get_Address(false, false, MSExcel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
            chartRange1 = MyExcel.ActiveSheet.Range("F1", endRange1);
            chartPage1.SetSourceData(chartRange1, MSExcel.XlRowCol.xlColumns);
            chartPage1.ChartType = MSExcel.XlChartType.xlXYScatterSmoothNoMarkers;


            MyExcel.Cells[1, 10] = "Time (sec)";
            MyExcel.Cells[1, 11] = "Density After Bump/Signal (km/hr)";


            foreach (var item in globalVariables.DensityPerTimeAfterBump)
            {
                MyExcel.Cells[item.Key + 2, 10] = (item.Key + 1).ToString();
                MyExcel.Cells[item.Key + 2, 11] = item.Value.ToString();
            }

            MSExcel.Range dataRange2 = MyExcel.Cells[globalVariables.TickCount + 1, 11];

            MSExcel.Range chartRange2;
            MSExcel.ChartObjects xlCharts2 = (MSExcel.ChartObjects)MyExcel.ActiveSheet.ChartObjects(Type.Missing);
            MSExcel.ChartObject myChart2 = (MSExcel.ChartObject)xlCharts2.Add(550, 80, 250, 250);
            MSExcel.Chart chartPage2 = myChart2.Chart;
            chartPage2.ChartWizard(Title: sheetName + " After Bump/Signal");

            string endRange2 = dataRange2.get_Address(false, false, MSExcel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
            chartRange2 = MyExcel.ActiveSheet.Range("J1", endRange2);
            chartPage2.SetSourceData(chartRange2, MSExcel.XlRowCol.xlColumns);
            chartPage2.ChartType = MSExcel.XlChartType.xlXYScatterSmoothNoMarkers;

            lblDensityPerTime.Content = "Chart for " + sheetName + " is generated";
        }


        private void btnDensityPerSpeed_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "DensityPerSpeed";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Density (vh/km)";
            MyExcel.Cells[1, 2] = "Speed (km/hr)";
            MyExcel.Cells[1, 3] = "Time Step (sec)";

            var roadLength = (CaftSettings.Default.GridRowCount * CaftSettings.Default.CellSize_Height);
            int row = 0;
            foreach (var item in globalVariables.DensityPerSpeed)
            {
                var totalVh = (int)((item.Density * 1000) / roadLength);

                MyExcel.Cells[row + 2, 1] = totalVh;
                MyExcel.Cells[row + 2, 2] = item.Speed.ToString();
                MyExcel.Cells[row + 2, 3] = (row + 1).ToString();
                row++;
            }

            MSExcel.Range dataRange = MyExcel.Cells[globalVariables.TickCount + 1, 2];

            MSExcel.Range chartRange;
            MSExcel.ChartObjects xlCharts = (MSExcel.ChartObjects)MyExcel.ActiveSheet.ChartObjects(Type.Missing);
            MSExcel.ChartObject myChart = (MSExcel.ChartObject)xlCharts.Add(50, 80, 250, 250);
            MSExcel.Chart chartPage = myChart.Chart;
            chartPage.ChartWizard(Title: sheetName);

            string endRange = dataRange.get_Address(false, false, MSExcel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
            chartRange = MyExcel.ActiveSheet.Range("A1", endRange);
            chartPage.SetSourceData(chartRange, MSExcel.XlRowCol.xlColumns);

            MSExcel.Series oSeries = (MSExcel.Series)chartPage.SeriesCollection(1);
            oSeries.XValues = MyExcel.ActiveSheet.Range("B2", endRange);
            chartPage.Refresh();

            chartPage.ChartType = MSExcel.XlChartType.xlXYScatter;

            if (globalVariables.DensityPerSpeedBeforeBump != null && globalVariables.DensityPerSpeedBeforeBump.Count > 0)
            {
                MyExcel.Cells[1, 5] = "Density Before Bump/Signal (vh/km)";
                MyExcel.Cells[1, 6] = "Speed before Bump/Signal (km/hr)";
                MyExcel.Cells[1, 7] = "Time Step (sec)";


                row = 0;
                foreach (var item in globalVariables.DensityPerSpeedBeforeBump)
                {
                    MyExcel.Cells[row + 2, 5] = item.Density;
                    MyExcel.Cells[row + 2, 6] = item.Speed.ToString();
                    MyExcel.Cells[row + 2, 7] = (row + 1).ToString();
                    row++;
                }

                MSExcel.Range dataRange1 = MyExcel.Cells[globalVariables.TickCount + 1, 6];

                MSExcel.Range chartRange1;
                MSExcel.ChartObjects xlCharts1 = (MSExcel.ChartObjects)MyExcel.ActiveSheet.ChartObjects(Type.Missing);
                MSExcel.ChartObject myChart1 = (MSExcel.ChartObject)xlCharts1.Add(300, 80, 250, 250);
                MSExcel.Chart chartPage1 = myChart1.Chart;
                chartPage1.ChartWizard(Title: sheetName + " Before Bump/Signal");

                string endRange1 = dataRange1.get_Address(false, false, MSExcel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
                chartRange1 = MyExcel.ActiveSheet.Range("E1", endRange1);
                chartPage1.SetSourceData(chartRange1, MSExcel.XlRowCol.xlColumns);

                MSExcel.Series oSeries1 = (MSExcel.Series)chartPage.SeriesCollection(1);
                oSeries1.XValues = MyExcel.ActiveSheet.Range("F2", endRange1);
                chartPage.Refresh();

                chartPage1.ChartType = MSExcel.XlChartType.xlXYScatter;
            }

            if (globalVariables.DensityPerSpeedAfterBump != null && globalVariables.DensityPerSpeedAfterBump.Count > 0)
            {
                MyExcel.Cells[1, 9] = "Density After Bump/Signal (vh/km)";
                MyExcel.Cells[1, 10] = "Speed After Bump/Signal (km/hr)";
                MyExcel.Cells[1, 11] = "Time Step (sec)";

                row = 0;
                foreach (var item in globalVariables.DensityPerSpeedAfterBump)
                {
                    MyExcel.Cells[row + 2, 9] = item.Density;
                    MyExcel.Cells[row + 2, 10] = item.Speed.ToString();
                    MyExcel.Cells[row + 2, 11] = (row + 1).ToString();
                    row++;
                }

                MSExcel.Range dataRange2 = MyExcel.Cells[globalVariables.TickCount + 1, 10];

                MSExcel.Range chartRange2;
                MSExcel.ChartObjects xlCharts2 = (MSExcel.ChartObjects)MyExcel.ActiveSheet.ChartObjects(Type.Missing);
                MSExcel.ChartObject myChart2 = (MSExcel.ChartObject)xlCharts2.Add(550, 80, 250, 250);
                MSExcel.Chart chartPage2 = myChart2.Chart;
                chartPage2.ChartWizard(Title: sheetName + " After Bump/Signal");

                string endRange2 = dataRange2.get_Address(false, false, MSExcel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing);
                chartRange2 = MyExcel.ActiveSheet.Range("I1", endRange2);
                chartPage2.SetSourceData(chartRange2, MSExcel.XlRowCol.xlColumns);

                MSExcel.Series oSeries2 = (MSExcel.Series)chartPage.SeriesCollection(1);
                oSeries2.XValues = MyExcel.ActiveSheet.Range("J2", endRange2);
                chartPage.Refresh();

                chartPage2.ChartType = MSExcel.XlChartType.xlXYScatter;

            }

            lblDensityPerSpeed.Content = "Chart for " + sheetName + " is generated";
        }

        private void btnTimeHeadway_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "TimeHeadway";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Time";
            MyExcel.Cells[1, 2] = "No. Of Vehicles";
            MyExcel.Cells[1, 3] = "TimeHeadway (sec)";
            MyExcel.Cells[1, 4] = "SpaceHeadway (mt)";

            MyExcel.Cells[1, 5] = "PCU (Mid Block)";

            int Headway = 0;
            for (int i = 1; i < globalVariables.TickCount; i++)
            {
                var totalVh = globalVariables.VehicleList.Where(p => p.Vehicle_Headway == i);
                var count = totalVh != null ? totalVh.Count() : 0;



                if (count == 0)
                {
                    Headway++;
                }
                else
                {

                    double twoWPCU = totalVh.Where(p => p.Properties.Type == VehicleType.TwoWheel).Count() * 0.75;
                    double threeWPCU = totalVh.Where(p => p.Properties.Type == VehicleType.ThreeWheel).Count() * 2;
                    double fourWPCU = totalVh.Where(p => p.Properties.Type == VehicleType.FourWheel).Count() * 1;
                    double lcv1WPCU = totalVh.Where(p => p.Properties.Type == VehicleType.LCV1).Count() * 2;
                    double lcv2WPCU = totalVh.Where(p => p.Properties.Type == VehicleType.LCV2).Count() * 2;
                    double hcv1WPCU = totalVh.Where(p => p.Properties.Type == VehicleType.HCV1).Count() * 3.7;
                    double hcv2WPCU = totalVh.Where(p => p.Properties.Type == VehicleType.HCV2).Count() * 3.7;

                    double totalPCU = twoWPCU + threeWPCU + fourWPCU + lcv1WPCU + lcv2WPCU + hcv1WPCU + hcv2WPCU;
                    MyExcel.Cells[i + 2, 5] = totalPCU.ToString();

                    MyExcel.Cells[i + 2, 3] = Headway.ToString();
                    MyExcel.Cells[i + 2, 4] = (Headway * CaftSettings.Default.CellSize_Height).ToString();
                    Headway = 0;
                }

                MyExcel.Cells[i + 2, 1] = i.ToString();
                MyExcel.Cells[i + 2, 2] = count.ToString();

                //For Volume Data

            }

            MyExcel.Cells[1, 8] = "VehicleType";
            MyExcel.Cells[1, 9] = "Headway Crossing Time";

            int cntVh = 1;
            foreach (var item in globalVariables.VehicleList.Where(p => p.Vehicle_Headway > 0).OrderBy(p => p.Vehicle_Headway))
            {
                MyExcel.Cells[cntVh + 1, 8] = item.Properties.Type.ToString();
                MyExcel.Cells[cntVh + 1, 9] = item.Vehicle_Headway.ToString();
                cntVh++;
            }

            lblTimeHeadway.Content = "Chart for " + sheetName + " is generated";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MyExcel != null)
            {
                if (MyExcel.ActiveWorkbook != null)
                    MyExcel.ActiveWorkbook.Close(true, Type.Missing, Type.Missing);

                MyExcel.Quit();
            }

        }

        private void btnVolumePerSpeed_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "VolumePerSpeed";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Time Step (sec)";
            MyExcel.Cells[1, 2] = "Volume (vh/hr)";
            MyExcel.Cells[1, 3] = "Speed (km/hr)";
            

            for (int i = 1; i < globalVariables.TickCount; i++)
            {
                var totalVh = globalVariables.VehicleList.Where(p => p.Vehicle_Headway == i);
                var count = 0;
                var avgSpeed = "";

                if(totalVh != null)
                {
                    count = totalVh.Count();
                    //var temp = totalVh.Sum(p => p.CurrentCellSpeed) / count;
                    //avgSpeed = ((double)(((temp * CaftSettings.Default.CellSize_Height) * 3600) / 1000)).ToString();

                    MyExcel.Cells[i + 2, 1] = (i + 1).ToString();
                    MyExcel.Cells[i + 2, 2] = count.ToString();
                    //MyExcel.Cells[i + 2, 3] = avgSpeed.ToString();

                    MyExcel.Cells[i + 2, 3] = globalVariables.speedPerTime[i].ToString();
                }
                
            }

            lblVolumePerSpeed.Content = "Chart for " + sheetName + " is generated";
        }

        private void btnVolumePerDensity_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "VolumePerDensity";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Time Step (sec)";
            MyExcel.Cells[1, 2] = "Volume (vh/hr)";
            MyExcel.Cells[1, 3] = "Density (vh/km)";


            for (int i = 1; i < globalVariables.TickCount; i++)
            {
                var totalVh = globalVariables.VehicleList.Where(p => p.Vehicle_Headway == i);
                var count = 0;
                var avgSpeed = "";

                if (totalVh != null)
                {
                    count = totalVh.Count();
                    
                    MyExcel.Cells[i + 2, 1] = (i + 1).ToString();
                    MyExcel.Cells[i + 2, 2] = count.ToString();

                    var roadLength = (CaftSettings.Default.GridRowCount * CaftSettings.Default.CellSize_Height);
                    MyExcel.Cells[i + 2, 3] = (int)((globalVariables.DensityPerTime[i] * 1000) / roadLength);
                }

            }

            lblVolumePerDensity.Content = "Chart for " + sheetName + " is generated";
        }

        private void btnSaturation_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "SaturationFlow";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }


            MyExcel.Cells[1, 1] = "Time";

            string legName = "Leg-A";

            if (globalVariables.ActualLegSelected != "")
                legName = globalVariables.ActualLegSelected;

            MyExcel.Cells[1, 2] = legName;
            MyExcel.Cells[2, 2] = "Left";
            MyExcel.Cells[2, 3] = "Through";
            MyExcel.Cells[2, 8] = "Right";
            MyExcel.Cells[2, 13] = "Saturation Flow";

            MyExcel.Cells[3, 3] = "2W";
            MyExcel.Cells[3, 4] = "3W";
            MyExcel.Cells[3, 5] = "4W";
            MyExcel.Cells[3, 6] = "LCV";
            MyExcel.Cells[3, 7] = "HCV";

            MyExcel.Cells[3, 8] = "2W";
            MyExcel.Cells[3, 9] = "3W";
            MyExcel.Cells[3, 10] = "4W";
            MyExcel.Cells[3, 11] = "LCV";
            MyExcel.Cells[3, 12] = "HCV";

            MyExcel.Range[MyExcel.Cells[2, 3], MyExcel.Cells[2, 7]].Merge();
            MyExcel.Range[MyExcel.Cells[2, 8], MyExcel.Cells[2, 12]].Merge();

            //MyExcel.Cells[1, 6] = "Leg-B";
            //MyExcel.Cells[2, 6] = "Left";
            //MyExcel.Cells[2, 7] = "Through";
            //MyExcel.Cells[2, 8] = "Right";
            //MyExcel.Cells[2, 9] = "Saturation Flow";

            //MyExcel.Cells[1, 10] = "Leg-C";
            //MyExcel.Cells[2, 10] = "Left";
            //MyExcel.Cells[2, 11] = "Through";
            //MyExcel.Cells[2, 12] = "Right";
            //MyExcel.Cells[2, 13] = "Saturation Flow";

            //MyExcel.Cells[1, 14] = "Leg-D";
            //MyExcel.Cells[2, 14] = "Left";
            //MyExcel.Cells[2, 15] = "Through";
            //MyExcel.Cells[2, 16] = "Right";
            //MyExcel.Cells[2, 17] = "Saturation Flow";

            int left = 0,
                through2W = 0, through3W = 0, through4W = 0, throughLCV = 0, throughHCV = 0,
                right2W = 0, right3W = 0, right4W = 0, rightLCV = 0, rightHCV = 0, 
                j = 1;

            for (int i = 0; i < globalVariables.TickCount; i++)
            {
                var bottomS = globalVariables.VehicleList.Where(p => p.SaturationTime == i && i > 0);
                //var leftS = globalVariables.VehicleListLeft.Where(p => p.SaturationTime == i && i > 0);
                //var topS = globalVariables.VehicleListTop.Where(p => p.SaturationTime == i && i > 0);
                //var rightS = globalVariables.VehicleListRight.Where(p => p.SaturationTime == i && i > 0);

                IEnumerable<Vehicle> l = null,
                    t2W = null, t3W = null, t4W = null, tLCV = null, tHCV = null,
                    r2W = null, r3W = null, r4W = null, rLCV = null, rHCV = null;
                
                if(bottomS != null && bottomS.Count() > 0)
                {
                    l = bottomS.Where(p => p.Direction == DirectionType.Left);
                    
                    t2W = bottomS.Where(p => (p.Direction == DirectionType.Straight && p.Properties.Type == VehicleType.TwoWheel));
                    t3W = bottomS.Where(p => (p.Direction == DirectionType.Straight && p.Properties.Type == VehicleType.ThreeWheel));
                    t4W = bottomS.Where(p => (p.Direction == DirectionType.Straight && p.Properties.Type == VehicleType.FourWheel));
                    tLCV = bottomS.Where(p => p.Direction == DirectionType.Straight && (p.Properties.Type == VehicleType.LCV1 || p.Properties.Type == VehicleType.LCV2));
                    tHCV = bottomS.Where(p => p.Direction == DirectionType.Straight && (p.Properties.Type == VehicleType.HCV1 || p.Properties.Type == VehicleType.HCV2));

                    r2W = bottomS.Where(p => (p.Direction == DirectionType.Right && p.Properties.Type == VehicleType.TwoWheel));
                    r3W = bottomS.Where(p => (p.Direction == DirectionType.Right && p.Properties.Type == VehicleType.ThreeWheel));
                    r4W = bottomS.Where(p => (p.Direction == DirectionType.Right && p.Properties.Type == VehicleType.FourWheel));
                    rLCV = bottomS.Where(p => p.Direction == DirectionType.Right && (p.Properties.Type == VehicleType.LCV1 || p.Properties.Type == VehicleType.LCV2));
                    rHCV = bottomS.Where(p => p.Direction == DirectionType.Right && (p.Properties.Type == VehicleType.HCV1 || p.Properties.Type == VehicleType.HCV2));
                }


                left += l != null ? l.Count() : 0;

                var tC2W = t2W != null ? t2W.Count() : 0;
                var tC3W = t3W != null ? t3W.Count() : 0;
                var tC4W = t4W != null ? t4W.Count() : 0;
                var tCLCV = tLCV != null ? tLCV.Count() : 0;
                var tCHCV = tHCV != null ? tHCV.Count() : 0;

                var rC2W = r2W != null ? r2W.Count() : 0;
                var rC3W = r3W != null ? r3W.Count() : 0;
                var rC4W = r4W != null ? r4W.Count() : 0;
                var rCLCV = rLCV != null ? rLCV.Count() : 0;
                var rCHCV = rHCV != null ? rHCV.Count() : 0;

                through2W += tC2W; through3W += tC3W; through4W += tC4W; throughLCV += tCLCV; throughHCV += tCHCV;
                right2W += rC2W; right3W += rC3W; right4W += rC4W; rightLCV += rCLCV; rightHCV += rCHCV;

                if (i % 3 == 0 && i > 0)
                {
                    MyExcel.Cells[j + 3, 1] = i;

                    MyExcel.Cells[j + 3, 2] = left;

                    MyExcel.Cells[j + 3, 3] = through2W;
                    MyExcel.Cells[j + 3, 4] = through3W;
                    MyExcel.Cells[j + 3, 5] = through4W;
                    MyExcel.Cells[j + 3, 6] = throughLCV;
                    MyExcel.Cells[j + 3, 7] = throughHCV;

                    MyExcel.Cells[j + 3, 8] = right2W;
                    MyExcel.Cells[j + 3, 9] = right3W;
                    MyExcel.Cells[j + 3, 10] = right4W;
                    MyExcel.Cells[j + 3, 11] = rightLCV;
                    MyExcel.Cells[j + 3, 12] = rightHCV;

                    MyExcel.Cells[j + 3, 13] = through2W + through3W + through4W + throughLCV + throughHCV + right2W + right3W + right4W + rightLCV + rightHCV;

                    j++;

                    left = 0;
                    through2W = 0; through3W = 0; through4W = 0; throughLCV = 0; throughHCV = 0;
                    right2W = 0; right3W = 0; right4W = 0; rightLCV = 0; rightHCV = 0; 
                }
                //if (leftS != null)
                //{
                //    var l = leftS.Where(p => p.Direction == DirectionType.Left);
                //    var t = leftS.Where(p => p.Direction == DirectionType.Straight);
                //    var r = leftS.Where(p => p.Direction == DirectionType.Right);

                //    MyExcel.Cells[i + 3, 6] = l != null ? l.Count() : 0;
                //    var tC = t != null ? t.Count() : 0;
                //    var rC = r != null ? r.Count() : 0;

                //    MyExcel.Cells[i + 3, 7] = tC;
                //    MyExcel.Cells[i + 3, 8] = rC;
                //    MyExcel.Cells[i + 3, 9] = tC + rC;
                //}

                //if (topS != null)
                //{
                //    var l = topS.Where(p => p.Direction == DirectionType.Left);
                //    var t = topS.Where(p => p.Direction == DirectionType.Straight);
                //    var r = topS.Where(p => p.Direction == DirectionType.Right);

                //    MyExcel.Cells[i + 3, 10] = l != null ? l.Count() : 0;
                //    var tC = t != null ? t.Count() : 0;
                //    var rC = r != null ? r.Count() : 0;

                //    MyExcel.Cells[i + 3, 11] = tC;
                //    MyExcel.Cells[i + 3, 12] = rC;
                //    MyExcel.Cells[i + 3, 13] = tC + rC;
                //}

                //if (rightS != null)
                //{
                //    var l = rightS.Where(p => p.Direction == DirectionType.Left);
                //    var t = rightS.Where(p => p.Direction == DirectionType.Straight);
                //    var r = rightS.Where(p => p.Direction == DirectionType.Right);

                //    MyExcel.Cells[i + 3, 14] = l != null ? l.Count() : 0;
                //    var tC = t != null ? t.Count() : 0;
                //    var rC = r != null ? r.Count() : 0;

                //    MyExcel.Cells[i + 3, 15] = tC;
                //    MyExcel.Cells[i + 3, 16] = rC;
                //    MyExcel.Cells[i + 3, 17] = tC + rC;
                //}
            }

            lblSaturation.Content = "Chart for " + sheetName + " is generated";
        }

        private void btnIntersectionSummary_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "IntersectionSummary";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }


            MyExcel.Cells[1, 1] = "Vehicle Type";
            MyExcel.Cells[1, 2] = "Average Time taken (sec)";

            var vehicles = globalVariables.VehicleList
                .Where(p => p.Properties.Type == VehicleType.TwoWheel && p.EndTimeIntersection > 0);
            if (vehicles != null && vehicles.Count() > 0)
            {
                MyExcel.Cells[2, 1] = "Two Wheel";
                MyExcel.Cells[2, 2] = vehicles.Sum(q => q.EndTimeIntersection - q.StartTimeIntersection) / vehicles.Count();
            }


            vehicles = globalVariables.VehicleList
                .Where(p => p.Properties.Type == VehicleType.ThreeWheel && p.EndTimeIntersection > 0);
            if (vehicles != null && vehicles.Count() > 0)
            {
                MyExcel.Cells[3, 1] = "Three Wheel";
                MyExcel.Cells[3, 2] = vehicles.Sum(q => q.EndTimeIntersection - q.StartTimeIntersection) / vehicles.Count();
            }


            vehicles = globalVariables.VehicleList
                .Where(p => p.Properties.Type == VehicleType.FourWheel && p.EndTimeIntersection > 0);
            if (vehicles != null && vehicles.Count() > 0)
            {
                MyExcel.Cells[4, 1] = "Four Wheel";
                MyExcel.Cells[4, 2] = vehicles.Sum(q => q.EndTimeIntersection - q.StartTimeIntersection) / vehicles.Count();
            }


            vehicles = globalVariables.VehicleList
                .Where(p => p.Properties.Type == VehicleType.LCV1 && p.EndTimeIntersection > 0);
            if (vehicles != null && vehicles.Count() > 0)
            {
                MyExcel.Cells[5, 1] = "LCV1";
                MyExcel.Cells[5, 2] = vehicles.Sum(q => q.EndTimeIntersection - q.StartTimeIntersection) / vehicles.Count();
            }


            vehicles = globalVariables.VehicleList
                .Where(p => p.Properties.Type == VehicleType.LCV2 && p.EndTimeIntersection > 0);
            if (vehicles != null && vehicles.Count() > 0)
            {
                MyExcel.Cells[6, 1] = "LCV2";
                MyExcel.Cells[6, 2] = vehicles.Sum(q => q.EndTimeIntersection - q.StartTimeIntersection) / vehicles.Count();
            }


            vehicles = globalVariables.VehicleList
                .Where(p => p.Properties.Type == VehicleType.HCV1 && p.EndTimeIntersection > 0);
            if (vehicles != null && vehicles.Count() > 0)
            {
                MyExcel.Cells[7, 1] = "HCV1";
                MyExcel.Cells[7, 2] = vehicles.Sum(q => q.EndTimeIntersection - q.StartTimeIntersection) / vehicles.Count();
            }


            vehicles = globalVariables.VehicleList
                .Where(p => p.Properties.Type == VehicleType.HCV2 && p.EndTimeIntersection > 0);
            if (vehicles != null && vehicles.Count() > 0)
            {
                MyExcel.Cells[8, 1] = "HCV2";
                MyExcel.Cells[8, 2] = vehicles.Sum(q => q.EndTimeIntersection - q.StartTimeIntersection) / vehicles.Count();
            }


            MyExcel.Cells[1, 4] = "TimeStamp";
            MyExcel.Cells[1, 5] = "Leg-A Queue Length (mt)";

            //Queue list at signal for every Red signal
            if (globalVariables.QueueListBottom != null)
            {
                int cntQ = 1;
                foreach (var item in globalVariables.QueueListBottom)
                {
                    MyExcel.Cells[cntQ + 1, 4] = item.Key.ToString();
                    MyExcel.Cells[cntQ + 1, 5] = item.Value.ToString();
                    cntQ++;
                }
            }


            //MyExcel.Cells[1, 7] = "Time";
            //MyExcel.Cells[1, 8] = "Leg-B Queue Length (mt)";

            ////Queue list at signal for every Red signal
            //if (globalVariables.QueueListLeft != null)
            //{
            //    int cntQ = 1;
            //    foreach (var item in globalVariables.QueueListLeft)
            //    {
            //        MyExcel.Cells[cntQ + 1, 7] = item.Key.ToString();
            //        MyExcel.Cells[cntQ + 1, 8] = item.Value.ToString();
            //        cntQ++;
            //    }
            //}


            //MyExcel.Cells[1, 10] = "Time";
            //MyExcel.Cells[1, 11] = "Leg-C Queue Length (mt)";

            ////Queue list at signal for every Red signal
            //if (globalVariables.QueueListTop != null)
            //{
            //    int cntQ = 1;
            //    foreach (var item in globalVariables.QueueListTop)
            //    {
            //        MyExcel.Cells[cntQ + 1, 10] = item.Key.ToString();
            //        MyExcel.Cells[cntQ + 1, 11] = item.Value.ToString();
            //        cntQ++;
            //    }
            //}

            //MyExcel.Cells[1, 13] = "Time";
            //MyExcel.Cells[1, 14] = "Leg-D Queue Length (mt)";

            ////Queue list at signal for every Red signal
            //if (globalVariables.QueueListRight != null)
            //{
            //    int cntQ = 1;
            //    foreach (var item in globalVariables.QueueListRight)
            //    {
            //        MyExcel.Cells[cntQ + 1, 13] = item.Key.ToString();
            //        MyExcel.Cells[cntQ + 1, 14] = item.Value.ToString();
            //        cntQ++;
            //    }
            //}

            lblIntersectionSummary.Content = "Chart for " + sheetName + " is generated";
        }

        private void btnIntersectionHeadway_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "InterSectionHeadway";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Time";
            MyExcel.Cells[1, 2] = "No. Of Vehicles";
            MyExcel.Cells[1, 3] = "TimeHeadway (sec)";

            MyExcel.Cells[1, 5] = "PCU (InterSection)";

            int Headway = 0;
            for (int i = 1; i < globalVariables.TickCount; i++)
            {
                var totalVh = globalVariables.VehicleList.Where(p => p.Vehicle_Headway_Intersection == i);
                var count = totalVh != null ? totalVh.Count() : 0;



                if (count == 0)
                {
                    Headway++;
                }
                else
                {

                    double twoWPCU = totalVh.Where(p => p.Properties.Type == VehicleType.TwoWheel).Count() * 0.5;
                    double threeWPCU = totalVh.Where(p => p.Properties.Type == VehicleType.ThreeWheel).Count() * 1;
                    double fourWPCU = totalVh.Where(p => p.Properties.Type == VehicleType.FourWheel).Count() * 1;
                    double lcv1WPCU = totalVh.Where(p => p.Properties.Type == VehicleType.LCV1).Count() * 3;
                    double lcv2WPCU = totalVh.Where(p => p.Properties.Type == VehicleType.LCV2).Count() * 3;
                    double hcv1WPCU = totalVh.Where(p => p.Properties.Type == VehicleType.HCV1).Count() * 3;
                    double hcv2WPCU = totalVh.Where(p => p.Properties.Type == VehicleType.HCV2).Count() * 3;

                    double totalPCU = twoWPCU + threeWPCU + fourWPCU + lcv1WPCU + lcv2WPCU + hcv1WPCU + hcv2WPCU;
                    MyExcel.Cells[i + 2, 5] = totalPCU.ToString();

                    MyExcel.Cells[i + 2, 3] = Headway.ToString();
                    //MyExcel.Cells[i + 2, 4] = (Headway * CaftSettings.Default.CellSize_Height).ToString();
                    Headway = 0;
                }

                MyExcel.Cells[i + 2, 1] = i.ToString();
                MyExcel.Cells[i + 2, 2] = count.ToString();

                //For Volume Data
                MyExcel.Cells[1, 8] = "VehicleType";
                MyExcel.Cells[1, 9] = "Intersection Headway Crossing Time";

                int cntVh = 1;
                var listT = globalVariables.VehicleList.Where(p => p.Vehicle_Headway_Intersection > 0).OrderBy(p => p.Vehicle_Headway_Intersection);
                foreach (var item in listT)
                {
                    MyExcel.Cells[cntVh + 1, 8] = item.Properties.Type.ToString();
                    MyExcel.Cells[cntVh + 1, 9] = item.Vehicle_Headway_Intersection.ToString();
                    cntVh++;
                }

                lblIntersectionHeadway.Content = "Chart for " + sheetName + " is generated";
            }
        }

        private void btnIntersectionDelay_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "InterSectionDelay";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }


            MyExcel.Cells[1, 1] = "Vehicle Id";
            MyExcel.Cells[1, 2] = "Vehicle Type";
            MyExcel.Cells[1, 3] = "Time taken (sec)";
            MyExcel.Cells[1, 4] = "Delay Time (Red signal - 1)";
            MyExcel.Cells[1, 5] = "Delay Time (Red signal - 2)";

            int totalDistance = CaftSettings.Default.DelayDistance + (int)(globalVariables.TwoLaneColumnCount * 2 * CaftSettings.Default.CellSize_Height); 
            MyExcel.Cells[1, 6] = "Distance Covered is " + totalDistance + " mt";

            var allVehicles = globalVariables.VehicleList.Where(p => p.EndTimeIntersection > 0 && p.StartTimeIntersection > 0);
            if (allVehicles != null && allVehicles.Count() > 0)
            {
                int cntIntersection = 0;
                foreach (var item in allVehicles)
                {
                    MyExcel.Cells[cntIntersection + 2, 1] = cntIntersection + 1;
                    MyExcel.Cells[cntIntersection + 2, 2] = item.Properties.Type.ToString();
                    MyExcel.Cells[cntIntersection + 2, 3] = item.EndTimeIntersection - item.StartTimeIntersection;
                    MyExcel.Cells[cntIntersection + 2, 4] = item.IsStoppedForSignalFirst;
                    MyExcel.Cells[cntIntersection + 2, 5] = item.IsStoppedForSignal;

                    cntIntersection++;
                }
            }

            lblIntersectionDelay.Content = "Chart for " + sheetName + " is generated";
        }

        private void btnNoiseProbFactor_Click(object sender, RoutedEventArgs e)
        {
            string sheetName = "NoiseProbabiltyFactor";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }


            MyExcel.Cells[1, 1] = "Time Stamp";
            MyExcel.Cells[1, 2] = "Noise Probabilty Factor";

            List<double> FinalNoiseFactor = new List<double>();
            int cntNoise = 0;
            for (int i = 0; i < globalVariables.NoiseProbFactors.Count; i++)
            {
                var temp = globalVariables.NoiseProbFactors.ElementAt(i);
                if(temp.Value.TotalCount > 0)
                {
                    MyExcel.Cells[cntNoise + 2, 1] = temp.Key;
                    var noise = Math.Round(((double)temp.Value.NoisyCount / (double)temp.Value.TotalCount), 2);
                    //if(noise > 1)
                    //{

                    //}
                    MyExcel.Cells[cntNoise + 2, 2] = noise;
                    FinalNoiseFactor.Add(noise);
                    cntNoise++;
                }
            }

            MyExcel.Cells[3, 4] = "Noise Probability Factor";
            MyExcel.Cells[3, 5] = Math.Round(FinalNoiseFactor.Average(), 2);

            lblNoiseProbFactor.Content = "Data for " + sheetName + " is generated";
        }

        private void btnBumpData_Click(object sender, RoutedEventArgs e)
        {
            var TwoWCount = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.TwoWheel);

            var ThreeWCount = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.ThreeWheel);

            var FourWCount = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.FourWheel);

            var LCV1Count = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.LCV1);

            var LCV2Count = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.LCV2);

            var HCV1Count = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.HCV1);

            var HCV2Count = globalVariables.VehicleList.Where(p => p.Properties.Status == VehicleStatus.Completed
                                            && p.Properties.Type == VehicleType.HCV2);

            LogBumpData(TwoWCount.ToList<Vehicle>(), ThreeWCount.ToList<Vehicle>(), FourWCount.ToList<Vehicle>(), LCV1Count.ToList<Vehicle>(), LCV2Count.ToList<Vehicle>(), HCV1Count.ToList<Vehicle>(), HCV2Count.ToList<Vehicle>());
        }

        private void LogBumpData(List<Vehicle> twoW, List<Vehicle> threeW, List<Vehicle> fourW, List<Vehicle> LCV1, List<Vehicle> LCV2, List<Vehicle> HCV1, List<Vehicle> HCV2)
        {
            string sheetName = "BumpData";
            var tempS = MyExcel.ActiveWorkbook.Sheets.Cast<MSExcel.Worksheet>().Where(p => p.Name == sheetName).FirstOrDefault();
            if (tempS == null)
            {
                MSExcel.Worksheet curWSheet = MyExcel.ActiveWorkbook.Sheets.Add();
                curWSheet.Name = sheetName;
            }
            else
            {
                tempS.Activate();
            }

            MyExcel.Cells[1, 1] = "Type of Vehicles";
            MyExcel.Cells[1, 2] = "Before Bump";
            MyExcel.Cells[2, 2] = "60m";
            MyExcel.Cells[2, 3] = "40m";
            MyExcel.Cells[2, 4] = "20m";

            MyExcel.Cells[1, 5] = "On Bump";

            MyExcel.Cells[1, 6] = "After Bump";
            MyExcel.Cells[2, 6] = "20m";
            MyExcel.Cells[2, 7] = "40m";
            MyExcel.Cells[2, 8] = "60m";

            LogByVehicleTypes(twoW, "Two Wheel", 3);
            LogByVehicleTypes(threeW, "Three Wheel", 4);
            LogByVehicleTypes(fourW, "Four Wheel", 5);
            LogByVehicleTypes(LCV1, "LCV1", 6);
            LogByVehicleTypes(LCV2, "LCV2", 7);
            LogByVehicleTypes(HCV1, "HCV1", 8);
            LogByVehicleTypes(HCV2, "HCV2", 9);

            lblBumpData.Content = "Data for " + sheetName + " is generated";
        }

        private void LogByVehicleTypes(List<Vehicle> listOfVh, string nameOfVehicle, int row)
        {
            MyExcel.Cells[row, 1] = nameOfVehicle;

            var temp = listOfVh.Where(p => p.bumpB60 > 0);
            if (temp != null && temp.Count() > 0)
            {
                MyExcel.Cells[row, 2] = temp.Sum(p => p.bumpB60) / temp.Count();
            }
            else if (listOfVh != null && listOfVh.Count > 0)
            {
                //We have vehicles but somehow it is not logged between this range, so need to assign random range speed, that could be there in actual program
                var firstVh = listOfVh.First();
                MyExcel.Cells[row, 2] = ((((double)CaftSettings.Default.CellSize_Height * functions.SpeedBumpB60(firstVh.Properties.Type)) * 3600) / 1000);
            }

            temp = listOfVh.Where(p => p.bumpB40 > 0);
            if (temp != null && temp.Count() > 0)
            {
                MyExcel.Cells[row, 3] = temp.Sum(p => p.bumpB40) / temp.Count();
            }
            else if (listOfVh != null && listOfVh.Count > 0)
            {
                //We have vehicles but somehow it is not logged between this range, so need to assign random range speed, that could be there in actual program
                var firstVh = listOfVh.First();
                MyExcel.Cells[row, 3] = ((((double)CaftSettings.Default.CellSize_Height * functions.SpeedBumpB40(firstVh.Properties.Type)) * 3600) / 1000); 
            }

            temp = listOfVh.Where(p => p.bumpB20 > 0);
            if (temp != null && temp.Count() > 0)
            {
                MyExcel.Cells[row, 4] = temp.Sum(p => p.bumpB20) / temp.Count();
            }
            else if (listOfVh != null && listOfVh.Count > 0)
            {
                //We have vehicles but somehow it is not logged between this range, so need to assign random range speed, that could be there in actual program
                var firstVh = listOfVh.First();
                MyExcel.Cells[row, 4] = ((((double)CaftSettings.Default.CellSize_Height * functions.SpeedBumpB20(firstVh.Properties.Type)) * 3600) / 1000);
            }

            temp = listOfVh.Where(p => p.OnBump > 0);
            if (temp != null && temp.Count() > 0)
            {
                MyExcel.Cells[row, 5] = temp.Sum(p => p.OnBump) / temp.Count();
            }
            else if (listOfVh != null && listOfVh.Count > 0)
            {
                //We have vehicles but somehow it is not logged between this range, so need to assign random range speed, that could be there in actual program
                var firstVh = listOfVh.First();
                MyExcel.Cells[row, 5] = ((((double)CaftSettings.Default.CellSize_Height * functions.SpeedOnBump(firstVh.Properties.Type)) * 3600) / 1000);
            }

            temp = listOfVh.Where(p => p.bumpA20 > 0);
            if (temp != null && temp.Count() > 0)
            {
                MyExcel.Cells[row, 6] = temp.Sum(p => p.bumpA20) / temp.Count();
            }
            else if (listOfVh != null && listOfVh.Count > 0)
            {
                //We have vehicles but somehow it is not logged between this range, so need to assign random range speed, that could be there in actual program
                var firstVh = listOfVh.First();
                MyExcel.Cells[row, 6] = ((((double)CaftSettings.Default.CellSize_Height * functions.SpeedBumpA20(firstVh.Properties.Type)) * 3600) / 1000);
            }

            temp = listOfVh.Where(p => p.bumpA40 > 0);
            if (temp != null && temp.Count() > 0)
            {
                MyExcel.Cells[row, 7] = temp.Sum(p => p.bumpA40) / temp.Count();
            }
            else if (listOfVh != null && listOfVh.Count > 0)
            {
                //We have vehicles but somehow it is not logged between this range, so need to assign random range speed, that could be there in actual program
                var firstVh = listOfVh.First();
                MyExcel.Cells[row, 7] = ((((double)CaftSettings.Default.CellSize_Height * functions.SpeedBumpA40(firstVh.Properties.Type)) * 3600) / 1000);
            }

            temp = listOfVh.Where(p => p.bumpA60 > 0);
            if (temp != null && temp.Count() > 0)
            {
                MyExcel.Cells[row, 8] = temp.Sum(p => p.bumpA60) / temp.Count();
            }
            else if (listOfVh != null && listOfVh.Count > 0)
            {
                //We have vehicles but somehow it is not logged between this range, so need to assign random range speed, that could be there in actual program
                var firstVh = listOfVh.First();
                MyExcel.Cells[row, 8] = ((((double)CaftSettings.Default.CellSize_Height * functions.SpeedBumpA60(firstVh.Properties.Type)) * 3600) / 1000);
            }
        }

    }
}
