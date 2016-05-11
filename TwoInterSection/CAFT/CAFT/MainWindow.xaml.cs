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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CAFT.Helper;
using CAFT.Models;
using System.Windows.Threading;
using System.IO;

namespace CAFT
{

    public class GlobalVariables
    {
        //static GlobalVariables _instance;

        //public static GlobalVariables Instance
        //{
        //    get { return _instance ?? (_instance = new GlobalVariables()); }
        //}
        //private GlobalVariables() { }
        public string ActualLegSelected;

        public List<Vehicle> VehicleList = new List<Vehicle>();

        public List<Vehicle> VehicleListTop = new List<Vehicle>();
        public List<Vehicle> VehicleListRight = new List<Vehicle>();
        public List<Vehicle> VehicleListLeft = new List<Vehicle>();

        public Random RndmNo = new Random();

        private bool _LaneSignalOn;
        public bool LaneSignalOn 
        { 
            get
            {
                return _LaneSignalOn;
            }
            set
            {
                if(_LaneSignalOn == false && value == true && this.TickCount > 0)
                {
                    var veh = this.VehicleList
                        .Where(p => p.IsStoppedForSignal > 2
                            && p.Properties.Status == VehicleStatus.InProgress)
                            .OrderByDescending(q => q.CurrentPosition.Row).FirstOrDefault();

                    if (veh != null)
                    {
                        this.QueueListBottom.Add(this.TickCount, (double)(CaftSettings.Default.CellSize_Height * (veh.CurrentPosition.Row - this.ExtraRoadCellsS - (this.TwoLaneColumnCount * 2))));
                    }
                    
                    //MessageBox.Show(abc.Count().ToString() + ": " + cdf.CurrentPosition.Row.ToString() + "," + cdf.Properties.Type.ToString());  
                }
                _LaneSignalOn = value;
            }
        }

        private bool _LaneSignalOn_Top;
        public bool LaneSignalOn_Top
        {
            get { return _LaneSignalOn_Top; }
            set
            {
                if (_LaneSignalOn_Top == false && value == true && this.TickCount > 0)
                {
                    var veh = this.VehicleListTop
                        .Where(p => p.IsStoppedForSignal > 2
                            && p.Properties.Status == VehicleStatus.InProgress)
                            .OrderBy(q => q.CurrentPosition.Row).FirstOrDefault();

                    if (veh != null)
                    {
                        //MessageBox.Show((veh.CurrentPosition.Row).ToString() + ": " + veh.Properties.Type.ToString());
                        this.QueueListTop.Add(this.TickCount, (double)(CaftSettings.Default.CellSize_Height * (this.ExtraRoadCellsS - veh.CurrentPosition.Row)));
                    }

                    //MessageBox.Show(abc.Count().ToString() + ": " + cdf.CurrentPosition.Row.ToString() + "," + cdf.Properties.Type.ToString());  
                }
                _LaneSignalOn_Top = value;
            }
        }



        private bool _LaneSignalOn_Left;
        public bool LaneSignalOn_Left
        {
            get { return _LaneSignalOn_Left; }
            set
            {
                if (_LaneSignalOn_Left == false && value == true && this.TickCount > 0)
                {
                    var veh = this.VehicleListLeft
                        .Where(p => p.IsStoppedForSignal > 2
                            && p.Properties.Status == VehicleStatus.InProgress)
                            .OrderBy(q => q.CurrentPosition.Column).FirstOrDefault();

                    if (veh != null)
                    {
                        //MessageBox.Show((veh.CurrentPosition.Column + veh.Properties.RowOccupancy).ToString() + ": " + veh.Properties.Type.ToString());
                        this.QueueListLeft.Add(this.TickCount, (double)(CaftSettings.Default.CellSize_Height * ((this.ExtraRoadCellsLR - veh.CurrentPosition.Column) + veh.Properties.RowOccupancy)));
                    }

                    //MessageBox.Show(abc.Count().ToString() + ": " + cdf.CurrentPosition.Row.ToString() + "," + cdf.Properties.Type.ToString());  
                }
                _LaneSignalOn_Left = value;
            }
        }

        private bool _LaneSignalOn_Right;
        public bool LaneSignalOn_Right
        {
            get { return _LaneSignalOn_Right; }
            set
            {
                if (_LaneSignalOn_Right == false && value == true && this.TickCount > 0)
                {
                    var veh = this.VehicleListRight
                        .Where(p => p.IsStoppedForSignal > 2
                            && p.Properties.Status == VehicleStatus.InProgress)
                            .OrderByDescending(q => q.CurrentPosition.Column).FirstOrDefault();

                    if (veh != null)
                    {
                        //MessageBox.Show((veh.CurrentPosition.Column).ToString() + ": " + veh.Properties.Type.ToString());
                        this.QueueListRight.Add(this.TickCount, (double)(CaftSettings.Default.CellSize_Height * (veh.CurrentPosition.Column - this.ExtraRoadCellsLR - (this.TwoLaneColumnCount * 2))));
                    }

                    //MessageBox.Show(abc.Count().ToString() + ": " + cdf.CurrentPosition.Row.ToString() + "," + cdf.Properties.Type.ToString());  
                }
                _LaneSignalOn_Right = value;
            }
        }

        public bool StopExecution { get; set; }

