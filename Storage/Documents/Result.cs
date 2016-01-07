using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Documents
{

    // Elastic put-mapping; The put mapping API allows to register specific mapping 
    // definition for a specific type.

    //ElasticType(Name = "moving_averages" )]
    public class Result
    {
        /*
        
        Lucene; Analyze (chunking) then index (searching)
                
        */

        //PAT_ENC_CSN_ID|PAT_ID|AGE|SEX|ORDER_NUMBER|PROC_CODE|PROC_NAME|ORDER_DATE|COMPONENT|RESULT|CAMPUS
        // attribute key for value; keep short
        
        public string Id { get; set; }

        //[ElasticProperty(Name ="eid")]
        public string EncounterId { get; set; }

        //[ElasticProperty(Name = "pid")]
        public string PatientId { get; set; }
        //[ElasticProperty(Name = "age")]
        public string Age { get; set; }
        //[ElasticProperty(Name = "sex")]
        public string Sex { get; set; }
        //[ElasticProperty(Name = "oid")]
        public string OrderId { get; set; }

        // doesn't run through the analyzer 
        //[ElasticProperty(Name = "pr_code", Index = FieldIndexOption.NotAnalyzed)]
        public string ProcedureCode { get; set; }

        //[ElasticProperty(Name = "pr_name", Index = FieldIndexOption.NotAnalyzed)]
        public string ProcedureName { get; set; }

        //[ElasticProperty(Name = "o_ts")]
        public string OrderTimestamp { get; set; }
        
        //[ElasticProperty(Name = "@timestamp", Type = FieldType.Date, DateFormat = "yyyy-MM-dd'T'HH:mm:ss", Store = true)]
        public DateTime Timestamp { get; set; }

        //[ElasticProperty(Name = "comp", Index = FieldIndexOption.NotAnalyzed)]
        public string Component { get; set; }

        
        //[ElasticProperty(Name = "s_val")]
        public string StringValue { get; set; }

        //[ElasticProperty(Name = "d_val", NumericType = NumberType.Double)]
        public double NumericValue { get; set; }

        //[ElasticProperty(Name = "camp")]
        public string Campus { get; set; }

    }
}
