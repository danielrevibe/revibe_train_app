using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LrtOperator.Model;
using Newtonsoft.Json;
using Plugin.Geolocator;
using SocketIO.Client;


namespace LrtOperator
{
    public class HomeFragment : Fragment
    {
        Button startFeed;
        Button stopFeed;

        Button btnLight;
        Button btnModerate;
        Button btnHeavy;

        //needed for location
        LocationManager lm;

        //global for dialog to call anywhere in the class.
        Android.App.AlertDialog.Builder alert;
        Dialog dialog;

        SocketIO.Client.Socket socket;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.home_layout, container, false);

            startFeed = view.FindViewById<Button>(Resource.Id.btnStart);
            startFeed.Click += StartFeed_Click;


            stopFeed = view.FindViewById<Button>(Resource.Id.btnStop);
            stopFeed.Click += StopFeed_Click;

            btnLight = view.FindViewById<Button>(Resource.Id.btnLight);
            btnLight.Click += BtnLight_Click;

            btnModerate = view.FindViewById<Button>(Resource.Id.btnModerate);
            btnModerate.Click += BtnModerate_Click;

            btnHeavy = view.FindViewById<Button>(Resource.Id.btnHeavy);
            btnHeavy.Click += BtnHeavy_Click;


            ConnectToServer();

            return view;

        }


        //connect client to tcp
        void ConnectToServer()
        {
            //socket = IO.Socket("http://192.168.8.100:5000/train");
            socket = IO.Socket("https://navigo.serveo.net/train");

            socket.On(Socket.EventConnect, (data) => {
            
               
            });

            socket.Connect();

        }


        private void BtnHeavy_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnModerate_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnLight_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        async void GPSFeed(bool feed)
        {

            while (feed)
            {
                var data = await RetrieveLocation();
                var jsonGPS = JsonConvert.SerializeObject(data);

                //send to websocket
                socket.Emit("train_feed", jsonGPS);

                Activity.RunOnUiThread(() =>
                {

                    Toast.MakeText(Activity, jsonGPS, ToastLength.Long).Show();

                });

                await Task.Delay(5000);

            }
        }

        private async Task<GPSdata> RetrieveLocation()
        {

            //c.acceleration = "60";
            //coord.longitude = "120.98202";
            //coord.latitude = "14.60571";

            ////1st attempt: Get user's location via GPS
            if (IsGPSRunning())
            {
               
                //Get current location.
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                Coordinates coord = new Coordinates();

                coord.longitude = position.Longitude.ToString();
                coord.latitude = position.Latitude.ToString();

                GPSdata c = new GPSdata();

                c.acceleration = position.Speed.ToString();

                c.train_plate = "12345";
                c.coordinates = coord;

                return c;
            }

            else
            {
                return null;
            }

        }

        private async void StopFeed_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                GPSFeed(false);

            });
        }

        private async void StartFeed_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                GPSFeed(true);
            });
        }


        //Method for checking if gps is running.
        private bool IsGPSRunning()
        {
            bool gps_enabled = false;

            try
            {
                gps_enabled = lm.IsProviderEnabled(LocationManager.GpsProvider);
                return gps_enabled;
            }
            catch (Exception) { return false; }
        }

        //Method is for displaying alert dialog
        void AlertDialog(string message)
        {
            alert = new Android.App.AlertDialog.Builder(Activity);
            alert.SetTitle("Error");
            alert.SetMessage(message);
            alert.SetNegativeButton("Close", (senderAlert, args) =>
            {

            });

            dialog = alert.Create();
            dialog.Show();
        }
    }
}