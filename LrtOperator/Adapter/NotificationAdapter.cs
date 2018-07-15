using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using LrtOperator.Model;

namespace LrtOperator.Adapter
{
    class NotificationAdapter : RecyclerView.Adapter
    {

        Context _context;
        List<Notifications> _notifications;

        public event EventHandler<int> ItemClick;


        public NotificationAdapter(Context context, List<Notifications> notifications)
        {
            _context = context;
            _notifications = notifications;
        }

        public override int ItemCount
        {
            get
            {
                return _notifications.Count();
            }
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            NotifyViewHolder vh = holder as NotifyViewHolder;
            vh.Name.Text = _notifications[position].Name;
            vh.Content.Text = _notifications[position].Content;
            vh.Time.Text = _notifications[position].Time;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.notification_card_view, parent, false);
            NotifyViewHolder vh = new NotifyViewHolder(itemView, _context, OnClick);
            return vh;
        }

        private void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

    public class NotifyViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView Content { get; private set; }
        public TextView Time { get; private set; }
        
        public NotifyViewHolder(View itemView, Context context, Action<int> listener) : base(itemView)
        {

            Name = itemView.FindViewById<TextView>(Resource.Id.txtName);
            Content = itemView.FindViewById<TextView>(Resource.Id.txtContent);
            Time = itemView.FindViewById<TextView>(Resource.Id.txtTime);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

}