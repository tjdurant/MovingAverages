using highChartsTesting.Models;
using Nest;
using Storage.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace highChartsTesting.Elastic
{
    public class MovingAverage
    {
        // custom data object: public string component list of moving averag


        public List<MovingAverageModels> MovingAverageFunction(string aggPath, string component, 
                                                                string greaterThan, string lessThan, 
                                                                string timeInterval, string windowFrame)
        {

            var node = new Uri("http://localhost.:9200");
            var index = "moving_averages";
            var settings = new ConnectionSettings(node, index);
            var elasticClient = new ElasticClient(settings);

            string aggString = File.ReadAllText(aggPath);
            aggString = aggString   .Replace("***component", component)
                                    .Replace("***greaterThan", greaterThan)
                                    .Replace("***lessThan", lessThan)
                                    .Replace("***timeInterval", timeInterval)
                                    .Replace("***windowFrame", windowFrame);

            var searchResult = elasticClient.Search<Result>(s => s
                .QueryRaw(aggString)
                );

            var agBucket = (Bucket)searchResult.Aggregations["my_date_histo"];

            var ma = new List<MovingAverageModels>();


            foreach (HistogramItem item in agBucket.Items)
            {
                var mov_avg = (ValueMetric)item.Aggregations["agg_avg"];

                // convert date_histogram to dateTime object
                var date = item.Date;

                // convert valueMetric to value
                var avg_value = mov_avg.Value;

                var avgResult = new MovingAverageVals
                {
                    Value = avg_value,
                    Date = date
                };

                var cur = new MovingAverageModels
                {
                    Component = component,
                    TimeResolution = timeInterval,
                    AvgVals = avgResult
                };

                ma.Add(cur);
            }
            return ma;
        }
    }
}
