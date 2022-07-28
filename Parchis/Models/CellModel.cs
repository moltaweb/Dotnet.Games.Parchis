using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parchis.Models
{
    public class CellModel
    {
        public int Position { get; set; }
        public List<ChipModel> Chips { get; set; }
    }
}
