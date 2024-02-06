using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svt_robotics
{
      public sealed class Point
    {
        public readonly double X;
        public readonly double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}