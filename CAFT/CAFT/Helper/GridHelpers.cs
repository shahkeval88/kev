using CAFT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CAFT.Helper
{
    public class GridHelpers
    {
        #region RowCount Property

        /// <summary>
        /// Adds the specified number of Rows to RowDefinitions. 
        /// Default Height is Auto
        /// </summary>
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.RegisterAttached(
                "RowCount", typeof(int), typeof(GridHelpers),
                new PropertyMetadata(-1, RowCountChanged));

        // Get
        public static int GetRowCount(DependencyObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }

        // Set
        public static void SetRowCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
        }

        // Change Event - Adds the Rows
        public static void RowCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.RowDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
            {
                grid.RowDefinitions.Add(
                    new RowDefinition()
                    {
                        Height = new GridLength(10.0) // GridLength.Auto 
                    });
            }
            SetStarRows(grid);
        }

        #endregion

        #region ColumnCount Property

        /// <summary>
        /// Adds the specified number of Columns to ColumnDefinitions. 
        /// Default Width is Auto
        /// </summary>
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.RegisterAttached(
                "ColumnCount", typeof(int), typeof(GridHelpers),
                new PropertyMetadata(-1, ColumnCountChanged));

        // Get
        public static int GetColumnCount(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnCountProperty);
        }

        // Set
        public static void SetColumnCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnCountProperty, value);
        }

        // Change Event - Add the Columns
        public static void ColumnCountChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || (int)e.NewValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < (int)e.NewValue; i++)
            {
                grid.ColumnDefinitions.Add(
                    new ColumnDefinition()
                    {
                        Width = new GridLength(10.0)
                    });
            }

            Style style = new Style();
            style.Setters.Add(new Setter(Control.BorderBrushProperty, new SolidColorBrush(Colors.Blue)));
            style.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0.3)));

            int totalRows = grid.RowDefinitions.Count;
            int totalColumns = grid.ColumnDefinitions.Count;

            for (int i = 0; i < totalRows; i++)
            {
                for (int j = 0; j < totalColumns; j++)
                {
                    Border bd = new Border();
                    bd.Style = style;

                    bd.SetValue(Grid.RowProperty, i);
                    bd.SetValue(Grid.ColumnProperty, j);
                    grid.Children.Add(bd);
        
                }
            }

            
            SetStarColumns(grid);
        }

        #endregion

        #region StarRows Property

        /// <summary>
        /// Makes the specified Row's Height equal to Star. 
        /// Can set on multiple Rows
        /// </summary>
        public static readonly DependencyProperty StarRowsProperty =
            DependencyProperty.RegisterAttached(
                "StarRows", typeof(string), typeof(GridHelpers),
                new PropertyMetadata(string.Empty, StarRowsChanged));

        // Get
        public static string GetStarRows(DependencyObject obj)
        {
            return (string)obj.GetValue(StarRowsProperty);
        }

        // Set
        public static void SetStarRows(DependencyObject obj, string value)
        {
            obj.SetValue(StarRowsProperty, value);
        }

        // Change Event - Makes specified Row's Height equal to Star
        public static void StarRowsChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
                return;

            SetStarRows((Grid)obj);
        }

        #endregion

        #region StarColumns Property

        /// <summary>
        /// Makes the specified Column's Width equal to Star. 
        /// Can set on multiple Columns
        /// </summary>
        public static readonly DependencyProperty StarColumnsProperty =
            DependencyProperty.RegisterAttached(
                "StarColumns", typeof(string), typeof(GridHelpers),
                new PropertyMetadata(string.Empty, StarColumnsChanged));

        // Get
        public static string GetStarColumns(DependencyObject obj)
        {
            return (string)obj.GetValue(StarColumnsProperty);
        }

        // Set
        public static void SetStarColumns(DependencyObject obj, string value)
        {
            obj.SetValue(StarColumnsProperty, value);
        }

        // Change Event - Makes specified Column's Width equal to Star
        public static void StarColumnsChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
                return;

            SetStarColumns((Grid)obj);
        }

        #endregion

        private static void SetStarColumns(Grid grid)
        {
            string[] starColumns =
                GetStarColumns(grid).Split(',');

            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                if (starColumns.Contains(i.ToString()))
                    grid.ColumnDefinitions[i].Width =
                        new GridLength(1, GridUnitType.Star);
            }
        }

        private static void SetStarRows(Grid grid)
        {
            string[] starRows =
                GetStarRows(grid).Split(',');

            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                if (starRows.Contains(i.ToString()))
                    grid.RowDefinitions[i].Height =
                        new GridLength(1, GridUnitType.Star);
            }
        }


        #region GridHighlighter Property

        /// <summary>
        /// Adds the specified number of Rows to RowDefinitions. 
        /// Default Height is Auto
        /// </summary>
        public static readonly DependencyProperty GridHighLighterProperty =
            DependencyProperty.RegisterAttached(
                "GridHighLighter", typeof(ObservableCollection<CellHighLighterType>), typeof(GridHelpers),
                new PropertyMetadata(new ObservableCollection<CellHighLighterType>(), GridHighLighterChanged));

        // Get
        public static ObservableCollection<CellHighLighterType> GetGridHighLighter(DependencyObject obj)
        {
            return (ObservableCollection<CellHighLighterType>)obj.GetValue(GridHighLighterProperty);
        }

        // Set
        public static void SetGridHighLighter(DependencyObject obj, ObservableCollection<CellHighLighterType> value)
        {
            obj.SetValue(GridHighLighterProperty, value);
        }

        // Change Event - Adds the Rows
        public static void GridHighLighterChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is Grid) || e.NewValue == null)
                return;

            Grid grid = (Grid)obj;
            ObservableCollection<CellHighLighterType> cellhighlighter = (ObservableCollection<CellHighLighterType>)e.NewValue;

            if(grid != null && cellhighlighter != null && cellhighlighter.Count > 0)
            {
                foreach (var cell in cellhighlighter)
                {
                    var border = grid.Children.OfType<Border>().Where(p => Grid.GetRow(p) == cell.RowNumber && Grid.GetColumn(p) == cell.ColumnNumber).FirstOrDefault();
                    Rectangle r = new Rectangle();
                    r.Height = 10.0;
                    r.Width = 10.0;
                    switch (cell.ColorCode)
                    {
                        case 1:
                            r.Fill = new SolidColorBrush(Colors.Red);
                            border.Child = r;
                            break;
                        default:
                            break;
                    }

                }
            }
            
            
        }

        #endregion


    }
}
