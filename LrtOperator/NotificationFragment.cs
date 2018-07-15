using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using LrtOperator.Adapter;
using LrtOperator.Model;

namespace LrtOperator
{
    public class NotificationFragment : Fragment
    {

        //holder for list of nearby stores
        RecyclerView _notifications;

        //responsible for laying out the list of stores
        RecyclerView.LayoutManager _LayoutManager;

        List<Notifications> _notificationList = new List<Notifications>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.notifications_layout, container, false);

            //attach recycler view control to backend recycler.
            _notifications = view.FindViewById<RecyclerView>(Resource.Id.recyclerView1);

            // Plug in the linear layout manager:
            _LayoutManager = new LinearLayoutManager(Activity);
            _notifications.SetLayoutManager(_LayoutManager);

            LoadNotifications();
            
            return view;
        }

        async void LoadNotifications()
        {
            _notificationList.Add(new Notifications
            {
                Name = "Daniel Sarabusing",
                Content = "Aircon not functioning",
                Time = "08:00AM"
               
            });

            _notificationList.Add(new Notifications
            {
                Name = "Daniel Sarabusing",
                Content = "Aircon not functioning",
                Time = "08:00AM"

            });

            _notificationList.Add(new Notifications
            {
                Name = "Enber Francisco",
                Content = "Aircon problem",
                Time = "08:01AM"

            });

            _notificationList.Add(new Notifications
            {
                Name = "JB Rillo",
                Content = "Mainit po",
                Time = "08:03AM"

            });

            _notificationList.Add(new Notifications
            {
                Name = "Rico Arabia",
                Content = "Sira ang aircon",
                Time = "08:05AM"

            });

            NotificationAdapter adapter = new NotificationAdapter(Activity, _notificationList);
            _notifications.SetAdapter(adapter);
            adapter.NotifyDataSetChanged();

        }
    }
}