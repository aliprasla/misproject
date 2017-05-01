﻿using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.Mvc;

using PraslaBonnerWondwossenFinalProject.Models;

using PraslaBonnerWondwossenFinalProject.StockUtilities;



namespace PraslaBonnerWondwossenFinalProject.Controllers

{

    public class StockController : Controller

    {

        // GET: Home

        public ActionResult Index()

        {

            List<StockQuote> Quotes = new List<StockQuote>();

            StockQuote sq1 = GetQuote.GetStock("AAPL");
            Quotes.Add(sq1);


            StockQuote sq2 = GetQuote.GetStock("GOOG");
            Quotes.Add(sq2);


            StockQuote sq3 = GetQuote.GetStock("AMZN");
            Quotes.Add(sq3);


            StockQuote sq4 = GetQuote.GetStock("LUV");
            Quotes.Add(sq4);



            StockQuote sq5 = GetQuote.GetStock("TXN");
            Quotes.Add(sq5);



            StockQuote sq6 = GetQuote.GetStock("HSY");
            Quotes.Add(sq6);


            StockQuote sq7 = GetQuote.GetStock("V");
            Quotes.Add(sq7);


            StockQuote sq8 = GetQuote.GetStock("NKE");
            Quotes.Add(sq8);

            StockQuote sq9 = GetQuote.GetStock("VWO");
            Quotes.Add(sq9);

            StockQuote sq10 = GetQuote.GetStock("CORN");
            Quotes.Add(sq10);


            StockQuote sq11 = GetQuote.GetStock("OBMCX");
            Quotes.Add(sq11);


            StockQuote sq12 = GetQuote.GetStock("F");
            Quotes.Add(sq12);


            StockQuote sq13 = GetQuote.GetStock("BAC");
            Quotes.Add(sq13);



            StockQuote sq14 = GetQuote.GetStock("VNQ");
            Quotes.Add(sq14);



            StockQuote sq15 = GetQuote.GetStock("NDX");
            Quotes.Add(sq15);


            StockQuote sq16 = GetQuote.GetStock("KMX");
            Quotes.Add(sq16);


            StockQuote sq17 = GetQuote.GetStock("DIA");
            Quotes.Add(sq17);

            StockQuote sq18 = GetQuote.GetStock("SPY");
            Quotes.Add(sq18);

            StockQuote sq19 = GetQuote.GetStock("BEN");
            Quotes.Add(sq19);

            StockQuote sq20 = GetQuote.GetStock("PGSCX");
            Quotes.Add(sq20);

            return View(Quotes);

        }

    }

}