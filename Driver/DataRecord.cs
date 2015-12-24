using CsvHelper.Configuration;
using Storage.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    // Data class
    public class DataRecord
    {
        
        public string EncounterId { get; set; }
        public string PatientId { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string OrderId { get; set; }
        public string ProcedureCode { get; set; }
        public string ProcedureName { get; set; }
        public string OrderTimestamp { get; set; }
        public string Component { get; set; }
        public string StringValue { get; set; }
        public double NumericValue { get; set; }
        public string Campus { get; set; }

    }

    // Data map
    public sealed class MyClassMap : CsvClassMap<Result>
    {
        //PAT_ENC_CSN_ID|PAT_ID|AGE|SEX|ORDER_NUMBER|PROC_CODE|PROC_NAME|ORDER_DATE|COMPONENT|RESULT|CAMPUS
        public MyClassMap()
        {
            Map(m => m.EncounterId).Name("PAT_ENC_CSN_ID");
            Map(m => m.PatientId).Name("PAT_ID");
            Map(m => m.Age).Name("AGE");
            Map(m => m.Sex).Name("SEX");
            Map(m => m.OrderId).Name("ORDER_NUMBER");
            Map(m => m.ProcedureCode).Name("PROC_CODE");
            Map(m => m.ProcedureName).Name("PROC_NAME");
            Map(m => m.OrderTimestamp).Name("ORDER_DATE");
            Map(m => m.Timestamp).Name("ORDER_DATE");
            Map(m => m.Component).Name("COMPONENT");
            Map(m => m.StringValue).Name("RESULT");
            Map(m => m.NumericValue).Name("RESULT");
            Map(m => m.Campus).Name("CAMPUS");
        }

    }
}
