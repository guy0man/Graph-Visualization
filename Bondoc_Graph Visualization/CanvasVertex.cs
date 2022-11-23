using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bondoc_Graph_Visualization
{
    public class CanvasVertex
    {
        public char Letter { get; set; }
        public double positionX { get; set; }
        public double positionY { get; set; }
        public CanvasVertex (char letter, double x, double y)
        {
            Letter = letter;
            positionX = x;
            positionY = y; 
        }
    }
}
