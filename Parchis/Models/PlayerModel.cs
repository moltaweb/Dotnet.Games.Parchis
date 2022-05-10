using Parchis.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Parchis.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public bool isHuman { get; set; }
        public SolidColorBrush Color { get; set; }

        public List<ChipModel> Chips { get; set; }

        public PlayerModel(string Name, SolidColorBrush Color, bool isHuman = false)
        {
            this.Name = Name;
            this.Color = Color;            
            this.isHuman = isHuman;

            CreateChips();
        }

        private void CreateChips()
        {            
            Chips = new List<ChipModel>();
            
            for(int i = 0; i < 4; i++)
            {
                Chips.Add(new ChipModel(Color, isHuman));

            }
            
        }
    }
}
