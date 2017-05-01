
using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using PraslaBonnerWondwossenFinalProject.Models;

using System.Net;

using System.IO;



namespace PraslaBonnerWondwossenFinalProject.StockUtilities

{

    public static class GetQuote

    {

        // Get stock quote by passing symbol

        public static StockQuote GetStock(string symbol)

        {

            //s -> symbol

            //g -> days low

            //h -> days high

            //c -> change

            //o -> open

            //p -> previous close

            //v -> volume

            //l1 -> last trade (price only) -> close

            //n -> name

            string baseURL = "http://finance.yahoo.com/d/quotes.csv?s={0}&f=pl1vn";

            string url = string.Format(baseURL, symbol);



            //Get page showing the table with the chosen indices

            System.Net.HttpWebRequest request = null;



            //csv content

            string docText = string.Empty;

            StockQuote stock = null;

            try

            {

                request = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));

                request.Timeout = 300000;



                using (var response = (HttpWebResponse)request.GetResponse())

                using (StreamReader stReader = new StreamReader(response.GetResponseStream()))

                {

                    string output = stReader.ReadLine();

                    //"\"Apple Inc.\",587.44,572.98,36820544"



                    string[] sa = output.Split(new char[] { ',' });



                    stock = new StockQuote();

                    stock.Symbol = symbol;

                    stock.Name = sa[3];

                    stock.PreviousClose = double.Parse(sa[0]);

                    stock.LastTradePrice = double.Parse(sa[1]);

                    stock.Volume = double.Parse(sa[2]);

                }

            }

            catch (Exception e)

            {

                //Throw some exception here.

            }

            return stock;

        }

    }

}