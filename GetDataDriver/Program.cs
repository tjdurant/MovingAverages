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
            var node = new Uri("http://localhost:9200");
            var index = "moving_averages";
            var settings = new ConnectionSettings(node, index);
            var elasticClient = new ElasticClient(settings);


            // build queries
            var sodiumFilter = elasticClient.Search<Result>(s => s
                .From(0)
                .Size(50)
                .Query(q =>
                    (q.Match(m => m.OnField(o => o.Component).Query("GLUCOSE"))) 
                )
            );

            var response = elasticClient.Search<Result>(s => s
                .Size(0)
                .Aggregations(a => a
                    .Terms("comp", t => t 
                        .Field(o => o.NumericValue)
                    )
                )
            );

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
