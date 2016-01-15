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
        // custom data object: 
        public List<MovingAverageModels> MovingAverageFunction(string aggPath, string component, 
                                                                string greaterThan, string lessThan, 
                                                                string timeInterval, string windowFrame)
        {
            // instantiate connection objects
            var node = new Uri("http://localhost.:9200");
            var index = "moving_averages";
            var settings = new ConnectionSettings(node, index);
            var elasticClient = new ElasticClient(settings);

            // instatiate data objects
            var ma = new List<MovingAverageModels>();
            var avgList = new List<MovingAverageVals>();

            // read .txt file into string
            string aggString = File.ReadAllText(aggPath);

            // replace parameters in .txt file with arguments 
            aggString = aggString   .Replace("***component", component)
                                    .Replace("***greaterThan", greaterThan)
                                    .Replace("***lessThan", lessThan)
                                    .Replace("***timeInterval", timeInterval)
                                    .Replace("***windowFrame", windowFrame);

            // run elastic search with raw JSON query string
            var searchResult = elasticClient.Search<Result>(s => s
                .QueryRaw(aggString)
                );

            // instantiate bucket of aggregations based on timeInterval 
            var agBucket = (Bucket)searchResult.Aggregations["my_date_histo"];

            // iterate through each date-item an aggregation was performed on
            foreach (HistogramItem item in agBucket.Items)
            {
                // access the valueMetric for each nested aggregation 
                var mov_avg = (ValueMetric)item.Aggregations["agg_avg"];

                // convert date_histogram to dateTime object
                var date = item.Date;

                // convert valueMetric to value
                var avg_value = mov_avg.Value;

                // pass aggregation data into model 
                var avgResult = new MovingAverageVals
                {
                    Value = avg_value,
                    Date = date
                };

                avgList.Add(avgResult);
            }

            // pass component, timeInterval and avgList into model
            var cur = new MovingAverageModels
            {
                Component = component,
                TimeResolution = timeInterval,
                AvgVals = avgList
            };

            ma.Add(cur);
            return ma;
        }
    }
}
