using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriCare.Utilities
{
    public class DrawPoint
    {

        public float X { get; set; }
        public float Y { get; set; }


        public DrawPoint() { }
        public DrawPoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
