using System.Windows.Media;

namespace Parchis.Models
{
    public interface IChipModel
    {
        Brush Color { get; set; }
        int Position { get; set; }
    }
}