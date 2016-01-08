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
            var settings = new ConnectionSettings(node);
            var elasticClient = new ElasticClient(settings);

            new PutMappingRequest<Result>
            {
                Properties = new Properties<Result>
                {
                    { p=>p.EncounterId, new StringProperty { Name = "eid" } },
                    { p=>p.PatientId, new StringProperty { Name = "pid" } },
                    { p=>p.Age, new StringProperty { Name = "age" } },
                    { p=>p.Sex, new StringProperty { Name = "sex" } },
                    { p=>p.OrderId, new StringProperty { Name = "oid" } },
                    { p=>p.ProcedureCode, new StringProperty { Name = "pr_code",Index = FieldIndexOption.NotAnalyzed } },
                    { p=>p.ProcedureName, new StringProperty { Name = "pr_name",Index = FieldIndexOption.NotAnalyzed } },
                    { p=>p.OrderTimestamp, new StringProperty { Name = "o_ts" } },
                    { p=>p.Component, new StringProperty { Name = "comp", Index = FieldIndexOption.NotAnalyzed } },
                    { p=>p.StringValue, new StringProperty { Name = "s_val" } },
                    { p=>p.NumericValue, new StringProperty { Name = "d_val" } },
                    { p=>p.Campus, new StringProperty { Name = "camp" } }
                }
            };

            var result = elasticClient.Search<Result>(s => s
                .From(0)
                .Size(10)
                // Query here
            );

            var glucoseQuery = elasticClient.Search<Result>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Term(p => p.Component, "GLUCOSE")
                )
            );



        }
    }
}
