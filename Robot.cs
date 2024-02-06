using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svt_robotics
{
    public class Robot
    {
        public string robotId{ get; set; }
        public int batteryLevel{ get; set; }
        public int x{ get; set; }
        public int y{ get; set; }
        public double distanceToGoal{ get; set; }

        public Point ToPoint()
        {
            return new Point(x, y);
        }


        public RobotResult ToRes()
        {
            return new RobotResult(this);
        }
    }
}