using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parchis.Models
{
    public class ChipPositionModel
    {
        public int CellNumber { get; set; }        
        public AreaType Area { get; set; }

        public ChipPositionModel(AreaType area, int cellNumber)
        {
            CellNumber = cellNumber;
            Area = area;
        }

        public enum AreaType
        {
            Home,
            Board,
            FinishLane,
            GameOver
        }
    }
}
