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
            var node = new Uri("http://DESKTOP-09F78PI:9200");
            var index = "moving_averages";
            var settings = new ConnectionSettings(node, index);
            var elasticClient = new ElasticClient(settings);

            var queryPath = @"C:\Users\thoma\Documents\00GitHub\MovingAverages\JsonQueries\glucoseAggregationAverage.txt";


            System.IO.StreamReader myFile = new System.IO.StreamReader(queryPath);
            string myString = myFile.ReadToEnd();



            var result = elasticClient.Search<Result>(s => s
                .From(0)
                .QueryRaw(myString)
                );

            var myAgg = result.Aggs.Average("glucose");
            Console.WriteLine(myAgg);
            myFile.Close();
        }
    }
}
