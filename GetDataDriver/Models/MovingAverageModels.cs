using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// not sure how to fix this namespace without breaking the MVC app
// I cut and pasted the model from the highChartsTesting project
namespace highChartsTesting.Models
{
    public class MovingAverageModels
    {
        public string Component { get; set; }
        public string TimeResolution { get; set; }
        public MovingAverageVals AvgVals { get; set; }
    }

    public class MovingAverageVals
    {
        public DateTime Date { get; set; }
        public double? Value { get; set; }
    }
    
}