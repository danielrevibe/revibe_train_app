using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;

namespace LrtOperator.Service
{
    public class MyRestRequests
    {
        public RestClient Client(string reqType)
        {
            RestClient client;

            switch (reqType)
            {
                case "position":
                    client = new RestClient("https://pos.cit.api.here.com");
                    return client;

                case "places":
                    client = new RestClient("https://places.cit.api.here.com");
                    return client;

                default:
                    client = new RestClient(reqType);
                    return client;
            }

        }
    }
}