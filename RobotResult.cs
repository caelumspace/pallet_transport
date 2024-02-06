using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svt_robotics
{
    public class RobotResult
    {
        
        public string robotId{ get; set; }
        public int batteryLevel{ get; set; }
        public double distanceToGoal{ get; set; }

        public RobotResult() { }

        public RobotResult(Robot r){
            this.robotId = r.robotId;
            this.batteryLevel = r.batteryLevel;
            this.distanceToGoal = r.distanceToGoal;
        }
    }
}