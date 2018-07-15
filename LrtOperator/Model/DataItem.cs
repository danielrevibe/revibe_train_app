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

namespace LrtOperator.Model
{

    public class Coordinates
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class GPSdata
    {
        public string train_plate { get; set; }
        public Coordinates coordinates { get; set; }
        public string acceleration { get; set; }
    }


    public class Notifications
    {
        public string Name;
        public string Content;
        public string Time;
    }

}
