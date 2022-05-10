using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Parchis.Models
{
    public class ChipModel
    {
        public SolidColorBrush Color { get; set; }
        public ChipPositionModel OldPosition { get; set; }
        public ChipPositionModel NewPosition { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }

        public bool IsHumanChip { get; set; }
        //public bool CanMove { get; set; }

        public ChipModel(SolidColorBrush Color, bool isHumanChip)
        {
            this.Color = Color;
            this.IsHumanChip = isHumanChip;
            this.OldPosition = null;           
            //this.CanMove = false;
            
            if (Color == Brushes.Red)
            {
                this.StartPosition = 38;
            }
            else if (Color == Brushes.Green)
            {
                this.StartPosition = 55;
            }
            else if (Color == Brushes.Blue)
            {
                this.StartPosition = 21;
            }
            else if (Color == Brushes.Yellow)
            {
                this.StartPosition = 4;
            }

            this.EndPosition = this.StartPosition - 5;

            this.NewPosition = new ChipPositionModel(ChipPositionModel.AreaType.Home, this.StartPosition);            

            // determine random starter position
            //Random random = new Random();
            //this.Position = random.Next(0, 68);
        }
        
    }
}
