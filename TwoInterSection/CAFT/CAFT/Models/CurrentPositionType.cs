using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAFT.Models
{
    public class CurrentPositionType
    {
        public CurrentPositionType()
        {
 
        }

        public CurrentPositionType(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }


        private int _Column;

        public int Column
        {
            get { return _Column; }
            set { _Column = value; }
        }


        private int _Row;

        public int Row
        {
            get { return _Row; }
            set 
            { 
                _Row = value; 
                
            }
        }


        private int _PreviousRow;

        public int PreviousRow
        {
            get { return _PreviousRow; }
            set { _PreviousRow = value; }
        }
        
        
    }
}
