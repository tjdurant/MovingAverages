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

            var node = new Uri("http://DESKTOP-09F78PI:9200");
            var settings = new ConnectionSettings(node);
            var elasticClient = new ElasticClient(settings);

            //var glucoseResult = elasticClient.Search<Result>(s => s
            //    .Aggregations(a => a
            //        .Filter("my_filter_agg", f => f
            //            .Filter(fd => fd
            //                .Term("comp", "GLUCOSE")
            //                )
            //            .Aggregations(agg => agg
            //                .Average("my_avg_agg", avg => avg
            //                    .Field(p => p.NumericValue)
            //                    )
            //                )
            //            )
            //        )
            //    );

            var response = elasticClient.Search<Result>(s => s
                .From(0)
                .Size(10)
                .Query(q =>
                        q.Term(t => t.Component, "GLUCOSE")
                        || q.Match(mq => mq.Field(f => f.NumericValue))
                    )
                );
            //var avgAgg = filterAgg.Average("my_avg_agg");

            //var filterAgg = result.Aggs.Filter("my_filter_agg");


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

            //var componentTypes = result.Aggs.Terms("firstLevel");


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