        public int InProgress_BL { get; set; }
        public int InQueue_BL { get; set; }
        public int Completed_BL { get; set; }
        public int TotalVehicles_BL { get; set; }


        public int InProgress_LL { get; set; }
        public int InQueue_LL { get; set; }
        public int Completed_LL { get; set; }
        public int TotalVehicles_LL { get; set; }


        public int InProgress_TL { get; set; }
        public int InQueue_TL { get; set; }
        public int Completed_TL { get; set; }
        public int TotalVehicles_TL { get; set; }


        public int InProgress_RL { get; set; }
        public int InQueue_RL { get; set; }
        public int Completed_RL { get; set; }
        public int TotalVehicles_RL { get; set; }

        public int MaxColumn { get; set; }
        public int MinColumn { get; set; }

        public int MinimumColumn { get; set; }
        public int MaximumColumn { get; set; }
        //public int MinColumn { get; set; }

        public int TwoLaneColumnCount { get; set; }
        public int RowIntersection { get; set; }


        public int ExtraRoadCellsLR = 10;
        public int ExtraRoadCellsS = 10;

        public int TotalGridRowCount;
        public int TotalGridColumnCount;
        public int JumpTick = 5;

        //Variables for Chart
        public Dictionary<int, List<SpacePerTime>> spacePerTime = new Dictionary<int, List<SpacePerTime>>();
        public Dictionary<int, double> speedPerTime = new Dictionary<int, double>();
        public Dictionary<int, int> DensityPerTime = new Dictionary<int, int>();
        public Dictionary<int, int> DensityPerTimeBeforeBump = new Dictionary<int, int>();
        public Dictionary<int, int> DensityPerTimeAfterBump = new Dictionary<int, int>();
        public List<DensityPerSpeed> DensityPerSpeed = new List<DensityPerSpeed>();
        public List<DensityPerSpeed> VolumePerSpeed = new List<DensityPerSpeed>();
        public List<DensityPerSpeed> DensityPerSpeedBeforeBump = new List<DensityPerSpeed>();
        public List<DensityPerSpeed> DensityPerSpeedAfterBump = new List<DensityPerSpeed>();
        public Dictionary<int, int> QueueList = new Dictionary<int, int>();
        public Dictionary<int, double> QueueListBottom = new Dictionary<int, double>();
        public Dictionary<int, double> QueueListTop = new Dictionary<int, double>();
        public Dictionary<int, double> QueueListLeft = new Dictionary<int, double>();
        public Dictionary<int, double> QueueListRight = new Dictionary<int, double>();

        public Dictionary<int, NoiseProbabiltyFactorType> NoiseProbFactors = new Dictionary<int, NoiseProbabiltyFactorType>();

        //Variables for Unique Random Collections..
        public int[] rdTypeOfVehicles, rdTypeOfVehiclesTop, rdTypeOfVehiclesRight, rdTypeOfVehiclesLeft;
        public int[] rdVehicleDirectionRatio;

        public int TickCount = 0;
        public int AutoTickCount = 0;

        public int VhRangeRandom = 0;
        public int VhSession = 0;
        public int VhSessiongap = 0;
        public int[] VhAssignment, VhGenerated;

        public DateTime SimulationStartTime;
        public DateTime SimulationFinishTime;

    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int GridRowCount, TwoLaneColumnCount;
        int TimeStamp;

        DispatcherTimer dispatcherTimer;
        decimal cellheight = CaftSettings.Default.CellSize_Height;
        decimal cellwidth = CaftSettings.Default.CellSize_Width;
        decimal cellsize_pix = CaftSettings.Default.CellSize_InPixels;
        int tempGridRowcount;
        Program objProgram;

        GlobalVariables globalVariables;
        Functions functions;

        public MainWindow()
        {
            InitializeComponent();
            SetGlobals();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, TimeStamp);

            //File.Create("Logs.txt");

            MainScroll.ScrollToHorizontalOffset(600);

            if (CaftSettings.Default.LaneSignal)
            {
                btnLaneSignal.Content = "LEG-A Signal - ON";
                //GlobalVariables.LaneSignalOn = true;
                globalVariables.LaneSignalOn = true;
            }
            else
            {
                btnLaneSignal.Content = "LEG-A Signal - OFF";
                globalVariables.LaneSignalOn = false;
                globalVariables.LaneSignalOn_Left = false;
                globalVariables.LaneSignalOn_Right = false;
                globalVariables.LaneSignalOn_Top = false;

            }

            if ((bool)CaftSettings.Default.vhRangeInclude)
            {
                globalVariables.VhRangeRandom = functions.RandomNumberGenerator(CaftSettings.Default.VhRangeMin, CaftSettings.Default.VhRangeMax + 1);

                
                if (globalVariables.VhRangeRandom > 60)
                {
                    globalVariables.VhAssignment = functions.VehicleAssignmentSession(60, globalVariables.VhRangeRandom);
                    globalVariables.VhGenerated = new int[60];
                }
                else
                {
                    globalVariables.VhSessiongap = 60 / globalVariables.VhRangeRandom;
                    globalVariables.VhAssignment = functions.VehicleAssignmentSession(globalVariables.VhRangeRandom, globalVariables.VhRangeRandom);
                    globalVariables.VhGenerated = new int[globalVariables.VhRangeRandom];
                }
 
            }



