using Elasticsearch.Net.Connection;
using Nest;
using Storage.Documents;
using System;
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
            var filterPath = @"C:\Users\thoma\Documents\00GitHub\MovingAverages\JsonQueries\glucoseRange.txt";

            System.IO.StreamReader myFile = new System.IO.StreamReader(aggPath);
            string myString = myFile.ReadToEnd();

            System.IO.StreamReader filterFile = new System.IO.StreamReader(filterPath);
            string filterString = filterFile.ReadToEnd();

            var result = elasticClient.Search<Result>(s => s
                .From(0)
                .QueryRaw(myString)
                );

            var glucoseFilter = elasticClient.Search<Result>(s => s
                .From(0)
                .Size(20000)
                .QueryRaw(filterString)
                .Sort(o => o.OnField(p => p.Timestamp).Ascending())
                );

             
            var totalDocumentCount = glucoseFilter;
            foreach (var hit in glucoseFilter.Hits)
            {
                //2015-10-02T08:58:00
                Console.WriteLine(hit.Source.Timestamp);
                DateTime date = hit.Source.Timestamp;
            }

            var glucoseAvg = result.Aggs.Average("glucose").Value;

            

            //for (var i = totalDocumentCount; i – windowSize > 0 && averages.Count() <= 25; i = i – step)
            //{

            //    var endIdx = i;

            //    var startIdx = i – windowSize;

            //    // can include the {} part in your text file, 
            //    // then read it into a variable with the string replacement operator. It should then replace
            //    // the {variableName} piece with what’s defined above

            //    var query = $”some json here where we order by descending result_timestamp and take documents { startIdx}
            //    through { endIdx}”;

            //    var avg = query.Average(r => r.Value);

            //    averages.Add(avg);

            //}

            myFile.Close();
        }
    }
}
