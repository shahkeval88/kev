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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CAFT
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();



            // Setting LaneSignal
            if (CaftSettings.Default.LaneSignal)
            {
                rdbSettings_On.IsChecked = true;
                rdbSettings_Off.IsChecked = false;
            }
            else
            {
                rdbSettings_On.IsChecked = false;
                rdbSettings_Off.IsChecked = true;
            }

            // Time Stamp (in seconds)
            txtSetting_TimeStamp.Text = CaftSettings.Default.TimeStamp.ToString();

            // Total Grids
            txtSetting_GridCounts.Text = CaftSettings.Default.GridRowCount.ToString();

            // Roadwidth
            txtSetting_RoadWidth.Text = CaftSettings.Default.RoadWidth.ToString();

            // Cell Size (in meter)
            txtSetting_CellHeight.Text = CaftSettings.Default.CellSize_Height.ToString();
            txtSetting_CellWidth.Text = CaftSettings.Default.CellSize_Width.ToString();
            txtSetting_CellPixels.Text = CaftSettings.Default.CellSize_InPixels.ToString();

            // Vehicle Dimensions in meter
            // Two wheeler
            txtSetting_2WheelLength.Text = CaftSettings.Default.TwoVhSize_Row.ToString();
            txtSetting_2WheelWidth.Text = CaftSettings.Default.TwoVhSize_Column.ToString();
            txtSetting_2WheelAcc.Text = CaftSettings.Default.TwoVh_MaxAccelerationInKm.ToString();
            txtSetting_2WheelAccParam.Text = CaftSettings.Default.TwoVhAccParamInkm.ToString();
            txtSetting_2WheelPercent.Text = CaftSettings.Default.percentTwoVh.ToString();
            chk2Wheel.IsChecked = CaftSettings.Default.TwoVhInclude;

            // Three wheeler
            txtSetting_3WheelLength.Text = CaftSettings.Default.ThreeVhSize_Row.ToString();
            txtSetting_3WheelWidth.Text = CaftSettings.Default.ThreeVhSize_Column.ToString();
            txtSetting_3WheelAcc.Text = CaftSettings.Default.ThreeVh_MaxAccelerationInKm.ToString();
            txtSetting_3WheelAccParam.Text = CaftSettings.Default.ThreeVhAccParamInkm.ToString();
            txtSetting_3WheelPercent.Text = CaftSettings.Default.percentThreeVh.ToString();
            chk3Wheel.IsChecked = CaftSettings.Default.ThreeVhInclude;

            // Four wheeler
            txtSetting_4WheelLength.Text = CaftSettings.Default.FourVhSize_Row.ToString();
            txtSetting_4WheelWidth.Text = CaftSettings.Default.FourVhSize_Column.ToString();
            txtSetting_4WheelAcc.Text = CaftSettings.Default.FourVh_MaxAccelerationInKm.ToString();
            txtSetting_4WheelAccParam.Text = CaftSettings.Default.FourVhAccParamInkm.ToString();
            txtSetting_4WheelPercent.Text = CaftSettings.Default.percentFourVh.ToString();
            chk4Wheel.IsChecked = CaftSettings.Default.FourVhInclude;

            // LCV1 wheeler
            txtSetting_LCV1WheelLength.Text = CaftSettings.Default.LCV1VhSize_Row.ToString();
            txtSetting_LCV1WheelWidth.Text = CaftSettings.Default.LCV1VhSize_Column.ToString();
            txtSetting_LCV1WheelAcc.Text = CaftSettings.Default.LCV1Vh_MaxAccelerationInKm.ToString();
            txtSetting_LCV1WheelAccParam.Text = CaftSettings.Default.LCV1VhAccParamInkm.ToString();
            txtSetting_LCV1WheelPercent.Text = CaftSettings.Default.percentLCV1.ToString();
            chkLCV1Wheel.IsChecked = CaftSettings.Default.LCV1VhInclude;

            // LCV2 wheeler
            txtSetting_LCV2WheelLength.Text = CaftSettings.Default.LCV2VhSize_Row.ToString();
            txtSetting_LCV2WheelWidth.Text = CaftSettings.Default.LCV2VhSize_Column.ToString();
            txtSetting_LCV2WheelAcc.Text = CaftSettings.Default.LCV2Vh_MaxAccelerationInKm.ToString();
            txtSetting_LCV2WheelAccParam.Text = CaftSettings.Default.LCV2VhAccParamInkm.ToString();
            txtSetting_LCV2WheelPercent.Text = CaftSettings.Default.percentLCV2.ToString();
            chkLCV2Wheel.IsChecked = CaftSettings.Default.LCV2VhInclude;

            // HCV1 wheeler
            txtSetting_HCV1WheelLength.Text = CaftSettings.Default.HCV1VhSize_Row.ToString();
            txtSetting_HCV1WheelWidth.Text = CaftSettings.Default.HCV1VhSize_Column.ToString();
            txtSetting_HCV1WheelAcc.Text = CaftSettings.Default.HCV1Vh_MaxAccelerationInKm.ToString();
            txtSetting_HCV1WheelAccParam.Text = CaftSettings.Default.HCV1VhAccParamInkm.ToString();
            txtSetting_HCV1WheelPercent.Text = CaftSettings.Default.percentHCV1.ToString();
            chkHCV1Wheel.IsChecked = CaftSettings.Default.HCV1VhInclude;

            // HCV2 wheeler
            txtSetting_HCV2WheelLength.Text = CaftSettings.Default.HCV2VhSize_Row.ToString();
            txtSetting_HCV2WheelWidth.Text = CaftSettings.Default.HCV2VhSize_Column.ToString();
            txtSetting_HCV2WheelAcc.Text = CaftSettings.Default.HCV2Vh_MaxAccelerationInKm.ToString();
            txtSetting_HCV2WheelAccParam.Text = CaftSettings.Default.HCV2VhAccParamInkm.ToString();
            txtSetting_HCV2WheelPercent.Text = CaftSettings.Default.percentHCV2.ToString();
            chkHCV2Wheel.IsChecked = CaftSettings.Default.HCV2VhInclude;

            // Acceleration
            txtSetting_OvertakingGap.Text = CaftSettings.Default.Vh_OvertakingGap.ToString();
            txtSetting_AccelerationGap.Text = CaftSettings.Default.Vh_AccelerationGap.ToString();

            // Headway
            txtSetting_Headway.Text = CaftSettings.Default.headway.ToString();

            //BumpLine
            txtSetting_BumpLine.Text = CaftSettings.Default.BumpLine.ToString();
            txtSetting_BumpArea.Text = CaftSettings.Default.bumpArea.ToString();

            //DelayDistance
            txtSetting_DelayDistance.Text = CaftSettings.Default.DelayDistance.ToString();

            // include bump or not
            chkbump.IsChecked = CaftSettings.Default.bumpInclude;

            //Percentage Distribution in Straight, Left and Right
            txtSetting_PercentStaright.Text = CaftSettings.Default.percentStraight.ToString();
            txtSetting_PercentRight.Text = CaftSettings.Default.percentRight.ToString();
            txtSetting_PercentLeft.Text = CaftSettings.Default.percentLeft.ToString();

            //Or include signal
            chkSignal.IsChecked = CaftSettings.Default.signalInclude;
            txtSetting_SGreenTime.Text = CaftSettings.Default.GreenSignalTime.ToString();
            txtSetting_SGreenTimeLeft.Text = CaftSettings.Default.GreenSignalTimeLeft.ToString();
            txtSetting_SGreenTimeTop.Text = CaftSettings.Default.GreenSignalTimeTop.ToString();
            txtSetting_SGreenTimeRight.Text = CaftSettings.Default.GreenSignalTimeRight.ToString();
            //txtSetting_SRedTime.Text = CaftSettings.Default.RedSignalTime.ToString();
            txtSetting_SAmberTime.Text = CaftSettings.Default.AmberSignalTime.ToString();

            List<LegType> allLegs = new List<LegType>();
            allLegs.Add(new LegType() { Id = 1, Text = "Bottom" });
            allLegs.Add(new LegType() { Id = 2, Text = "Left" });
            allLegs.Add(new LegType() { Id = 3, Text = "Top" });
            allLegs.Add(new LegType() { Id = 4, Text = "Right" });

            cmbFirstSignalLeg.ItemsSource = allLegs;
            cmbFirstSignalLeg.DisplayMemberPath = "Text";
            cmbFirstSignalLeg.SelectedItem = allLegs.Where(p => p.Id == CaftSettings.Default.SignalFirstLegSelection).First();



            chkSignal_Checked(null, null);



            // Automatic Signal On - Off
            chkAutoSignal.IsChecked = CaftSettings.Default.AutoSignalInclude;
            txtSetting_AutoGreenTime_Bottom.Text = CaftSettings.Default.AutoGreenSignalTime_Bottom.ToString();
            txtSetting_AutoGreenTime_Left.Text = CaftSettings.Default.AutoGreenSignalTime_Left.ToString();
            txtSetting_AutoGreenTime_Top.Text = CaftSettings.Default.AutoGreenSignalTime_Top.ToString();
            txtSetting_AutoGreenTime_Right.Text = CaftSettings.Default.AutoGreenSignalTime_Right.ToString();
            txtSetting_CrossRoadSignalAmberTime.Text = CaftSettings.Default.CrossRoadSignalAmberTime.ToString();
            txtSetting_IntersectionSpeedZone.Text = CaftSettings.Default.IntersectionSpeedZone.ToString();

            if (!(bool)chkAutoSignal.IsChecked)
            {
                stSAutoGreen.Visibility = System.Windows.Visibility.Hidden;
                lblIntersectionSpeedZone1.Visibility = System.Windows.Visibility.Hidden;
                lblIntersectionSpeedZone2.Visibility = System.Windows.Visibility.Hidden;
                txtSetting_IntersectionSpeedZone.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                stSAutoGreen.Visibility = System.Windows.Visibility.Visible;
                lblIntersectionSpeedZone1.Visibility = System.Windows.Visibility.Visible;
                lblIntersectionSpeedZone2.Visibility = System.Windows.Visibility.Visible;
                txtSetting_IntersectionSpeedZone.Visibility = System.Windows.Visibility.Visible;
            }

            //txtSetting_AutoRedTime.Text = CaftSettings.Default.AutoRedSignalTime.ToString();

            chkLStraffic.IsChecked = CaftSettings.Default.lefttraffic;
            chkRStraffic.IsChecked = CaftSettings.Default.righttraffic;
            chkTStraffic.IsChecked = CaftSettings.Default.toptraffic;
            chkBStraffic.IsChecked = CaftSettings.Default.bottomtraffic;



            // Vehicle Range Include
            chkVhRange.IsChecked = CaftSettings.Default.vhRangeInclude;
            txtSetting_vhRangeMin.Text = CaftSettings.Default.VhRangeMin.ToString();
            txtSetting_VhRangeMax.Text = CaftSettings.Default.VhRangeMax.ToString();
            txtSetting_VhRangeTime.Text = CaftSettings.Default.VhRangeTime;

            //Noise Probability Factor
            txtSetting_NoiseProbFactor.Text = CaftSettings.Default.NoiseProbFactor.ToString();

            if (!(bool)chkVhRange.IsChecked)
            {
                txtSetting_vhRangeMin.Visibility = Visibility.Collapsed;
                txtSetting_VhRangeMax.Visibility = Visibility.Collapsed;
                txtSetting_VhRangeTime.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtSetting_vhRangeMin.Visibility = Visibility.Visible;
                txtSetting_VhRangeMax.Visibility = Visibility.Visible;
                txtSetting_VhRangeTime.Visibility = Visibility.Visible;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // ... Get RadioButton reference.
            var button = sender as RadioButton;

            // ... Display button content as title.
            this.Title = button.Content.ToString();
        }

        private void btnSetting_Save_Click(object sender, RoutedEventArgs e)
        {

            // Now, we need to save the settings file. If we don't save it, they
            // disappear. By saving it, we will be able to use all the settings exactly
            // as they are now, the next time the program is run.

            //Declaring fix size of the vehicles
            double TwoWhWidth = 0.6, TwoWhHeight = 1.8;
            double ThreeWhWidth = 1.4, ThreeWhHeight = 2.6;
            double FourWhWidth = 1.7, FourWhHeight = 4.7;
            double LCV1Width = 1.9, LCV1Height = 5.0;
            double LCV2Width = 2.2, LCV2Height = 6.8;
            double HCV1Width = 2.5, HCV1Height = 8.5;
            double HCV2Width = 2.5, HCV2Height = 10.3;


            if ((bool)chkbump.IsChecked && (bool)chkSignal.IsChecked)
            {
                CaftSettings.Default.bumpInclude = false;
                //return;
            }

            // Setting LaneSignal
            if (rdbSettings_On.IsChecked == true)
            {
                CaftSettings.Default.LaneSignal = true;
            }
            else
            {
                CaftSettings.Default.LaneSignal = false;
            }

            // Time Stamp (in seconds)
            CaftSettings.Default.TimeStamp = Convert.ToDecimal(txtSetting_TimeStamp.Text);

            // Total Grids
            CaftSettings.Default.GridRowCount = Convert.ToInt16(txtSetting_GridCounts.Text);

            // Road Width
            CaftSettings.Default.RoadWidth = Convert.ToDecimal(txtSetting_RoadWidth.Text);

            // Cell Size (in meter)
            CaftSettings.Default.CellSize_Height = Convert.ToDecimal(txtSetting_CellHeight.Text);
            CaftSettings.Default.CellSize_Width = Convert.ToDecimal(txtSetting_CellWidth.Text);
            CaftSettings.Default.CellSize_InPixels = Convert.ToDecimal(txtSetting_CellPixels.Text);

            // Vehicle Dimensions in meter
            // Two wheeler
            //CaftSettings.Default.TwoVhSize_Row = Convert.ToDecimal(txtSetting_2WheelLength.Text);
            //CaftSettings.Default.TwoVhSize_Column = Convert.ToDecimal(txtSetting_2WheelWidth.Text);
            //CaftSettings.Default.TwoVh_MaxAcceleration = Convert.ToDecimal(txtSetting_2WheelAcc.Text);

            CaftSettings.Default.TwoVhSize_Row = (int)Math.Ceiling(TwoWhHeight / Convert.ToDouble(txtSetting_CellHeight.Text));
            CaftSettings.Default.TwoVhSize_Column = (int)Math.Ceiling(TwoWhWidth / Convert.ToDouble(txtSetting_CellWidth.Text));
            CaftSettings.Default.TwoVh_MaxAcceleration = Math.Round((((Convert.ToDecimal(txtSetting_2WheelAcc.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.TwoVh_MaxAccelerationInKm = Convert.ToDecimal(txtSetting_2WheelAcc.Text);
            CaftSettings.Default.TwoVhAccParam = (int)Math.Round((((Convert.ToDecimal(txtSetting_2WheelAccParam.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.TwoVhAccParamInkm = Convert.ToDecimal(txtSetting_2WheelAccParam.Text);
            CaftSettings.Default.percentTwoVh = Convert.ToInt16(txtSetting_2WheelPercent.Text);
            CaftSettings.Default.TwoVhInclude = (bool)chk2Wheel.IsChecked;

            // Three wheeler
            //CaftSettings.Default.ThreeVhSize_Row = Convert.ToDecimal(txtSetting_3WheelLength.Text);
            //CaftSettings.Default.ThreeVhSize_Column = Convert.ToDecimal(txtSetting_3WheelWidth.Text);
            //CaftSettings.Default.ThreeVh_MaxAcceleration = Convert.ToDecimal(txtSetting_3WheelAcc.Text);

            CaftSettings.Default.ThreeVhSize_Row = (int)Math.Ceiling(ThreeWhHeight / Convert.ToDouble(txtSetting_CellHeight.Text));
            CaftSettings.Default.ThreeVhSize_Column = (int)Math.Ceiling(ThreeWhWidth / Convert.ToDouble(txtSetting_CellWidth.Text));
            CaftSettings.Default.ThreeVh_MaxAcceleration = Math.Round((((Convert.ToDecimal(txtSetting_3WheelAcc.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.ThreeVh_MaxAccelerationInKm = Convert.ToDecimal(txtSetting_3WheelAcc.Text);
            CaftSettings.Default.ThreeVhAccParam = (int)Math.Round((((Convert.ToDecimal(txtSetting_3WheelAccParam.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.ThreeVhAccParamInkm = Convert.ToDecimal(txtSetting_3WheelAccParam.Text);
            CaftSettings.Default.percentThreeVh = Convert.ToInt16(txtSetting_3WheelPercent.Text);
            CaftSettings.Default.ThreeVhInclude = (bool)chk3Wheel.IsChecked;

            // Four wheeler
            //CaftSettings.Default.FourVhSize_Row = Convert.ToDecimal(txtSetting_4WheelLength.Text);
            //CaftSettings.Default.FourVhSize_Column = Convert.ToDecimal(txtSetting_4WheelWidth.Text);
            //CaftSettings.Default.FourVh_MaxAcceleration = Convert.ToDecimal(txtSetting_4WheelAcc.Text);

            CaftSettings.Default.FourVhSize_Row = (int)Math.Ceiling(FourWhHeight / Convert.ToDouble(txtSetting_CellHeight.Text));
            CaftSettings.Default.FourVhSize_Column = (int)Math.Ceiling(FourWhWidth / Convert.ToDouble(txtSetting_CellWidth.Text));
            CaftSettings.Default.FourVh_MaxAcceleration = Math.Round((((Convert.ToDecimal(txtSetting_4WheelAcc.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.FourVh_MaxAccelerationInKm = Convert.ToDecimal(txtSetting_4WheelAcc.Text);
            CaftSettings.Default.FourVhAccParam = (int)Math.Round((((Convert.ToDecimal(txtSetting_4WheelAccParam.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.FourVhAccParamInkm = Convert.ToDecimal(txtSetting_4WheelAccParam.Text);
            CaftSettings.Default.percentFourVh = Convert.ToInt16(txtSetting_4WheelPercent.Text);
            CaftSettings.Default.FourVhInclude = (bool)chk4Wheel.IsChecked;

            // LCV1 wheeler
            //CaftSettings.Default.LCV1VhSize_Row = Convert.ToDecimal(txtSetting_LCV1WheelLength.Text);
            //CaftSettings.Default.LCV1VhSize_Column = Convert.ToDecimal(txtSetting_LCV1WheelWidth.Text);
            //CaftSettings.Default.LCV1Vh_MaxAcceleration = Convert.ToDecimal(txtSetting_LCV1WheelAcc.Text);

            CaftSettings.Default.LCV1VhSize_Row = (int)Math.Ceiling(LCV1Height / Convert.ToDouble(txtSetting_CellHeight.Text));
            CaftSettings.Default.LCV1VhSize_Column = (int)Math.Ceiling(LCV1Width / Convert.ToDouble(txtSetting_CellWidth.Text));
            CaftSettings.Default.LCV1Vh_MaxAcceleration = Math.Round((((Convert.ToDecimal(txtSetting_LCV1WheelAcc.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.LCV1Vh_MaxAccelerationInKm = Convert.ToDecimal(txtSetting_LCV1WheelAcc.Text);
            CaftSettings.Default.LCV1VhAccParam = (int)Math.Round((((Convert.ToDecimal(txtSetting_LCV1WheelAccParam.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.LCV1VhAccParamInkm = Convert.ToDecimal(txtSetting_LCV1WheelAccParam.Text);
            CaftSettings.Default.percentLCV1 = Convert.ToInt16(txtSetting_LCV1WheelPercent.Text);
            CaftSettings.Default.LCV1VhInclude = (bool)chkLCV1Wheel.IsChecked;

            // LCV2 wheeler
            //CaftSettings.Default.LCV2VhSize_Row = Convert.ToDecimal(txtSetting_LCV2WheelLength.Text);
            //CaftSettings.Default.LCV2VhSize_Column = Convert.ToDecimal(txtSetting_LCV2WheelWidth.Text);
            //CaftSettings.Default.LCV2Vh_MaxAcceleration = Convert.ToDecimal(txtSetting_LCV2WheelAcc.Text);

            CaftSettings.Default.LCV2VhSize_Row = (int)Math.Ceiling(LCV2Height / Convert.ToDouble(txtSetting_CellHeight.Text));
            CaftSettings.Default.LCV2VhSize_Column = (int)Math.Ceiling(LCV2Width / Convert.ToDouble(txtSetting_CellWidth.Text));
            CaftSettings.Default.LCV2Vh_MaxAcceleration = Math.Round((((Convert.ToDecimal(txtSetting_LCV2WheelAcc.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.LCV2Vh_MaxAccelerationInKm = Convert.ToDecimal(txtSetting_LCV2WheelAcc.Text);
            CaftSettings.Default.LCV2VhAccParam = (int)Math.Round((((Convert.ToDecimal(txtSetting_LCV2WheelAccParam.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.LCV2VhAccParamInkm = Convert.ToDecimal(txtSetting_LCV2WheelAccParam.Text);
            CaftSettings.Default.percentLCV2 = Convert.ToInt16(txtSetting_LCV2WheelPercent.Text);
            CaftSettings.Default.LCV2VhInclude = (bool)chkLCV2Wheel.IsChecked;

            // HCV1 wheeler
            //CaftSettings.Default.HCV1VhSize_Row = Convert.ToDecimal(txtSetting_HCV1WheelLength.Text);
            //CaftSettings.Default.HCV1VhSize_Column = Convert.ToDecimal(txtSetting_HCV1WheelWidth.Text);
            //CaftSettings.Default.HCV1Vh_MaxAcceleration = Convert.ToDecimal(txtSetting_HCV1WheelAcc.Text);

            CaftSettings.Default.HCV1VhSize_Row = (int)Math.Ceiling(HCV1Height / Convert.ToDouble(txtSetting_CellHeight.Text));
            CaftSettings.Default.HCV1VhSize_Column = (int)Math.Ceiling(HCV1Width / Convert.ToDouble(txtSetting_CellWidth.Text));
            CaftSettings.Default.HCV1Vh_MaxAcceleration = Math.Round((((Convert.ToDecimal(txtSetting_HCV1WheelAcc.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.HCV1Vh_MaxAccelerationInKm = Convert.ToDecimal(txtSetting_HCV1WheelAcc.Text);
            CaftSettings.Default.HCV1VhAccParam = (int)Math.Round((((Convert.ToDecimal(txtSetting_HCV1WheelAccParam.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.HCV1VhAccParamInkm = Convert.ToDecimal(txtSetting_HCV1WheelAccParam.Text);
            CaftSettings.Default.percentHCV1 = Convert.ToInt16(txtSetting_HCV1WheelPercent.Text);
            CaftSettings.Default.HCV1VhInclude = (bool)chkHCV1Wheel.IsChecked;

            // HCV2 wheeler
            //CaftSettings.Default.HCV2VhSize_Row = Convert.ToDecimal(txtSetting_HCV2WheelLength.Text);
            //CaftSettings.Default.HCV2VhSize_Column = Convert.ToDecimal(txtSetting_HCV2WheelWidth.Text);
            //CaftSettings.Default.HCV2Vh_MaxAcceleration = Convert.ToDecimal(txtSetting_HCV2WheelAcc.Text);

            CaftSettings.Default.HCV2VhSize_Row = (int)Math.Ceiling(HCV2Height / Convert.ToDouble(txtSetting_CellHeight.Text));
            CaftSettings.Default.HCV2VhSize_Column = (int)Math.Ceiling(HCV2Width / Convert.ToDouble(txtSetting_CellWidth.Text));
            CaftSettings.Default.HCV2Vh_MaxAcceleration = Math.Round((((Convert.ToDecimal(txtSetting_HCV2WheelAcc.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.HCV2Vh_MaxAccelerationInKm = Convert.ToDecimal(txtSetting_HCV2WheelAcc.Text);
            CaftSettings.Default.HCV2VhAccParam = (int)Math.Round((((Convert.ToDecimal(txtSetting_HCV2WheelAccParam.Text) * 1000) / 3600) / Convert.ToDecimal(txtSetting_CellHeight.Text)) / 1, 0);
            CaftSettings.Default.HCV2VhAccParamInkm = Convert.ToDecimal(txtSetting_HCV2WheelAccParam.Text);
            CaftSettings.Default.percentHCV2 = Convert.ToInt16(txtSetting_HCV2WheelPercent.Text);
            CaftSettings.Default.HCV2VhInclude = (bool)chkHCV2Wheel.IsChecked;

            // Acceleration
            CaftSettings.Default.Vh_OvertakingGap = Convert.ToDecimal(txtSetting_OvertakingGap.Text);
            CaftSettings.Default.Vh_AccelerationGap = Convert.ToDecimal(txtSetting_AccelerationGap.Text);

            // Headway
            CaftSettings.Default.headway = Convert.ToDecimal(txtSetting_Headway.Text);

            //BumpLine
            CaftSettings.Default.BumpLine = Convert.ToInt32(txtSetting_BumpLine.Text);
            CaftSettings.Default.bumpArea = Convert.ToInt32(txtSetting_BumpArea.Text);

            //DelayDistance
            CaftSettings.Default.DelayDistance = Convert.ToInt16(txtSetting_DelayDistance.Text);

            //Include Bump or not
            CaftSettings.Default.bumpInclude = (bool)chkbump.IsChecked;

            //or Include Signal or not
            CaftSettings.Default.signalInclude = (bool)chkSignal.IsChecked;
            CaftSettings.Default.GreenSignalTime = Convert.ToInt16(txtSetting_SGreenTime.Text);
            CaftSettings.Default.GreenSignalTimeLeft = Convert.ToInt16(txtSetting_SGreenTimeLeft.Text);
            CaftSettings.Default.GreenSignalTimeTop = Convert.ToInt16(txtSetting_SGreenTimeTop.Text);
            CaftSettings.Default.GreenSignalTimeRight = Convert.ToInt16(txtSetting_SGreenTimeRight.Text);
            //CaftSettings.Default.RedSignalTime = Convert.ToInt16(txtSetting_SRedTime.Text);
            CaftSettings.Default.AmberSignalTime = Convert.ToInt16(txtSetting_SAmberTime.Text);

            CaftSettings.Default.SignalFirstLegSelection = ((LegType)cmbFirstSignalLeg.SelectedItem).Id;

            //Percentage Distribution in Straight, Left and Right
            CaftSettings.Default.percentStraight = Convert.ToInt16(txtSetting_PercentStaright.Text);
            CaftSettings.Default.percentRight = Convert.ToInt16(txtSetting_PercentRight.Text);
            CaftSettings.Default.percentLeft = Convert.ToInt16(txtSetting_PercentLeft.Text);

            // Auto Signal
            CaftSettings.Default.AutoSignalInclude = (bool)chkAutoSignal.IsChecked;

            CaftSettings.Default.AutoGreenSignalTime_Bottom = Convert.ToInt16(txtSetting_AutoGreenTime_Bottom.Text);
            CaftSettings.Default.AutoGreenSignalTime_Left = Convert.ToInt16(txtSetting_AutoGreenTime_Left.Text);
            CaftSettings.Default.AutoGreenSignalTime_Top = Convert.ToInt16(txtSetting_AutoGreenTime_Top.Text);
            CaftSettings.Default.AutoGreenSignalTime_Right = Convert.ToInt16(txtSetting_AutoGreenTime_Right.Text);
            CaftSettings.Default.CrossRoadSignalAmberTime = Convert.ToInt16(txtSetting_CrossRoadSignalAmberTime.Text);
            CaftSettings.Default.IntersectionSpeedZone = Convert.ToInt16(txtSetting_IntersectionSpeedZone.Text);

            //CaftSettings.Default.AutoRedSignalTime = Convert.ToInt16(txtSetting_AutoRedTime.Text);
            //CaftSettings.Default.AmberSignalTime = Convert.ToInt16(txtSetting_AutoRedTime.Text);

            CaftSettings.Default.lefttraffic = (bool)chkLStraffic.IsChecked;
            CaftSettings.Default.righttraffic = (bool)chkRStraffic.IsChecked;
            CaftSettings.Default.toptraffic = (bool)chkTStraffic.IsChecked;
            CaftSettings.Default.bottomtraffic = (bool)chkBStraffic.IsChecked;

            // Vehicle Range Include
            CaftSettings.Default.vhRangeInclude = (bool)chkVhRange.IsChecked;
            CaftSettings.Default.VhRangeMin = Convert.ToInt16(txtSetting_vhRangeMin.Text);
            CaftSettings.Default.VhRangeMax = Convert.ToInt16(txtSetting_VhRangeMax.Text);
            CaftSettings.Default.VhRangeTime = txtSetting_VhRangeTime.Text;

            //Noise Probability Factor
            CaftSettings.Default.NoiseProbFactor = Convert.ToInt16(txtSetting_NoiseProbFactor.Text);


            // Saving the configurations
            CaftSettings.Default.Save();

            MessageBox.Show("Your Configurations has been saved.");

            this.Close();

            System.Windows.Application.Current.Shutdown();
            //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            //System.Windows.Application.Current.Run();
        }

        private void chkSignal_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)chkSignal.IsChecked)
            {
                lblFirstSignalLeg.Visibility = System.Windows.Visibility.Visible;
                cmbFirstSignalLeg.Visibility = System.Windows.Visibility.Visible;
                lblSetting_BumpLine.Visibility = System.Windows.Visibility.Visible;
                txtSetting_BumpLine.Visibility = System.Windows.Visibility.Visible;
                stckSignal1.Visibility = System.Windows.Visibility.Visible;
                stckSignal2.Visibility = System.Windows.Visibility.Visible;
                stckSignal3.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                lblFirstSignalLeg.Visibility = System.Windows.Visibility.Hidden;
                cmbFirstSignalLeg.Visibility = System.Windows.Visibility.Hidden;
                lblSetting_BumpLine.Visibility = System.Windows.Visibility.Hidden;
                txtSetting_BumpLine.Visibility = System.Windows.Visibility.Hidden;
                stckSignal1.Visibility = System.Windows.Visibility.Hidden;
                stckSignal2.Visibility = System.Windows.Visibility.Hidden;
                stckSignal3.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void chkAutoSignal_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)chkAutoSignal.IsChecked)
            {
                //stSAutoRed.Visibility = System.Windows.Visibility.Visible;
                stSAutoGreen.Visibility = System.Windows.Visibility.Visible;
                lblIntersectionSpeedZone1.Visibility = System.Windows.Visibility.Visible;
                lblIntersectionSpeedZone2.Visibility = System.Windows.Visibility.Visible;
                txtSetting_IntersectionSpeedZone.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                //stSAutoRed.Visibility = System.Windows.Visibility.Hidden;
                stSAutoGreen.Visibility = System.Windows.Visibility.Hidden;
                lblIntersectionSpeedZone1.Visibility = System.Windows.Visibility.Hidden;
                lblIntersectionSpeedZone2.Visibility = System.Windows.Visibility.Hidden;
                txtSetting_IntersectionSpeedZone.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void chkVhRange_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)chkVhRange.IsChecked)
            {
                txtSetting_vhRangeMin.Visibility = Visibility.Visible;
                txtSetting_VhRangeMax.Visibility = Visibility.Visible;
                txtSetting_VhRangeTime.Visibility = Visibility.Visible;
            }
            else
            {
                txtSetting_vhRangeMin.Visibility = Visibility.Collapsed;
                txtSetting_VhRangeMax.Visibility = Visibility.Collapsed;
                txtSetting_VhRangeTime.Visibility = Visibility.Collapsed;
            }
        }

    }
}
