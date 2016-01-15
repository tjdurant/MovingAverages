using highChartsTesting.Elastic;
using highChartsTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace highChartsTesting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MovingAverageModels tvModel = new MovingAverageModels();
            MovingAverage ma = new MovingAverage();
            JavaScriptSerializer javaSerial = new JavaScriptSerializer();

            var aggPath = @"C:\Users\thoma\Documents\00GitHub\MovingAverages\JsonQueries\stepwiseMovingAverage.txt";

            //System.IO.StreamReader myFile = new System.IO.StreamReader(aggPath);
            //string myString = myFile.ReadToEnd();

            var glucoseDay = ma.MovingAverageFunction(aggPath, "GLUCOSE", "65", "105", "day", "50");


            //tvModel.date = glucoseDay[0];
            //tvModel.value = glucoseDay[]

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}