            GenerateGrid();
        }


        private void SetGlobals()
        {
            globalVariables = new GlobalVariables();
            functions = new Functions(globalVariables);

            GridRowCount = CaftSettings.Default.GridRowCount;
            TimeStamp = Convert.ToInt32((Convert.ToDouble(CaftSettings.Default.TimeStamp) * 1000));
        }

        private void GenerateGrid()
        {

            lbltwoWheeler.Content = "Two Wheeler (Black - " + CaftSettings.Default.TwoVhSize_Row + " * " + CaftSettings.Default.TwoVhSize_Column + ")";
            lblthreeWheeler.Content = "Three Wheeler (Yellow - " + CaftSettings.Default.ThreeVhSize_Row + " * " + CaftSettings.Default.ThreeVhSize_Column + ")";
            lblFourWheeler.Content = "Four Wheeler (Red - " + CaftSettings.Default.FourVhSize_Row + " * " + CaftSettings.Default.FourVhSize_Column + ")";
            lblLCV1.Content = "LCV1 (Green - " + CaftSettings.Default.LCV1VhSize_Row + " * " + CaftSettings.Default.LCV1VhSize_Column + ")";
            lblLCV2.Content = "LCV2 (Yellow Green - " + CaftSettings.Default.LCV2VhSize_Row + " * " + CaftSettings.Default.LCV2VhSize_Column + ")";
            lblHCV1.Content = "HCV1 (Blue - " + CaftSettings.Default.HCV1VhSize_Row + " * " + CaftSettings.Default.HCV1VhSize_Column + ")";
            lblHCV2.Content = "HCV2 (Blue Violet - " + CaftSettings.Default.HCV2VhSize_Row + " * " + CaftSettings.Default.HCV2VhSize_Column + ")";


            decimal fixRoadWidth = CaftSettings.Default.RoadWidth;
            TwoLaneColumnCount = (int)(fixRoadWidth / cellwidth);
            decimal shoulder = (decimal)((fixRoadWidth * 10) % TwoLaneColumnCount) / 10; // (decimal)((fixRoadWidth / cellwidth) - (fixRoadWidth % GridColumnCount));
            TwoLaneColumnCount++;

            #region login for Signal Grid Generation
            int ExtraRoadCells = globalVariables.ExtraRoadCellsLR;
            int tempGridColumnCount = (TwoLaneColumnCount * 2) + (ExtraRoadCells * 2);
            tempGridRowcount = GridRowCount + (TwoLaneColumnCount * 2) + globalVariables.ExtraRoadCellsS;
            
            #endregion

            globalVariables.MaxColumn = ExtraRoadCells + TwoLaneColumnCount;
            globalVariables.MinColumn = ExtraRoadCells + 1;
            globalVariables.RowIntersection = tempGridRowcount - GridRowCount;
            globalVariables.TwoLaneColumnCount = TwoLaneColumnCount;
            globalVariables.TotalGridRowCount = tempGridRowcount;
            globalVariables.TotalGridColumnCount = tempGridColumnCount;

            #region Different Grids Commented Currently
            //int tempExtras = 50;

            //for (int i = 0; i < GridColumnCount * 2; i++)
            //{
            //    RowDefinition rd = new RowDefinition();

            //    MainGridLeft.RowDefinitions.Add(rd);

            //    for (int j = 0; j < tempExtras; j++)
            //    {
            //        ColumnDefinition cd = new ColumnDefinition();


            //        MainGridLeft.ColumnDefinitions.Add(cd);

            //        Border bd = new Border();
            //        bd.BorderBrush = new SolidColorBrush(Colors.Black);


            //        bd.BorderThickness = new Thickness(0.3);


            //        bd.SetValue(Grid.RowProperty, i);
            //        bd.SetValue(Grid.ColumnProperty, j);


            //        Rectangle rt = new Rectangle();

            //        rt.Width = Convert.ToInt32(cellheight * cellsize_pix);
            //        rt.Height = Convert.ToInt32(cellheight * cellsize_pix);

            //        bd.Child = rt;

            //        MainGridLeft.Children.Add(bd);
            //    }
            //}

            //double tempT = (double)(tempExtras * cellsize_pix) + (0.3 * 2 * tempExtras);
            //MainGridLeft.Margin = new Thickness(0, tempT, 0, 0);

            //for (int i = 0; i < tempExtras; i++)
            //{
            //    RowDefinition rd = new RowDefinition();

            //    MainGridTop.RowDefinitions.Add(rd);

            //    for (int j = 0; j < GridColumnCount * 2; j++)
            //    {
            //        ColumnDefinition cd = new ColumnDefinition();


            //        MainGridTop.ColumnDefinitions.Add(cd);

            //        Border bd = new Border();
            //        bd.BorderBrush = new SolidColorBrush(Colors.Black);


            //        bd.BorderThickness = new Thickness(0.3);


            //        bd.SetValue(Grid.RowProperty, i);
            //        bd.SetValue(Grid.ColumnProperty, j);


            //        Rectangle rt = new Rectangle();

            //        if (j == 0 || j == (GridColumnCount * 2) - 1)
            //        {
            //            rt.Width = Convert.ToInt32(shoulder * cellsize_pix); ;
            //        }
            //        else
            //        {
            //            rt.Width = Convert.ToInt32(cellwidth * cellsize_pix);
            //        }
            //        rt.Height = Convert.ToInt32(cellheight * cellsize_pix);

            //        bd.Child = rt;

            //        MainGridTop.Children.Add(bd);
            //    }
            //}

            //double tempW = (double)(cellsize_pix * globalVariables.ExtraRoadCellsLR) + (0.3 * 2 * globalVariables.ExtraRoadCellsLR);
            //MainGridTop.Margin = new Thickness(tempW, 0, 0, 0);
            #endregion

            for (int i = 0; i < tempGridRowcount; i++)
            {

                RowDefinition rd = new RowDefinition();

                MainGrid.RowDefinitions.Add(rd);

                for (int j = 0; j < tempGridColumnCount; j++)
                {
                    ColumnDefinition cd = new ColumnDefinition();


                    MainGrid.ColumnDefinitions.Add(cd);

                    Border bd = new Border();
                    bd.BorderBrush = new SolidColorBrush(Colors.Black);


                    bd.BorderThickness = new Thickness(0.3);


                    bd.SetValue(Grid.RowProperty, i);
                    bd.SetValue(Grid.ColumnProperty, j);


                    Rectangle rt = new Rectangle();

                    if ((j < ExtraRoadCells && (i < globalVariables.ExtraRoadCellsS || i > tempGridRowcount - GridRowCount - 1)) ||
                        (j > TwoLaneColumnCount + ExtraRoadCells - 1 && (i < globalVariables.ExtraRoadCellsS || i > tempGridRowcount - GridRowCount - 1)))
                    {
                        if (j < ExtraRoadCells || j > (TwoLaneColumnCount * 2) + ExtraRoadCells - 1)
                        {
                            bd.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        }
                        //else
                        //{
                        //    bd.BorderBrush = new SolidColorBrush(Colors.Red);
                        //}
                    }
                    else
                    {
                        if (j == ((tempGridColumnCount / 2) - 1))
                        {
                            bd.BorderThickness = new Thickness(0.3, 0.3, 2, 0.3);
                        }

                        if (i == globalVariables.ExtraRoadCellsS + TwoLaneColumnCount - 1)
                        {
                            if (j == ((tempGridColumnCount / 2) - 1))
                            {
                                bd.BorderThickness = new Thickness(0.3, 0.3, 2, 2);
                            }
                            else
                            {
                                bd.BorderThickness = new Thickness(0.3, 0.3, 0.3, 2);
                            }

                        }

                        //Setting color of bump to make it visible to user
                        if (CaftSettings.Default.bumpInclude)
                        {
                            int cellsToShowAsBump = ((int)Math.Ceiling(3 / CaftSettings.Default.CellSize_Height)) - 1;

                            if (i >= CaftSettings.Default.BumpLine && i <= CaftSettings.Default.BumpLine + cellsToShowAsBump)
                            {
                                bd.BorderBrush = new SolidColorBrush(Colors.Blue);

                                if (i == CaftSettings.Default.BumpLine)
                                {
                                    bd.BorderThickness = new Thickness(0.3, 2, 0.3, 0.3);
                                }
                                if (i == CaftSettings.Default.BumpLine + cellsToShowAsBump)
                                {
                                    bd.BorderThickness = new Thickness(0.3, 0.3, 0.3, 2);
                                }
                            }
                        }

                        if (CaftSettings.Default.signalInclude)
                        {
                            if (i == CaftSettings.Default.BumpLine)
                            {
                                bd.BorderBrush = new SolidColorBrush(Colors.Red);
                                bd.BorderThickness = new Thickness(0, 2, 0, 0);
                            }
                        }

                        if (i == CaftSettings.Default.headway)
                        {
                            bd.BorderThickness = new Thickness(0.3, 2, 0.3, 2);
                            bd.BorderBrush = new SolidColorBrush(Colors.Brown);
                        }
                    }

                    if (j < ExtraRoadCells || j > (TwoLaneColumnCount * 2) + ExtraRoadCells - 1)
                    {
                        rt.Height = Convert.ToInt32(cellwidth * cellsize_pix);
                        rt.Width = Convert.ToInt32(cellheight * cellsize_pix);
                    }
                    else
                    {
                        if (j == 0 + ExtraRoadCells || j == (TwoLaneColumnCount * 2) + ExtraRoadCells - 1)
                        {
                            rt.Width = Convert.ToInt32(shoulder * cellsize_pix);
                            rt.Fill = new SolidColorBrush(Colors.DimGray);
                        }
                        else
                        {
                            rt.Width = Convert.ToInt32(cellwidth * cellsize_pix);
                        }

                        rt.Height = Convert.ToInt32(cellheight * cellsize_pix);
                    }
                    bd.Child = rt;

                    MainGrid.Children.Add(bd);

                }
            }
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //File.AppendAllText("Logs.txt", "===DispatcherTimer_Tick===\r\n");

            objProgram.Flow(MainGrid);
            if (btnStart.Content.ToString() == "Finish")
            {
                if (globalVariables.InProgress_BL == 0
                    && globalVariables.InQueue_BL == 0 && globalVariables.Completed_BL > 0)
                {
                    dispatcherTimer.Stop();
                    globalVariables.SimulationFinishTime = DateTime.Now;
                }
            }

            if ((bool)CaftSettings.Default.vhRangeInclude)
            {
                //if (CaftSettings.Default.lefttraffic)
                //{
                //    if (globalVariables.TotalVehicles_LL > globalVariables.VhRangeRandom)
                //    {
                //        globalVariables.StopExecution = true;
                //        btnStart.Content = "Finish";
                //    }
                //}
                //if (CaftSettings.Default.righttraffic)
                //{
                //    if (globalVariables.TotalVehicles_RL > globalVariables.VhRangeRandom)
                //    {
                //        globalVariables.StopExecution = true;
                //        btnStart.Content = "Finish";
                //    }
                //}
                if (CaftSettings.Default.bottomtraffic)
                {
                    if (globalVariables.TotalVehicles_BL > globalVariables.VhRangeRandom)
                    {
                        globalVariables.StopExecution = true;
                        btnStart.Content = "Finish";
                    }
                }
                //if (CaftSettings.Default.toptraffic)
                //{
                //    if (globalVariables.TotalVehicles_TL > globalVariables.VhRangeRandom)
                //    {
                //        globalVariables.StopExecution = true;
                //        btnStart.Content = "Finish";
                //    }
                //}
            }

            // AutoSignal
            if (CaftSettings.Default.AutoSignalInclude)
            {
                btnLSLaneSignal.Visibility = Visibility.Collapsed;
                btnRSLaneSignal.Visibility = Visibility.Collapsed;
                btnTSLaneSignal.Visibility = Visibility.Collapsed;


                int RedCount = (int)CaftSettings.Default.AutoRedSignalTime;
                int GreenCount_Bottom = (int)CaftSettings.Default.AutoGreenSignalTime_Bottom;
                int GreenCount_Left = (int)CaftSettings.Default.AutoGreenSignalTime_Left;
                int GreenCount_Top = (int)CaftSettings.Default.AutoGreenSignalTime_Top;
                int GreenCount_Right = (int)CaftSettings.Default.AutoGreenSignalTime_Right;

                int lanesignalindicator = 0;

                //int bottomsignal = GreenCount_Bottom;
                //int leftsignal = bottomsignal + GreenCount_Left;
                //int topsignal = leftsignal + GreenCount_Top;
                //int rightsignal = topsignal + GreenCount_Right;

                
                int AmberTime = CaftSettings.Default.CrossRoadSignalAmberTime;

                if (globalVariables.AutoTickCount <= GreenCount_Bottom)
                {
                    lanesignalindicator = 0;
                }
                else if (globalVariables.AutoTickCount > GreenCount_Bottom && globalVariables.AutoTickCount <= GreenCount_Bottom + AmberTime)
                {
                    lanesignalindicator = 4;
                }
                else if (globalVariables.AutoTickCount > GreenCount_Bottom + AmberTime && globalVariables.AutoTickCount <= GreenCount_Bottom + AmberTime + GreenCount_Left)
                {
                    lanesignalindicator = 1;
                }
                else if (globalVariables.AutoTickCount > GreenCount_Bottom + AmberTime + GreenCount_Left && globalVariables.AutoTickCount <= GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime)
                {
                    lanesignalindicator = 4;
                }
                else if (globalVariables.AutoTickCount > GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime && globalVariables.AutoTickCount <= GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime + GreenCount_Top)
                {
                    lanesignalindicator = 2;
                }
                else if (globalVariables.AutoTickCount > GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime + GreenCount_Top && globalVariables.AutoTickCount <= GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime + GreenCount_Top + AmberTime)
                {
                    lanesignalindicator = 4;
                }
                else if (globalVariables.AutoTickCount > GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime + GreenCount_Top + AmberTime && globalVariables.AutoTickCount <= GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime + GreenCount_Top + AmberTime + GreenCount_Right)
                {
                    lanesignalindicator = 3;
                }
                else if (globalVariables.AutoTickCount > GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime + GreenCount_Top + AmberTime + GreenCount_Right && globalVariables.AutoTickCount <= GreenCount_Bottom + AmberTime + GreenCount_Left + AmberTime + GreenCount_Top + AmberTime + GreenCount_Right + AmberTime)
                {
                    lanesignalindicator = 4;
                }
                else
                {
                    lanesignalindicator = 0;
                    globalVariables.AutoTickCount = 1;
                }

                AutoSignal_Enable(lanesignalindicator);
            }
            
            globalVariables.TickCount++;

            lblCompleted_BL.Content = "Completed - " + globalVariables.Completed_BL.ToString();
            lblInProgress_BL.Content = "In Progress - " + globalVariables.InProgress_BL.ToString();
            lblInQueue_BL.Content = "In Queue - " + globalVariables.InQueue_BL.ToString();
            lblTotal_BL.Content = "Total Vehicles - " + globalVariables.TotalVehicles_BL.ToString();



            lblCompleted_LL.Content = "Completed - " + globalVariables.Completed_LL.ToString();
            lblInProgress_LL.Content = "In Progress - " + globalVariables.InProgress_LL.ToString();
            lblInQueue_LL.Content = "In Queue - " + globalVariables.InQueue_LL.ToString();
            lblTotal_LL.Content = "Total Vehicles - " + globalVariables.TotalVehicles_LL.ToString();


            lblCompleted_TL.Content = "Completed - " + globalVariables.Completed_TL.ToString();
            lblInProgress_TL.Content = "In Progress - " + globalVariables.InProgress_TL.ToString();
            lblInQueue_TL.Content = "In Queue - " + globalVariables.InQueue_TL.ToString();
            lblTotal_TL.Content = "Total Vehicles - " + globalVariables.TotalVehicles_TL.ToString();


            lblCompleted_RL.Content = "Completed - " + globalVariables.Completed_RL.ToString();
            lblInProgress_RL.Content = "In Progress - " + globalVariables.InProgress_RL.ToString();
            lblInQueue_RL.Content = "In Queue - " + globalVariables.InQueue_RL.ToString();
            lblTotal_RL.Content = "Total Vehicles - " + globalVariables.TotalVehicles_RL.ToString();

            lblTickCount.Content = "Time in seconds  - " + globalVariables.TickCount.ToString();

            //lblConsole.Content = globalVariables.VehicleList.Where(p => p.CurrentPosition.Row > globalVariables.RowIntersection - 1 && p.CurrentPosition.Row < globalVariables.TotalGridRowCount).Count().ToString();
        }

