using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Parchis.Models
{
    public class ChipPlayerModel : Button, IChipModel
    {
        public Brush Color { get; set; }

        public int Position { get; set; }

        public ChipPlayerModel(Brush Color)
        {
            this.Color = Color;

            // determine starter position
            Random random = new Random();
            this.Position = random.Next(0, 68);
        }
    }
}
