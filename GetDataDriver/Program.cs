using Nest;
using Storage.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:9200");
            var index = "moving_averages";
            var elasticClient = new ElasticClient(new ConnectionSettings(uri, index));


            // build queries
            QueryContainer sodiumQuery = new TermQuery
            {
                Field = "comp",
                Value = "SODIUM"
            };

            QueryContainer resultQuery = new TermQuery
            {
                Field = "d_val"
            };

            var searchRequest = new SearchRequest
            {
                From = 0,
                Size = 10,
                Query = sodiumQuery && resultQuery
            };

            var searchResults = elasticClient.Search<Result>(searchRequest);

            //aggregate queries 
            //Query = (query1 && !query2) || query3

        }
    }
}
