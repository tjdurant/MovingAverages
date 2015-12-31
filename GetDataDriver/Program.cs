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
            var node = new Uri("http://127.0.0.1:9200");
            var index = "moving_averages";
            var settings = new ConnectionSettings(node, index);
            var elasticClient = new ElasticClient(settings);


            // build queries
            var glucoseFilter = elasticClient.Search<Result>(s => s
                .From(0)
                .Size(50)
                .Query(q =>
                    (q.Match(m => m.OnField(o => o.Component).Query("GLUCOSE"))) 
                )
            );

            // s = search, a = aggregation, st

            var result = elasticClient.Search<Result>(s => s
                .Size(10)
                .Query(q => q
                    .Filtered(f => f
                        .Query(ff => ff
                            .Term(o => o.Component, "GLUCOSE"))
                    .Filter(agg => agg
                        .Range(r => r
                            .GreaterOrEquals(70)
                            .LowerOrEquals(100)))))
                .Aggregations(agg => agg
                    .Average("glucose_avg", avg => avg
                        .Field(p => p.NumericValue)))
                );


            var filter = Filter<Result>.Term("comp", "GLUCOSE");

            var glucoseQuery = elasticClient.Search<Result>(s => s
                .Query(q => q
                    .Filtered(fd => fd
                        .Filter(f => f
                            .Term("comp", "GLUCOSE")
                        )
                    )
                )
            );

            var glucoseResult = elasticClient.Search<Result>(s => s
                .Aggregations(a => a
                    .Filter("my_filter_agg", f => f
                        .Filter(fd => fd
                            .Term("comp", "GLUCOSE")
                            )
                        .Aggregations(agg => agg
                            .Average("my_avg_agg", avg => avg
                                .Field(p => p.NumericValue)
                                )
                            )
                        )
                    )
                );

            var filterAgg = result.Aggs.Filter("my_filter_agg");
            var avgAgg = filterAgg.Average("my_avg_agg");


            var glucoseAverage = elasticClient.Search<Result>(s => s
                 .Aggregations(fstAgg => fstAgg
                     .Terms("firstLevel", f => f
                         .Field(z => z.Component)
                             .Aggregations(sums => sums
                                 .Average("average", son => son
                                 .Field(f4 => f4.NumericValue)
                             )
                         )
                     )
                 )
             );

            var componentTypes = result.Aggs.Terms("firstLevel");
            foreach (var c in componentTypes.Items)
            {
                var compAvg = (decimal)c.Average("average").Value;
            }

            //var glucoseAverage = elasticClient.Search<Result>(s => s
            //    .Aggregations(a => a


            //    .Terms("level_1", t => t.Field("firstName")),
            //            .Aggregations
            //                (b =>
            //                    b.Terms("level_2", t => t.Field("name")),
            //                )
            //        )


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
