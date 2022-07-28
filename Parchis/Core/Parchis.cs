using Parchis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parchis.Core
{
    public class Parchis
    {
        public List<CellModel> Board { get; set; }
        public List<CellModel> FinishLaneRed { get; set; }
        public List<CellModel> FinishLaneBlue { get; set; }
        public List<CellModel> FinishLaneYellow { get; set; }
        public List<CellModel> FinishLaneGreen { get; set; }

        public void PlaceChipInPosition(ChipModel chip, ChipPositionModel position)
        {

        }

        public void RemoveChipFromPosition(ChipModel chip, ChipPositionModel position)
        {

        }
    }
}