        private void AutoSignal_Enable(int lanesignalindic)
        {
            switch (lanesignalindic)
            {   
                case 0:
                        btnLaneSignal.Content = "LEG-A Signal On";
                        globalVariables.LaneSignalOn = true;
                        globalVariables.LaneSignalOn_Left = false;
                        globalVariables.LaneSignalOn_Top = false;
                        globalVariables.LaneSignalOn_Right = false;
                    break;

                case 1:
                        btnLaneSignal.Content = "LEG-B Signal On";
                        globalVariables.LaneSignalOn = false;
                        globalVariables.LaneSignalOn_Left = true;
                        globalVariables.LaneSignalOn_Top = false;
                        globalVariables.LaneSignalOn_Right = false;
                    break;

                case 2:
                        btnLaneSignal.Content = "LEG-C Signal On";
                        globalVariables.LaneSignalOn = false;
                        globalVariables.LaneSignalOn_Left = false;
                        globalVariables.LaneSignalOn_Top = true;
                        globalVariables.LaneSignalOn_Right = false;
                        break;

                case 3:
                        btnLaneSignal.Content = "LEG-D Signal On";
                        globalVariables.LaneSignalOn = false;
                        globalVariables.LaneSignalOn_Left = false;
                        globalVariables.LaneSignalOn_Top = false;
                        globalVariables.LaneSignalOn_Right = true;
                    break;

                default:
                        btnLaneSignal.Content = "Yellow Light";
                        globalVariables.LaneSignalOn = false;
                        globalVariables.LaneSignalOn_Left = false;
                        globalVariables.LaneSignalOn_Top = false;
                        globalVariables.LaneSignalOn_Right = false;
                    break;
            }

            globalVariables.AutoTickCount += 1;
        }


