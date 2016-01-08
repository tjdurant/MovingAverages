using CsvHelper;
using CsvHelper.Configuration;
using Nest;
using Storage.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            // complex bulk call
            var descriptor = new BulkDescriptor();
            var uri = new Uri("http://localhost:9200");
            var index = "moving_averages";
            var elastic = new ElasticClient(new ConnectionSettings(uri, index));

            // create index; index doesn't exist 
            elastic.CreateIndex(ci => ci.Index("moving_averages").AddMapping<Result>(m => m.MapFromAttributes()));            

            // index does exist; apply index for inserts; builds as per document(model)
            var response = elastic.Map<Result>(m => m.MapFromAttributes().Type<Result>().Indices("moving_averages"));

            // csv helper; nuget
            var file = File.OpenText(@"C:\Users\thoma\Documents\00GitHub\00_LOCAL_ONLY\deid-labs-dt\deid-labs-dt.csv");

            var csvReader = new CsvReader(file);

            csvReader.Configuration.Delimiter = "|";

            // Default value
            csvReader.Configuration.HasHeaderRecord = true;

            // register map
            csvReader.Configuration.RegisterClassMap<MyClassMap>();

            // register data model class
            // loop through and read in one line, array hits 1000 and bulk insert list, clear list, 
            var records = new List<Result>();
            var i = 0;
            while (csvReader.Read())
            {
                // <T>
                i++;
                if (i % 1000 == 0)
                {
                    Console.WriteLine(i.ToString());
                }

                try
                {
                    var record = csvReader.GetRecord<Result>();
                    records.Add(record);
                    if (records.Count > 9999)
                    {
                        // indexes current result - like add/save
                        elastic.IndexMany<Result>(records);
                        records.Clear();
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine(i.ToString());

                }

            }
            elastic.IndexMany<Result>(records);

            //while read in each loop.
            //     don't type to class
            //     record.getfield go by column indexing '
            //     var result = new Result.Age = records.getfield<Int32>{ 0}
            //     ReadOnlyUrlRepository into model, then do ElasticClient.index

            /*
            web app that can pull from elastic and send out by webservice
            This will bulk load data into Elastic 
            */
        }
    }
}




