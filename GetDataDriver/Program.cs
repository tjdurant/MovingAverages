﻿using CsvHelper;
using Elasticsearch.Net.Connection;
using highChartsTesting.Elastic;
using highChartsTesting.Models;
using Nest;
using Storage.Documents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace highChartsTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            MovingAverage ma = new MovingAverage();
            var aggPath = @"C:\Users\thoma\Documents\00GitHub\MovingAverages\JsonQueries\stepwiseMovingAverage.txt";

            //System.IO.StreamReader myFile = new System.IO.StreamReader(aggPath);
            //string myString = myFile.ReadToEnd();

            var glucoseDay = ma.MovingAverageFunction(aggPath, "GLUCOSE", "65", "105", "day", "50");

        }
    }
}