        private void Button_Start(object sender, RoutedEventArgs e)
        {
            #region manual creation of vehicle - commented now

            //globalVariables.VehicleListLeft.Add(new Vehicle()
            //{
            //    Id = 1,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(70, 0),
            //    CurrentCellSpeed = 5,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 4, 2, 44),
            //});

            //globalVariables.VehicleListLeft.Add(new Vehicle()
            //{
            //    Id = 2,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(70, 0),
            //    CurrentCellSpeed = 2,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 4, 2, 44),
            //});

            //globalVariables.VehicleListRight.Add(new Vehicle()
            //{
            //    Id = 2,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(70, 55),
            //    CurrentCellSpeed = 2,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 4, 2, 44),
            //});

            //globalVariables.VehicleListTop.Add(new Vehicle()
            //{
            //    Id = 2,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(0, 30),
            //    CurrentCellSpeed = 5,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 4, 2, 44),
            //});

            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 2,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(globalVariables.TotalGridRowCount, 18),
            //    CurrentCellSpeed = 10,
            //    Properties = new VehicleProperties(VehicleType.LCV1, 4, 2, 44),
            //});

            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 3,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(globalVariables.TotalGridRowCount, 11),
            //    CurrentCellSpeed = 12,
            //    Properties = new VehicleProperties(VehicleType.ThreeWheel, 4, 2, 44),
            //});

            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 4,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(globalVariables.TotalGridRowCount, 16),
            //    CurrentCellSpeed = 14,
            //    Properties = new VehicleProperties(VehicleType.FourWheel, 4, 2, 44),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 3,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(globalVariables.TotalGridRowCount, 13),
            //    CurrentCellSpeed = 6,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 4, 2, 44),
            //});

            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 4,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(globalVariables.TotalGridRowCount, 14),
            //    CurrentCellSpeed = 2,
            //    Properties = new VehicleProperties(VehicleType.LCV2, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 5,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(globalVariables.TotalGridRowCount, 13),
            //    CurrentCellSpeed = 3,
            //    Properties = new VehicleProperties(VehicleType.LCV1, 3, 2, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 6,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(globalVariables.TotalGridRowCount, 12),
            //    CurrentCellSpeed = 2,
            //    Properties = new VehicleProperties(VehicleType.FourWheel, 2, 2, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 4,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 0),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 5,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 0),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 6,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 0),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 7,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 0),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 8,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 0),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 9,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 0),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 10,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 0),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 11,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 0),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});


            //globalVariables.VehicleList.Add(new Vehicle()
            //{
            //    Id = 12,
            //    Direction = DirectionType.Straight,
            //    CurrentPosition = new CurrentPositionType(CaftSettings.Default.GridRowCount, 12),
            //    CurrentCellSpeed = 1,
            //    Properties = new VehicleProperties(VehicleType.TwoWheel, 1, 1, 3),
            //});
            #endregion


            globalVariables.rdTypeOfVehicles = functions.GetUniqueRandoms(1, 1001);
            globalVariables.rdTypeOfVehiclesTop = functions.GetUniqueRandoms(1, 1001);
            globalVariables.rdTypeOfVehiclesRight = functions.GetUniqueRandoms(1, 1001);
            globalVariables.rdTypeOfVehiclesLeft = functions.GetUniqueRandoms(1, 1001);
            globalVariables.rdVehicleDirectionRatio = functions.GetUniqueRandoms(1, 101);

            objProgram = new Program(1000, tempGridRowcount, globalVariables, functions);


            objProgram.TwoWheelRatio = CaftSettings.Default.percentTwoVh;
            objProgram.ThreeWheelRatio = CaftSettings.Default.percentTwoVh + CaftSettings.Default.percentThreeVh;
            objProgram.FourWheenRatio = CaftSettings.Default.percentTwoVh + CaftSettings.Default.percentThreeVh + CaftSettings.Default.percentFourVh;
            objProgram.LCVRatio = CaftSettings.Default.percentTwoVh + CaftSettings.Default.percentThreeVh + CaftSettings.Default.percentFourVh + CaftSettings.Default.percentLCV1 + CaftSettings.Default.percentLCV2;
            objProgram.HCVRatio = CaftSettings.Default.percentTwoVh + CaftSettings.Default.percentThreeVh + CaftSettings.Default.percentFourVh + CaftSettings.Default.percentLCV1 + CaftSettings.Default.percentLCV2 + CaftSettings.Default.percentHCV1 + CaftSettings.Default.percentHCV2;

            objProgram.StraightRatio = CaftSettings.Default.percentStraight;
            objProgram.RightRatio = CaftSettings.Default.percentStraight + CaftSettings.Default.percentRight;
            objProgram.LeftRatio = CaftSettings.Default.percentStraight + CaftSettings.Default.percentRight + CaftSettings.Default.percentLeft;

            globalVariables.ActualLegSelected = "Leg-A";

            if (CaftSettings.Default.lefttraffic)
            {
                //RotateTransform rotateTR = new RotateTransform(90);
                //MainGrid.LayoutTransform = rotateTR;
                globalVariables.ActualLegSelected = "Leg-B";

                var tempTime = CaftSettings.Default.AutoGreenSignalTime_Bottom;
                CaftSettings.Default.AutoGreenSignalTime_Bottom = CaftSettings.Default.AutoGreenSignalTime_Left;
                CaftSettings.Default.AutoGreenSignalTime_Left = CaftSettings.Default.AutoGreenSignalTime_Top;
                CaftSettings.Default.AutoGreenSignalTime_Top = CaftSettings.Default.AutoGreenSignalTime_Right;
                CaftSettings.Default.AutoGreenSignalTime_Right = tempTime;

                globalVariables.AutoTickCount = globalVariables.AutoTickCount
                                + CaftSettings.Default.AutoGreenSignalTime_Bottom + 4
                                + CaftSettings.Default.AutoGreenSignalTime_Top + 4
                                + CaftSettings.Default.AutoGreenSignalTime_Right + 4;

                CaftSettings.Default.bottomtraffic = true;
                CaftSettings.Default.lefttraffic = false;
                CaftSettings.Default.toptraffic = false;
                CaftSettings.Default.righttraffic = false;

                //objProgram.TwoWheelRatio = 65;
                //objProgram.ThreeWheelRatio = 65 + 19;
                //objProgram.FourWheenRatio = 65 + 19 + 16;
                //objProgram.LCVRatio = 65 + 19 + 16 + 1;

                //objProgram.StraightRatio = 65;
                //objProgram.RightRatio = 65 + 25;
                //objProgram.LeftRatio = 65 + 25 + 10;

            }

            if (CaftSettings.Default.toptraffic)
            {
                //RotateTransform rotateTR = new RotateTransform(180);
                //MainGrid.LayoutTransform = rotateTR;
                globalVariables.ActualLegSelected = "Leg-C";

                var tempTime = CaftSettings.Default.AutoGreenSignalTime_Bottom;
                var tempTime1 = CaftSettings.Default.AutoGreenSignalTime_Left;

                CaftSettings.Default.AutoGreenSignalTime_Bottom = CaftSettings.Default.AutoGreenSignalTime_Top;
                CaftSettings.Default.AutoGreenSignalTime_Left = CaftSettings.Default.AutoGreenSignalTime_Right;
                CaftSettings.Default.AutoGreenSignalTime_Top = tempTime;
                CaftSettings.Default.AutoGreenSignalTime_Right = tempTime1;

                globalVariables.AutoTickCount = globalVariables.AutoTickCount
                                + CaftSettings.Default.AutoGreenSignalTime_Bottom + 4
                                + CaftSettings.Default.AutoGreenSignalTime_Left + 4;

                CaftSettings.Default.bottomtraffic = true;
                CaftSettings.Default.lefttraffic = false;
                CaftSettings.Default.toptraffic = false;
                CaftSettings.Default.righttraffic = false;

                //objProgram.TwoWheelRatio = 52;
                //objProgram.ThreeWheelRatio = 52 + 21;
                //objProgram.FourWheenRatio = 52 + 21 + 26;
                //objProgram.LCVRatio = 52 + 21 + 26 + 1;
                

                //objProgram.StraightRatio = 56;
                //objProgram.RightRatio = 56 + 26;
                //objProgram.LeftRatio = 56 + 26 + 18;
            }

            if (CaftSettings.Default.righttraffic)
            {
                //RotateTransform rotateTR = new RotateTransform(270);
                //MainGrid.LayoutTransform = rotateTR;
                globalVariables.ActualLegSelected = "Leg-D";

                var tempTime = CaftSettings.Default.AutoGreenSignalTime_Bottom;

                CaftSettings.Default.AutoGreenSignalTime_Bottom = CaftSettings.Default.AutoGreenSignalTime_Right;
                CaftSettings.Default.AutoGreenSignalTime_Right = CaftSettings.Default.AutoGreenSignalTime_Top;
                CaftSettings.Default.AutoGreenSignalTime_Top = CaftSettings.Default.AutoGreenSignalTime_Left;
                CaftSettings.Default.AutoGreenSignalTime_Left = tempTime;


                globalVariables.AutoTickCount = globalVariables.AutoTickCount
                                + CaftSettings.Default.AutoGreenSignalTime_Bottom + 4;

                CaftSettings.Default.bottomtraffic = true;
                CaftSettings.Default.lefttraffic = false;
                CaftSettings.Default.toptraffic = false;
                CaftSettings.Default.righttraffic = false;

                //objProgram.TwoWheelRatio = 49;
                //objProgram.ThreeWheelRatio = 49 + 19;
                //objProgram.FourWheenRatio = 49 + 19 + 32;
                //objProgram.LCVRatio = 49 + 19 + 32 + 1;

                //objProgram.StraightRatio = 55;
                //objProgram.RightRatio = 55 + 30;
                //objProgram.LeftRatio = 55 + 30 + 15;
            }



            //globalVariables.TickCount = 0;
            
            dispatcherTimer.Start();
            if (btnStart.Content.ToString() == "Start")
            {
                globalVariables.SimulationStartTime = DateTime.Now;
                btnStart.Content = "Stop";
                globalVariables.StopExecution = false;
            }
            else if (btnStart.Content.ToString() == "Stop")
            {
                globalVariables.StopExecution = true;
                btnStart.Content = "Finish";
            }
            else if (btnStart.Content.ToString() == "Finish")
            {
                this.Close();
            }
        }

