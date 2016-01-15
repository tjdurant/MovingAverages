using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace highChartsTesting.Models
{
    public class MovingAverageModels
    {
        public string Component { get; set; }
        public string TimeResolution { get; set; }
        public List<MovingAverageVals> AvgVals { get; set; }
    }

    public class MovingAverageVals
    {
        public DateTime Date { get; set; }
        public double? Value { get; set; }
    }
    
}