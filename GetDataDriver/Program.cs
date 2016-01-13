using CsvHelper;
using Elasticsearch.Net.Connection;
using Nest;
using Storage.Documents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            var node = new Uri("http://localhost:9200");
            var index = "moving_averages";
            var settings = new ConnectionSettings(node, index);
            var elasticClient = new ElasticClient(settings);
 
            var aggPath = @"C:\Users\thoma\Documents\00GitHub\MovingAverages\JsonQueries\glucoseMovingAverage.txt";

            System.IO.StreamReader myFile = new System.IO.StreamReader(aggPath);
            string myString = myFile.ReadToEnd();


            var component = "glucose";
            var greaterThan = "65";
            var lessThan = "105";
            var timeInterval = "day";
            var windowFrame = "50";

            string aggString = File.ReadAllText(aggPath);
            aggString = aggString   .Replace("component", component)
                                    .Replace("greaterThan", greaterThan)
                                    .Replace("lessThan", lessThan)
                                    .Replace("timeInterval", timeInterval)
                                    .Replace("windowFrame", windowFrame);

            var result = elasticClient.Search<Result>(s => s
                .Size(1000)
                .QueryRaw(aggString)
                .Scroll("1m")
                );

            var agBucket = (Bucket)result.Aggregations["my_date_histo"];

            var movingAverageList = new List<KeyValuePair<DateTime, double?>>();
            foreach (HistogramItem item in agBucket.Items)
            {
                var mov_avg = (ValueMetric)item.Aggregations["glucose_avg"];
                var date = item.Date;
                    
                var avg_value = mov_avg.Value;

                movingAverageList.Add(new KeyValuePair<DateTime, double?>(date, avg_value));
                    
            }

            foreach (var element in movingAverageList)
            {
                Console.WriteLine(element);
            }
        }
    }
}
