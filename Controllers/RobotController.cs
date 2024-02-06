using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace svt_robotics.Controllers
{






    [ApiController]
    [Route("[controller]")]
    public class RobotController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<RobotController> _logger;

        public RobotController(ILogger<RobotController> logger)
        {
            _logger = logger;
        }


        private static double Pow2(double x)
        {
            return x * x;
        }

        private static double Distance2(Point p1, Point p2)
        {
            return Math.Sqrt(Pow2(p2.X - p1.X) + Pow2(p2.Y - p1.Y));
        }

        [Route("~/api/GetClosest/{loadId}/{x}/{y}")]
        [HttpPost]
        public RobotResult GetClosest(int loadId, int x, int y)
        {
            var res = GetClosestAsync(loadId,  x,  y);
            return res.Result;
        }

        [Route("~/api/GetClosestAsync/{loadId}/{x}/{y}")]
        [HttpPost]
        public async Task<RobotResult> GetClosestAsync(int loadId, int x, int y)
        {
            Robot ret = new Robot();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://60c8ed887dafc90017ffbd56.mockapi.io/robots"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var coll = JsonConvert.DeserializeObject<ICollection<Robot>>(apiResponse);
                        if (coll != null)
                        {

                            Robot nearestRobot = null;
                            Point toPoint = new Point(x, y);
                            double minDist2 = double.MaxValue;
                            List<Robot> robotsInRange = new List<Robot>();
                            foreach (Robot r in coll)
                            {
                                double dist2 = Distance2(r.ToPoint(), toPoint);
                                r.distanceToGoal = dist2;
                                if (dist2 < 10.0)
                                {
                                    robotsInRange.Add(r);
                                }

                                if (dist2 < minDist2)
                                {
                                    minDist2 = dist2;
                                    nearestRobot = r;
                                    ret = nearestRobot;
                                }
                            }

                            if (robotsInRange.Count > 1)
                            {
                                Robot robotMostFuel = null;
                                double mostFuel = double.MinValue;
                                foreach (Robot r in robotsInRange)
                                {
                                    if (r.batteryLevel > mostFuel)
                                    {
                                        mostFuel = r.batteryLevel;
                                        robotMostFuel = r;
                                        ret = robotMostFuel;
                                    }
                                }
                            }

                        }

                    }
                    else
                        Console.WriteLine(response.StatusCode);
                }
            }
            
            return ret.ToRes();
        }



    }
}
