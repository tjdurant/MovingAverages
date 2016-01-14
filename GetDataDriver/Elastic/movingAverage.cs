using Nest;
using Storage.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataDriver.Elastic
{
    public class MovingAverage
    {
        // custom data object: public string component list of moving averag
        public List<KeyValuePair<DateTime, double?>> MovingAverageFunction(string aggPath, string component, 
            string greaterThan, string lessThan, string timeInterval, string windowFrame)
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

            var result = elasticClient.Search<Result>(s => s
                .QueryRaw(aggString)
                );

            var agBucket = (Bucket)result.Aggregations["my_date_histo"];

            var movingAverageList = new List<KeyValuePair<DateTime, double?>>();
            foreach (HistogramItem item in agBucket.Items)
            {
                var mov_avg = (ValueMetric)item.Aggregations["agg_avg"];
                var date = item.Date;

                var avg_value = mov_avg.Value;

                movingAverageList.Add(new KeyValuePair<DateTime, double?>(date, avg_value));

            }
            return movingAverageList;
        }
    }
}