        private void Button_OnOff(object sender, RoutedEventArgs e)
        {
            if (btnLaneSignal.Content.ToString().ToUpper() == "LEG-A SIGNAL - ON")
            {
                btnLaneSignal.Content = "LEG-A Signal - OFF";
                globalVariables.LaneSignalOn = false;
            }
            else
            {
                btnLaneSignal.Content = "LEG-A Signal - ON";
                globalVariables.LaneSignalOn = true;
            }
        }

        private void LSButton_OnOff(object sender, RoutedEventArgs e)
        {
            if (btnLSLaneSignal.Content.ToString().ToUpper() == "LEG-B SIGNAL - ON")
            {
                btnLSLaneSignal.Content = "LEG-B Signal - OFF";
                globalVariables.LaneSignalOn_Left = false;
            }
            else
            {
                btnLSLaneSignal.Content = "LEG-B Signal - ON";
                globalVariables.LaneSignalOn_Left = true;
            }
        }

        private void RSButton_OnOff(object sender, RoutedEventArgs e)
        {
            if (btnRSLaneSignal.Content.ToString().ToUpper() == "LEG-D SIGNAL - ON")
            {
                btnRSLaneSignal.Content = "LEG-D Signal - OFF";
                globalVariables.LaneSignalOn_Right = false;
            }
            else
            {
                btnRSLaneSignal.Content = "LEG-D Signal - ON";
                globalVariables.LaneSignalOn_Right = true;
            }
        }

        private void TSButton_OnOff(object sender, RoutedEventArgs e)
        {
            if (btnTSLaneSignal.Content.ToString().ToUpper() == "LEG-C SIGNAL - ON")
            {
                btnTSLaneSignal.Content = "LEG-C Signal - OFF";
                globalVariables.LaneSignalOn_Top = false;
            }
            else
            {
                btnTSLaneSignal.Content = "LEG-C Signal - ON";
                globalVariables.LaneSignalOn_Top = true;
            }
        }

        private void btnShowChart_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            if (globalVariables.SimulationFinishTime == DateTime.MinValue) globalVariables.SimulationFinishTime = DateTime.Now;
            Summary sum = new Summary(tempGridRowcount, globalVariables);
            sum.Show();
        }

        private void Setings_Click(object sender, RoutedEventArgs e)
        {
            Settings set = new Settings();
            set.Show();
        }
    }
}
